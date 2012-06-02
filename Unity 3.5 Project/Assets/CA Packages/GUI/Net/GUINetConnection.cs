using UnityEngine;
using System;
using System.Collections;

public class GUINetConnection : MonoBehaviour {
	
    public GUISkin Skin;
	
    public GameObject LevelLoaderObj;

	private Rect ServerConnectRect;
	private Rect ServerDisconnectRect;
	private Rect ServerListRect;
    
    private float LastHostListRequest = -1000.0f;
	private float HostListRefreshTimeout = 10.0f;
	
	private string PlayerName = "Player Name";
	private string GameName = "CodenameAxix";
	private string ServerIP = "127.0.0.1";
	private int ServerPort = 29000;
	private bool UseNAT = false;
	
	private bool ShowDisconnect = false;

    public bool ShowingDisconnect()
    {
        return ShowDisconnect;
    }

	void Awake() {
        int Index = UnityEngine.Random.Range(0, Names.Length-1);
        PlayerName = Names[Index];

 		MasterServer.ipAddress = "10.0.11.19";
    	MasterServer.port = 23466;
        MasterServer.dedicatedServer = true;
		
		ServerConnectRect = new Rect(Screen.width / 2 - 140, Screen.height / 2 - 250, 280, 150);
		ServerDisconnectRect = new Rect(Screen.width / 2 - 140, Screen.height / 2 - 50, 280, 20);
		ServerListRect = new Rect(Screen.width / 2 - 300, Screen.height / 2 + 60, 600, 100);

        GameObject LevelLoader = GameObject.FindGameObjectWithTag("LevelLoader");

        // The level loader doesn't exist, create one for this run and set it the level.
        if (!LevelLoader)
        {
            GameObject LevelLoaderObjectClone;
            LevelLoaderObjectClone = Instantiate(LevelLoaderObj) as GameObject;

            AuthServerLevelLoader LevelLoaderScript = LevelLoaderObjectClone.GetComponent<AuthServerLevelLoader>();
            if (LevelLoaderScript)
            {
                LevelLoaderScript.SetAutoInit(false);

                string SceneName = Application.loadedLevelName;
                LevelLoaderScript.Level = SceneName;
                LevelLoaderScript.StartLoadLevel(SceneName);
            }
        }
	}
		
	void OnFailedToConnectToMasterServer(NetworkConnectionError Info) {
		Debug.Log(Info);
	}

	void OnFailedToConnect(NetworkConnectionError Info) {
		Debug.Log(Info);
    }

    void OnServerInitialized()
    {
        DeathmatchLogicScript Logic = FindObjectOfType(typeof(DeathmatchLogicScript)) as DeathmatchLogicScript;
        if (Logic)
            Logic.SetJoinData(PlayerName);
    }

    void OnConnectedToServer()
    {
        DeathmatchLogicScript Logic = FindObjectOfType(typeof(DeathmatchLogicScript)) as DeathmatchLogicScript;
        if (Logic)
            Logic.SetJoinData(PlayerName);
    }

	// Use this for initialization
	void Start () {
	
	}

    void OnGUI()
    {
		GUI.skin = Skin;
		
        if (Network.peerType == NetworkPeerType.Disconnected) {
            ServerConnectRect = GUILayout.Window(0, ServerConnectRect, MakeServerConnectWindow, "Server Controls");
        }
		if(Network.peerType != NetworkPeerType.Disconnected && ShowDisconnect) {
			ServerDisconnectRect = GUILayout.Window (1, ServerDisconnectRect, MakeServerDisconnectWindow, "Server Controls");
		}
		if (Network.peerType == NetworkPeerType.Disconnected)
			ServerListRect = GUILayout.Window(2, ServerListRect, MakeServerListWindow, "Server List");
			
		GUILayout.BeginHorizontal();
		if(Network.isServer)
			GUILayout.Label("Connected as server");
		else if(Network.isClient)
			GUILayout.Label("Connected as client");
		else
			GUILayout.Label("Not Connected");
		GUILayout.EndHorizontal();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			ShowDisconnect = !ShowDisconnect;
		}
	}
	
	void MakeServerDisconnectWindow(int ID) {
		Screen.lockCursor = false;
		
		GUILayout.Space(10);
		if (GUILayout.Button ("Disconnect")) {
			Network.Disconnect();
			MasterServer.UnregisterHost();
		}
		if (GUILayout.Button ("Quit")) {
			Application.Quit();
		}
		GUILayout.Space(10);
	}
	
	void MakeServerConnectWindow(int ID) {
		Screen.lockCursor = false;
		
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        // Start a new server
        GUILayout.Label("Game Name");
        GameName = GUILayout.TextField(GameName, GUILayout.Width(140));
        GUILayout.Space(10);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        // Start a new server
        GUILayout.Label("Player Name");
        PlayerName = GUILayout.TextField(PlayerName, GUILayout.Width(140));
        GUILayout.Space(10);
        GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		// Start a new server
        if (GUILayout.Button("Start Server (Port " + ServerPort + ")", GUILayout.Width(240)))
        {
            Network.InitializeServer(15, ServerPort, UseNAT);
            MasterServer.RegisterHost("AXIX", GameName, "Just A Bit Of Fun!");
		}

		GUILayout.EndHorizontal();
		
		GUILayout.Space(10);
		
		GUILayout.BeginHorizontal();	
		GUILayout.Space(10);
		ServerIP = GUILayout.TextField(ServerIP, GUILayout.Width(140));
		ServerPort = Convert.ToInt32(GUILayout.TextField(ServerPort.ToString(), GUILayout.Width(95)));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		// Start a new server
        if (GUILayout.Button("Join Local Server (DEBUG)", GUILayout.Width(240)))
        {
            Network.Connect(ServerIP, ServerPort);
		}
		GUILayout.EndHorizontal();
			
		GUILayout.Space(10);
			
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		// Start a new server
		if (GUILayout.Button ("Quit", GUILayout.Width(240))) {
			Application.Quit();
		}

		GUILayout.EndHorizontal();
			
		GUILayout.Space(10);
	}
	
	void MakeServerListWindow(int ID) {
		GUILayout.BeginHorizontal();
		if(MasterServer.PollHostList().Length == 0)
			GUILayout.Label("No servers found");
		else
			GUILayout.Label(MasterServer.PollHostList().Length + " Server(s) found");
		
		// Refresh hosts
		if (GUILayout.Button ("Refresh available Servers", GUILayout.Width(240)) || Time.realtimeSinceStartup > LastHostListRequest + HostListRefreshTimeout) {
            MasterServer.RequestHostList("AXIX");
			LastHostListRequest = Time.realtimeSinceStartup;
		}
		GUILayout.EndHorizontal();

        HostData[] Servers = MasterServer.PollHostList();
        GUILayout.BeginVertical();
        foreach (HostData Server in Servers) {
            GUILayout.BeginHorizontal();

			GUILayout.Label(Server.gameName);
			GUILayout.Space(5);
			
			string CurrentConnections = Server.connectedPlayers + " / " + Server.playerLimit;
			GUILayout.Label(CurrentConnections);
			GUILayout.Space(5);
			
			// Could be more than one IP (Internal LAN, external connection),
			// Unity tests all, so lets list them all. (Player probably doesn't 
			// need to know about them all)
			string hostInfo = "";
			foreach (string host in Server.ip) {
				hostInfo = hostInfo + host + ":" + Server.port + (Server.ip.Length > 1 ? ", " : "");
			}	
			
			GUILayout.Label(hostInfo);	
			GUILayout.Space(5);
			GUILayout.Label(Server.comment);
			GUILayout.Space(5);
			GUILayout.FlexibleSpace();

            if (GUILayout.Button("GO ->"))
            {
                Network.Connect(Server);
			}

            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
	}

    private string[] Names = 
    {
        "Penetrator",
        "Kenneth",
        "Palette",
        "Mark",
        "Parmesian",
        "Spitfire",
        "Eva",
        "Alpro",
        "Redtail",
        "Infiltrator",
        "RearEnd",
        "White Mice",
        "Unconventional",
        "Penis Man",
        "The Bin Man",
        "The Evacuator",
        "Giant Lump",
        "Ejaculatory",
        "The Sperminator",
        "Jester",
        "Maverick",
        "Ice Man",
        "Slider",
        "Ironside",
        "T-Bag",
        "Sundown",
        "Max",
        "Wizard",
        "Merlin",
        "BARRY",
        "Vitamin C",
        "Zoolander",
        "Coaster",
        "Plank",
        "Neo1988",
        "n3o",
        "ETC",
        "Read End Connection",
        "Blind",
        "Z - Ray",
        "Egg-man",
        "AXIX",
        "Sergio Georgini",
    };
}
