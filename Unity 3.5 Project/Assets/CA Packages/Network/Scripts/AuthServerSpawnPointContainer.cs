using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AuthServerSpawnPointContainer : MonoBehaviour {
	
	private int CurrentSpawnPoint = 0;
	public Transform[] SpawnPoints;
	public Transform ObserverCamera;

    public GameObject ShipCustomisationPrefab;
    GameObject ShipCustomisation = null;

    private Dictionary<NetworkPlayer, GameObject> SpawnedPlayers = new Dictionary<NetworkPlayer, GameObject>();

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void OnNetworkLoadedLevel()
    {
        if (Network.isServer && !ShipCustomisation)
        {
            ShipCustomisation = Network.Instantiate(ShipCustomisationPrefab, transform.position, transform.rotation, 0) as GameObject;
        }
	}
	
	void OnPlayerDisconnected(NetworkPlayer Player) {
 		Network.RemoveRPCs(Player);
		Network.DestroyPlayerObjects(Player);

        GameObject go = SpawnedPlayers[Player];
			
		NetOwnership NetOwnershipScript = go.GetComponent<NetOwnership>();

		if(NetOwnershipScript.IsOwner(Player)) {
			Debug.Log("Is Owner");
			Network.RemoveRPCs(go.networkView.viewID);
            Network.Destroy(go.networkView.viewID);

            SpawnedPlayers.Remove(Player);
		}
	}
	   
	public void RequestRespawnPlayer(NetworkPlayer NetPlayer) {
		Debug.Log("Requesting Respawn");
    	if(Network.isServer) {
            ServerRespawn(NetPlayer);
    	}
    	else {
            networkView.RPC("ServerRespawn", RPCMode.Server, NetPlayer);
        }

        // Turn off the observer Cam
        Camera ObserverCam = ObserverCamera.GetComponent<Camera>();
        ObserverCam.enabled = false;

        AudioListener Listener = ObserverCamera.GetComponent<AudioListener>();
        Listener.enabled = false;
    }

    public void DoSpawn(NetworkPlayer NetPlayer)
    {
        RequestRespawnPlayer(NetPlayer);
    }
	
	// The network has been initialised and the level has been loaded.
	void OnInitialisePlayer(NetworkPlayer NetPlayer) {
        NetworkPlayer Player = NetPlayer;
		
		OnServerPlayerConnectedAndLoadedLevel(Player);
	}
	
	[RPC]
	void OnServerPlayerConnectedAndLoadedLevel(NetworkPlayer Player) {		
		
		AuthServerSpawnPoint SpawnPoint = SpawnPoints[CurrentSpawnPoint].GetComponent<AuthServerSpawnPoint>();
		if(SpawnPoint) {
			
			// Spawn Player and let client know which controller it has.
			GameObject go = SpawnPoint.OnServerSpawnController(Player);

            SpawnedPlayers.Add(Player, go);
			
			NetOwnership NetOwnershipScript = go.GetComponent<NetOwnership>();
			if(NetOwnershipScript) {
				NetOwnershipScript.networkView.RPC("SetPlayer", RPCMode.AllBuffered, Player);
			}
			
			RespawnScript Respawn = go.GetComponent<RespawnScript>();
			Respawn.StartRespawnServer();
            Respawn.StartRespawnClient(go.transform.position, go.transform.rotation);
		}
		
		CurrentSpawnPoint++;
		if(CurrentSpawnPoint >= SpawnPoints.Length)
			CurrentSpawnPoint = 0;
	}
	
	[RPC]
	public void ServerRespawn(NetworkPlayer NetPlayer) {

        if (SpawnedPlayers.ContainsKey(NetPlayer))
        {
            GameObject go = SpawnedPlayers[NetPlayer];

            RespawnScript Respawn = go.GetComponent<RespawnScript>();
            Respawn.StartRespawnServer();

            AuthServerSpawnPoint SpawnPoint = SpawnPoints[CurrentSpawnPoint].GetComponent<AuthServerSpawnPoint>();
            go.transform.position = SpawnPoint.transform.position;
            go.transform.rotation = SpawnPoint.transform.rotation;

            CurrentSpawnPoint++;
            if (CurrentSpawnPoint >= SpawnPoints.Length)
                CurrentSpawnPoint = 0;

            Respawn.StartRespawnClient(go.transform.position, go.transform.rotation);
        }
        else
        {
            Debug.Log("Respawn - Init Player");
            OnInitialisePlayer(NetPlayer);
        }
	}
}
