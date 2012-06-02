using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Reflection;

[RequireComponent(typeof(NetworkView))]
public class BaseGameLogic : MonoBehaviour {
    class GameJoinData
    {
        public string PlayerName = "";
    }
    private GameJoinData JoinData = new GameJoinData();
	
	/***
	* A GamePlayer contains all game related data. If you implement BaseGameLogic, be sure to extend this
	* with any needed information.
	***/
	public class GamePlayer {
		// Each class that requires a sorted list should provide an IComparer interface.
        public class GamePlayerCompare : IComparer<GamePlayer>
        {
            public int Compare(GamePlayer x, GamePlayer y)
            {
                if (y.PlayerName[0] > x.PlayerName[0])
                    return 1;
                if (x.PlayerName[0] > y.PlayerName[0])
                    return -1;
                return 0;
            }
        }
		
		// Our network player
        public NetworkPlayer NetPlayer;
		
		// Some player specific information
		public string PlayerName = "Player!";
        public int Class = 0;
        public int Perk = -1;
        public int LeftWeapon = 0;
        public int RightWeapon = 0;
        public Color PrimaryColour;
        public Color SecondaryColour;
	}
	
    public LogWindowScript LogWindow;
    protected List<GamePlayer> PlayerList = new List<GamePlayer>();

    public List<GamePlayer> GetPlayerList()
    {
        return PlayerList;
    }
	
	void Awake() {
		DontDestroyOnLoad(this);
		networkView.group = 1;
	}
	
	void OnNetworkLoadedLevel() {
		networkView.RPC("AddPlayer", RPCMode.AllBuffered, Network.player, JoinData.PlayerName);
	}

	void OnPlayerDisconnected(NetworkPlayer NetPlayer) {
		networkView.RPC("RemovePlayer", RPCMode.AllBuffered, NetPlayer);
	}

    void OnDisconnectedFromServer()
    {
        PlayerList.Clear();
    }
	
	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
	}
	
    public string GetPlayerName(NetworkPlayer NetPlayer)
    {
        foreach (GamePlayer Player in PlayerList)
        {
            if (Player.NetPlayer == NetPlayer)
                return Player.PlayerName;
        }

        return null;
    }
    
    public int GetLeftWeapon(NetworkPlayer NetPlayer)
    {
        foreach (GamePlayer Player in PlayerList)
        {
            if (Player.NetPlayer == NetPlayer)
                return Player.LeftWeapon;
        }
        
        return 0;
    }
        
    public int GetRightWeapon(NetworkPlayer NetPlayer)
    {
        foreach (GamePlayer Player in PlayerList)
        {
            if (Player.NetPlayer == NetPlayer)
                return Player.RightWeapon;
        }
        
        return 0;
    }
        
    public int GetClass(NetworkPlayer NetPlayer)
    {
        foreach (GamePlayer Player in PlayerList)
        {
            if (Player.NetPlayer == NetPlayer)
                return Player.Class;
        }
        
        return 1;
    }
    
    public Color GetPrimaryColour(NetworkPlayer NetPlayer)
    {
        foreach (GamePlayer Player in PlayerList)
        {
            if (Player.NetPlayer == NetPlayer)
                return Player.PrimaryColour;
        }
        
        return Color.red;
    }
    
    public Color GetSecondaryColour(NetworkPlayer NetPlayer)
    {
        foreach (GamePlayer Player in PlayerList)
        {
            if (Player.NetPlayer == NetPlayer)
                return Player.SecondaryColour;
        }
        
        return Color.red;
	}
	
    public void SetJoinData(string PlayerName)
    {        
        JoinData.PlayerName = PlayerName;
    }
	
	public virtual void OnPlayerDied(NetworkPlayer Killer, NetworkPlayer Victim)
	{
		
	}
	
    public virtual void OnPlayerKilled(NetworkPlayer Killer, NetworkPlayer Victim)
	{
		
	}

    public void SetCustomisationData(NetworkPlayer NetPlayer, int Class, int Perk, int LeftWeapon, int RightWeapon,
        float PrimaryColourR, float PrimaryColourG, float PrimaryColourB,
        float SecondaryColourR, float SecondaryColourG, float SecondaryColourB)
    {
        Debug.Log("SetCustomisationData");
        for (int n = PlayerList.Count - 1; n >= 0; n--)
        {
            if (PlayerList[n].NetPlayer == NetPlayer)
            {
                int OldPerk = PlayerList[n].Perk;

                PlayerList[n].Class = Class;
                PlayerList[n].Perk = Perk;
                PlayerList[n].LeftWeapon = LeftWeapon;
                PlayerList[n].RightWeapon = RightWeapon;
                PlayerList[n].PrimaryColour = new Color(PrimaryColourR, PrimaryColourG, PrimaryColourB);
                PlayerList[n].SecondaryColour = new Color(SecondaryColourR, SecondaryColourG, SecondaryColourB);

                GameObject[] Targets;
                Targets = GameObject.FindGameObjectsWithTag("Player");
                foreach(GameObject go in Targets)
                {
                    if(go.GetComponent<NetOwnership>() && go.GetComponent<NetOwnership>().GetOwner() == NetPlayer)
                    {
                        InventoryManagerScript Inv = go.GetComponentInChildren<InventoryManagerScript>();
                        if (Inv)
                        {
                            Debug.Log("Clearing");
                            Inv.ClearWeapons();
                        }

                        if (Network.isServer)
                        {
                            AttachmentSystem AttachmentSystem = go.GetComponent<AttachmentSystem>();
                            if (AttachmentSystem)
                                AttachmentSystem.SetWeapons(LeftWeapon, RightWeapon);

                            Classes OurClass = go.GetComponent<Classes>();
                            if (OurClass)
                                OurClass.SetClass(Class);

                            if (OldPerk != PlayerList[n].Perk)
                            {
                                // Update Perks
                                PerkSystem Perks = go.GetComponentInChildren<PerkSystem>();
                                if (Perks)
                                {
                                    Perks.ClearPerks();
                                    Perks.AddPerk(Perk);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
	
	[RPC]
	public virtual void AddPlayer(NetworkPlayer NetPlayer, string PlayerName)
    {
		GamePlayer Player = new GamePlayer();
        Player.PlayerName = PlayerName;
        Player.NetPlayer = NetPlayer;

		PlayerList.Add(Player);

        if (LogWindow)
            LogWindow.AddMessage(PlayerName + " has Joined the game!");
	}
	
	[RPC]
    public virtual void RemovePlayer(NetworkPlayer NetPlayer)
    {
        for(int n = PlayerList.Count - 1; n >= 0; n--)
        {
            if (PlayerList[n].NetPlayer == NetPlayer)
            {
                if (LogWindow)
                    LogWindow.AddMessage(PlayerList[n].PlayerName + " has left the game!");

                PlayerList.Remove(PlayerList[n]);
            }
        }
	}
	
    public virtual void OnSerializeNetworkView(BitStream Stream, NetworkMessageInfo Info) {
        // Since this is spawned on everyones machine, we simply send the info, only if we are the server, since all decision making happens on the server.
        if (Stream.isWriting && !Network.isServer)
            return;

		if (Stream.isWriting) 
        {
			// Write Something
		}
        else
        {
			// Read Something
		}
	}
}
