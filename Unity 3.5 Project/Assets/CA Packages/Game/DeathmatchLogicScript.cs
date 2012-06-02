using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Reflection;

public class DeathmatchLogicScript : BaseGameLogic {
	
	public class DeathmatchPlayer : GamePlayer {
		// We extend GamePlayer with our Kills comparer.
        public class DeathmatchPlayerCompare : IComparer<DeathmatchPlayer>
        {
            public int Compare(DeathmatchPlayer x, DeathmatchPlayer y)
            {
                if (y.Kills > x.Kills)
                    return 1;
                if (x.Kills > y.Kills)
                    return -1;
                return 0;
            }
        }
		
		// Deathmatch specific GamePlayer attrbutes.
		public int Kills = 0;
		public int Deaths = 0;
        public int Cash = 0;
	}

    public KillMessageWindowScript KillWindow;
    public BlackBoxMessageWindowScript BlackBoxWindow;
	
	public override void OnPlayerDied(NetworkPlayer Killer, NetworkPlayer Victim) {
        string KillerName = "";
        string VictimName = "";

        foreach (DeathmatchPlayer Player in PlayerList)
        {
            if (Player.NetPlayer == Killer)
                KillerName = Player.PlayerName;
        }

        foreach (DeathmatchPlayer Player in PlayerList)
        {
            if (Player.NetPlayer == Victim)
            {
                VictimName = Player.PlayerName;
                Player.Deaths++;
            }
        }

        if (LogWindow)
        {
            if (Killer != Victim)
            {
                LogWindow.AddMessage(VictimName + " was killed by " + KillerName);

                if (Killer == Network.player)
                {
                    KillWindow.AddMessage("+ 1 Kill");
                }
            }
            else
            {
                LogWindow.AddMessage(VictimName + " killed themselves ");
            }
        }
		
		base.OnPlayerDied(Killer, Victim);
	}

    public override void OnPlayerKilled(NetworkPlayer Killer, NetworkPlayer Victim)
    {
        if (Killer != Victim)
        {
            foreach (DeathmatchPlayer Player in PlayerList)
            {
                if (Player.NetPlayer == Killer)
                {
                    Player.Kills++;
                }
            }
        }
		
		base.OnPlayerKilled(Killer, Victim);
	}

    public void OnBlackBoxPickedup(NetworkPlayer NetInstigator, NetworkPlayer NetOwner, int CashObtained)
    {
        networkView.RPC("DisplayBlackBoxMessage", RPCMode.All, NetInstigator, NetOwner, CashObtained);
    }

    [RPC]
    void DisplayBlackBoxMessage(NetworkPlayer NetInstigator, NetworkPlayer NetOwner, int CashObtained)
    {
        string InstigatorName = "";
        string OwnerName = "";
        foreach (DeathmatchPlayer Player in PlayerList)
        {
            if (Player.NetPlayer == NetInstigator)
            {
                InstigatorName = Player.PlayerName;
                Player.Cash += CashObtained;
            }
        }
        foreach (DeathmatchPlayer Player in PlayerList)
        {
            if (Player.NetPlayer == NetOwner)
            {
                OwnerName = Player.PlayerName;
            }
        }

        if (LogWindow)
        {
            if (NetInstigator == Network.player)
            {
                BlackBoxWindow.AddMessage("+ ¤" + CashObtained);
            }

            if (OwnerName == "")
            {
                LogWindow.AddMessage(InstigatorName + " COLLECTED a stray BlackBox");
            }
            else if (NetInstigator == NetOwner)
            {
                LogWindow.AddMessage(InstigatorName + " RECOLLECTED their own Blackbox");
            }
            else
            {
                LogWindow.AddMessage(InstigatorName + " STOLE ¤" + CashObtained + " from " + OwnerName + "'s BlackBox");
            }
        }
    }

    [RPC]
    public override void AddPlayer(NetworkPlayer NetPlayer, string PlayerName)
    {
        DeathmatchPlayer Player = new DeathmatchPlayer();
        Player.PlayerName = PlayerName;
        Player.NetPlayer = NetPlayer;

        PlayerList.Add(Player);

        if (LogWindow)
            LogWindow.AddMessage(PlayerName + " has Joined the game!");
    }
	    
	public override void OnSerializeNetworkView(BitStream Stream, NetworkMessageInfo Info) {
        // Since this is spawned on everyones machine, we simply send the info, only if we are the server, since all decision making happens on the server.
        if (Stream.isWriting && !Network.isServer)
            return;

		if (Stream.isWriting) 
        {
			foreach(DeathmatchPlayer Player in PlayerList) {
				NetworkPlayer NetPlayer = Player.NetPlayer;
				int Kills = Player.Kills;
				int Deaths = Player.Deaths;
                int Cash = Player.Cash;
				
				Stream.Serialize(ref NetPlayer);
				Stream.Serialize(ref Kills);
                Stream.Serialize(ref Deaths);
                Stream.Serialize(ref Cash);
			}
		}
        else
        {
            foreach (DeathmatchPlayer Player in PlayerList)
            {
				NetworkPlayer NetPlayer = Player.NetPlayer;
				int Kills = 0;
                int Deaths = 0;
                int Cash = 0;
				
				Stream.Serialize(ref NetPlayer);
				Stream.Serialize(ref Kills);
                Stream.Serialize(ref Deaths);
                Stream.Serialize(ref Cash);
				
            	foreach (DeathmatchPlayer PlayerData in PlayerList)
            	{
            		if(PlayerData.NetPlayer == NetPlayer)
            		{
						PlayerData.Kills = Kills;
						PlayerData.Deaths = Deaths;
                		PlayerData.Cash = Cash;
            		}
            	}
			}
		}
	}
}