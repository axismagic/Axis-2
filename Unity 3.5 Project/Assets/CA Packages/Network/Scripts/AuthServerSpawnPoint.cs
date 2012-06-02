using UnityEngine;
using System.Collections;

public class AuthServerSpawnPoint : MonoBehaviour {

	public GameObject PrefabToSpawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject OnServerSpawnController(NetworkPlayer Player) {
        Debug.Log("Server Spawn Controller");
        GameObject go = Network.Instantiate(PrefabToSpawn, transform.position, transform.rotation, 0) as GameObject;

        // Try and grab our name
        string PlayerName = "";
        DeathmatchLogicScript Logic = FindObjectOfType(typeof(DeathmatchLogicScript)) as DeathmatchLogicScript;
        if (Logic)
        {
            PlayerName = Logic.GetPlayerName(Player);
        }

        NameScript Name = go.GetComponent<NameScript>();
        if(Name)
            Name.ServerSetName(PlayerName);
		
        return go;
	}
}