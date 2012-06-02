using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]
public class AuthServerLevelLoader : MonoBehaviour {
	
	private int LastLevelID = 10;
	public string DisconnectedLevel = "";
	public string Level = "Level 0";

    private bool AutoInit = true;
    public void SetAutoInit(bool bAutoInit) {
        AutoInit = bAutoInit;
    }
	
	void Awake() {
	}
	
	void OnServerInitialized() {
		StartLoadLevel(Level);
	}

	// Use this for initialization
    void Start() {
        DontDestroyOnLoad(this);
        networkView.group = 1;

        if (!AutoInit)
            return;

        Application.LoadLevel(DisconnectedLevel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// This only works on the server
	public void StartLoadLevel(string Level) {
		// Only the server can load a level, the RPC gets buffered on any future connectees machines
		if(!Network.isServer)
			return;
		
		// This Removes all previous Load Level Messages
		Network.RemoveRPCsInGroup(0);
		Network.RemoveRPCsInGroup(1);
		
		// Load level with incremented level prefix (for view IDs)
		networkView.RPC( "LoadLevel", RPCMode.AllBuffered, Level, LastLevelID++);
	}
	
	// When the level has loaded we need to ensure we can recieve messages again.
	void OnLevelWasLoaded(int Level) {
		
		// The first two levels in our game are not playable.
		if(Level < 2)
			return;
		
		// Allow receiving data again
		Network.isMessageQueueRunning = true;
		// Now the level has been loaded and we can start sending out data
		Network.SetSendingEnabled(0, true);

		// Notify our objects that the level and the network is ready
		foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);	
			
		Debug.Log("Network Level Has Been Loaded");
	}
	
	[RPC]
	public void LoadLevel(string Level, int LevelID) {

		// There is no reason to send any more data over the network on the default channel,
		// because we are about to load the level, thus all those objects will get deleted anyway
		Network.SetSendingEnabled(0, false);	

		// We need to stop receiving because first the level must be loaded.
		// Once the level is loaded, RPC's and other state update attached to objects in the level are allowed to fire
		Network.isMessageQueueRunning = false;
		
		// All network views loaded from a level will get a prefix into their NetworkViewID.
		// This will prevent old updates from clients leaking into a newly created scene.
		Network.SetLevelPrefix(LevelID);
		Application.LoadLevel(Level);
		
		LastLevelID = LevelID;
	}

	void OnDisconnectedFromServer() {
		Application.LoadLevel(DisconnectedLevel);
	}
}