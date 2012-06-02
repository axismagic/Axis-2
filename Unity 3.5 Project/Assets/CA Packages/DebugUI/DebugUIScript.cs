using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Reflection;

public class DebugUIScript : MonoBehaviour
{
    public GUISkin Skin;
	
    private List<GameObject> VisiblePlayers = new List<GameObject>();
    private List<GameObject> Players = new List<GameObject>();
    private GameObject ThisPlayer = null;

    private List<Camera> Cameras = new List<Camera>();
    private Camera ThisCamera = null;

    PerkScript Perk = null;

    private Rect DeathMatchScoresRect;
    private bool DeathMatchShowScores = false;
    List<DeathmatchLogicScript.DeathmatchPlayer> SortedPlayerList = new List<DeathmatchLogicScript.DeathmatchPlayer>();

    private Rect BlackBoxLogRect;
    private Rect KillLogRect;
    private Rect LogRect;

    public Texture2D Logo;
    public Texture2D Background;

    public Texture2D HealthTexture;
    public Texture2D OverHealthTexture;
    public Texture2D DamageIndicatorTexture;

    public Texture2D CoolingTexture;
    public Texture2D ReloadTexture;
    public Texture2D OverheatTexture;

    void OnEnable()
    {
        StartCoroutine("UpdatePlayerList");
        StartCoroutine("UpdateVisiblePlayerList");
        StartCoroutine("UpdateCameraList");

        StartCoroutine("UpdatePerk");

        DontDestroyOnLoad(this);
    }

    void OnDisable()
    {
        StopCoroutine("UpdatePlayerList");
        StopCoroutine("UpdateVisiblePlayerList");
        StopCoroutine("UpdateCameraList");

        StopCoroutine("UpdatePerk");
    }

    void Awake()
    {
        DeathMatchScoresRect = new Rect(Screen.width / 2 - 250, Screen.height / 2 - 200, 500, 400);
        BlackBoxLogRect = new Rect(Screen.width / 2 - 30, Screen.height / 2 + 20, 60, 20);
        KillLogRect = new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 20);
        LogRect = new Rect(20, Screen.height - 120, 500, 100);
    }

    void OnGUI()
    {
		GUI.skin = Skin;
		
        //UpdateFETest();
        UpdateBlackBoxMessageWindow();
        UpdateKillMessageWindow();
        UpdateLogMessageWindow();
		
        if (ThisPlayer)
        {
            UpdateRespawnMessage(ThisPlayer);
            UpdateMovementDebug(ThisPlayer);
            UpdateRotationDebug(ThisPlayer);
            UpdateNameDebug(ThisPlayer);
            UpdateDeathmatchLogicDebug(ThisPlayer);
            UpdateWeapons(ThisPlayer);
            UpdatePerkDebug(ThisPlayer);

            UpdateHealthDebug(ThisPlayer);
        }
    }

    void UpdateRespawnMessage(GameObject ThisPlayer)
    {
        HealthScript Health = ThisPlayer.GetComponent<HealthScript>();
        if (Health && Health.IsAlive())
        {
            return;
        }

        DeathScript Death = ThisPlayer.GetComponent<DeathScript>();
        if (!Death)
            return;

        float RespawnTime = Death.GetDeathTime();

        ShipCustomisationScript Custom = FindObjectOfType(typeof(ShipCustomisationScript)) as ShipCustomisationScript;

        if (RespawnTime > 0.0f)
            GUI.Label(new Rect((Screen.width / 2) - 150, (Screen.height / 2) + 80, 300, 40), "You can RESPAWN in : " + RespawnTime + " SECONDS ");
        else
        {
            if (!Custom.IsShowingCusomisation())
            {
                GUI.Label(new Rect((Screen.width / 2) - 75, (Screen.height / 2) + 80, 250, 40), "FIRE to RESPAWN");
            }
        }

        if (!Custom.IsShowingCusomisation())
        {
            GUI.Label(new Rect((Screen.width / 2) - 75, (Screen.height / 2) + 120, 250, 40), "Y to CUSTOMISE");
        }
    }

    void UpdateMovementDebug(GameObject ThisPlayer)
    {
        bool RenderDebug = false;
        if (!RenderDebug)
            return;

        int YPos = 20;
        Movement MovementComponent = ThisPlayer.GetComponent<Movement>();
        if (!MovementComponent)
            return;

        foreach (KeyValuePair<int, Vector3> KVP in MovementComponent.PosHistory)
        {
            GUI.Label(new Rect(0, YPos, 600, 20), "Server KEY : " + KVP.Key + " POS X : " + KVP.Value.x + " Y : " + KVP.Value.y + " Z : " + KVP.Value.z);
            YPos += 25;
        }

        GUI.Label(new Rect(0, 20, 100, 20), "Horizontal : " + MovementComponent.BufferedInput.x);
        GUI.Label(new Rect(0, 40, 100, 20), "Vertical : " + MovementComponent.BufferedInput.y);
        GUI.Label(new Rect(0, 60, 100, 20), "At : " + MovementComponent.BufferedInput.z);
    }

    void UpdateRotationDebug(GameObject ThisPlayer)
    {
        bool RenderDebug = false;
        if (!RenderDebug)
            return;

        Rotation RotationComponent = ThisPlayer.GetComponent<Rotation>();
        if (!RotationComponent)
            return;

        // We are not the owner of this ship, do not process the update.
        int YPos = 20;
        foreach (KeyValuePair<int, Quaternion> KVP in RotationComponent.RotHistory)
        {
            GUI.Label(new Rect(0, YPos, 600, 20), "Server KEY : " + KVP.Key + " POS X : " + KVP.Value.x + " Y : " + KVP.Value.y + " Z : " + KVP.Value.z + " Z : " + KVP.Value.z);
            YPos += 25;
        }

        GUI.Label(new Rect(0, 80, 100, 20), "Rotate X : " + RotationComponent.BufferedInput.x);
        GUI.Label(new Rect(0, 100, 100, 20), "Rotate Y : " + RotationComponent.BufferedInput.y);
        GUI.Label(new Rect(0, 120, 100, 20), "Rotate Z : " + RotationComponent.BufferedInput.z);
    }

    void UpdateHealthDebug(GameObject ThisPlayer)
    {
        if (!ThisPlayer)
            return;

        if (!ThisCamera)
            return;

        HealthScript HealthComponent = ThisPlayer.GetComponent<HealthScript>();
        if (!HealthComponent)
            return;

        float CurrentHealth = HealthComponent.CurrentHealth;
        float TotalHealth = HealthComponent.GetModifiedTotalHealth();
        float HitFadeTime = HealthComponent.HitFadeTime;

        // Health Bar
        GUI.Box(new Rect(Screen.width / 2 - 75, 40, 150, 20), "");

        float fHealthRatio = CurrentHealth / TotalHealth;
		float fOverHealthRatio = fHealthRatio - 1.0f;
		fHealthRatio = Mathf.Min(fHealthRatio, 1.0f);
		
        GUI.DrawTexture(new Rect(Screen.width / 2 - 73, 42, 146 * fHealthRatio, 16), HealthTexture);
		if(fOverHealthRatio > 0.0f)
	        GUI.DrawTexture(new Rect(Screen.width / 2 - 73, 42, 146 * fOverHealthRatio, 16), OverHealthTexture);

        Color PrevColour = GUI.color;
        Vector3 Direction = Vector3.zero;
        Vector3 PrevDirection = Vector3.up;
        List<HealthScript.DamageIndicator> DamageIndicators = HealthComponent.GetIndicators();
        for (int n = DamageIndicators.Count - 1; n >= 0; n--)
        {
            DamageIndicators[n].ElapsedFadeTime += Time.deltaTime;

            if (DamageIndicators[n].ElapsedFadeTime > HitFadeTime || !DamageIndicators[n].InstigatorLoc)
            {
                DamageIndicators.RemoveAt(n);
                continue;
            }

            Direction = DamageIndicators[n].InstigatorLoc.position - ThisPlayer.transform.position;
            Direction.Normalize();

            // Check this is on screen.
            float fDot = Vector3.Dot(ThisPlayer.transform.forward, Direction);
            if (fDot > 0.76f)
                continue;

            Direction = ThisPlayer.transform.root.InverseTransformDirection(Direction);

            // Ignore the Z
            Direction.z = 0.0f;

            // Re-Normalize, since we've just ignored the Z.
            Direction.Normalize();

            float RotAngle = Vector3.Angle(PrevDirection, Direction);
            PrevDirection = Direction;

            if (Direction.x < 0.0f)
                RotAngle *= -1.0f;

            GUIUtility.RotateAroundPivot(RotAngle, new Vector2(Screen.width / 2, Screen.height / 2));

            GUI.color = new Color(PrevColour.r, PrevColour.g, PrevColour.b, (HitFadeTime - DamageIndicators[n].ElapsedFadeTime) / HitFadeTime);

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), DamageIndicatorTexture, ScaleMode.ScaleToFit, true, Screen.width / Screen.height);
        }

        GUI.color = PrevColour;
    }
    
    void UpdatePerkDebug(GameObject ThisPlayer)
    {
        if (!Perk)
            return;

        RegenerationScript Regen = Perk.GetComponent<RegenerationScript>();
        if (!Regen)
            return;

        float ElapsedCooldownTime = Regen.GetElapsedCooldown();
        bool HasDepleted = !Regen.HasRegenerated();
        float fReloadPercent = ElapsedCooldownTime / 1.0f;

        fReloadPercent = Mathf.Min(fReloadPercent, 1.0f);
        fReloadPercent = Mathf.Max(0.0f, fReloadPercent);

        GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height - 100, 200, 50), "");
        if (HasDepleted)
            GUI.DrawTexture(new Rect(Screen.width / 2 - 95, Screen.height - 95, (190 * fReloadPercent), 40), CoolingTexture);
        else
            GUI.DrawTexture(new Rect(Screen.width / 2 - 95, Screen.height - 95, (190 * fReloadPercent), 40), ReloadTexture);


        GUI.Label(new Rect(Screen.width / 2 - 95, Screen.height - 85, (190), 40), Perk.name);
    }

    void UpdateNameDebug(GameObject ThisPlayer)
    {
        if (!ThisCamera)
            return;

        foreach (GameObject Player in VisiblePlayers)
        {
        	if(!Player)
        		continue;
        	
            if (!Player.GetComponent<HealthScript>())
                continue;

            if (!Player.GetComponent<HealthScript>().IsAlive())
                continue;

            Vector3 PlayerPos = Player.transform.position;
            PlayerPos.y += 3.0f;

            Vector3 ScreenPos = ThisCamera.WorldToScreenPoint(PlayerPos);

            DeathmatchLogicScript GameLogic = GameObject.FindObjectOfType(typeof(DeathmatchLogicScript)) as DeathmatchLogicScript;
            string Class = "";
            switch (GameLogic.GetClass(Player.GetComponent<NetOwnership>().GetOwner()))
            {
                case 0:
                    Class = "(Scout) ";
                    break;
                case 1:
                    Class = "(Medium) ";
                    break;
                case 2:
                    Class = "(Heavy) ";
                    break;
            }

            float CharSize = 10.0f;
            float Width = CharSize * Player.name.Length;

            GUI.Label(new Rect(ScreenPos.x - Width / 2.0f, Screen.height - (ScreenPos.y), Width, 50), Class + Player.name);
        }
    }

    void UpdateDeathmatchLogicDebug(GameObject ThisPlayer)
    {
        if (DeathMatchShowScores)
        {
            int YOffset = 10;
            int YStep = 20;

            GUI.BeginGroup(DeathMatchScoresRect);

            GUI.Box(new Rect(0, 0, DeathMatchScoresRect.width, DeathMatchScoresRect.height), "Deathmatch Leaderboard");
            YOffset += YStep;

            GUI.Label(new Rect(10, YOffset, 190, 30), "Name");
            GUI.Label(new Rect(200, YOffset, 80, 30), "Kills");
            GUI.Label(new Rect(300, YOffset, 80, 30), "Deaths");
            GUI.Label(new Rect(400, YOffset, 80, 30), "€");
            YOffset += YStep;

            foreach (DeathmatchLogicScript.DeathmatchPlayer Player in SortedPlayerList)
            {
                YOffset += YStep;

                Color thisColor = GUI.color;
                GUI.color = Player.PrimaryColour;

                string Class = "";
                switch (Player.Class)
                {
                    case 0:
                        Class = "(Scout) ";
                        break;
                    case 1:
                        Class = "(Medium) ";
                        break;
                    case 2:
                        Class = "(Heavy) ";
                        break;
                }

                GUI.Label(new Rect(10, YOffset, 190, 30), Class + Player.PlayerName + (Player.NetPlayer == Network.player ? " (YOU)" : ""));

                GUI.color = Player.SecondaryColour;

                GUI.Label(new Rect(200, YOffset, 80, 30), "" + Player.Kills);
                GUI.Label(new Rect(300, YOffset, 80, 30), "" + Player.Deaths);
                GUI.Label(new Rect(400, YOffset, 80, 30), "" + Player.Cash);

                GUI.color = thisColor;
            }

            GUI.EndGroup();
        }

        foreach (DeathmatchLogicScript.DeathmatchPlayer Player in SortedPlayerList)
        {
            if (Player.NetPlayer == Network.player)
                GUI.Label(new Rect(Screen.width - 80, Screen.height - 80, 80, 60), "€" + Player.Cash);
        }
    }

    void UpdateWeapons(GameObject ThisPlayer)
    {
    	Gun[] Guns = ThisPlayer.GetComponentsInChildren<Gun>();
    	for(int n = 0; n < Guns.Length; n++)
    	{
            Vector3 Dir = Vector3.Normalize(Guns[n].transform.root.position - Guns[n].transform.position);
            Vector3 ScreenPos = new Vector3();
            if (Vector3.Dot(Guns[n].transform.root.right, Dir) > 0.0f)
                ScreenPos.x = 80;
            else
                ScreenPos.x = Screen.width - 130;

            ScreenPos.y = Screen.height - 160;
            
            // Get Regen Component
            RegenerationScript Regen = Guns[n].GetComponent<RegenerationScript>();
            if(!Regen)
            	continue;

            float ElapsedCooldownTime = Regen.GetElapsedCooldown();
            float OverheatAudioWarnRate = Regen.GetRegenerationWarning();
            bool HasDepleted = !Regen.HasRegenerated();
            float fReloadPercent = 1.0f - (ElapsedCooldownTime / 1.0f);

            fReloadPercent = Mathf.Min(fReloadPercent, 1.0f);
            fReloadPercent = Mathf.Max(0.0f, fReloadPercent);

            GUI.Box(new Rect(ScreenPos.x, ScreenPos.y, 50, 140), "");
            if (HasDepleted)
                GUI.DrawTexture(new Rect(ScreenPos.x + 5, ScreenPos.y + 135, 40, -(130 * fReloadPercent)), CoolingTexture);
            else if (fReloadPercent > OverheatAudioWarnRate)
                GUI.DrawTexture(new Rect(ScreenPos.x + 5, ScreenPos.y + 135, 40, -(130 * fReloadPercent)), OverheatTexture);
            else
                GUI.DrawTexture(new Rect(ScreenPos.x + 5, ScreenPos.y + 135, 40, -(130 * fReloadPercent)), ReloadTexture);
    	}
    }

    void UpdateBlackBoxMessageWindow()
    {
        BlackBoxMessageWindowScript BlackBoxLog = GameObject.FindObjectOfType(typeof(BlackBoxMessageWindowScript)) as BlackBoxMessageWindowScript;
        List<LogWindowScript.LogWindowMsg> LogMessages = BlackBoxLog.GetMessages();
        if (LogMessages.Count != 0)
        {
            GUI.BeginGroup(BlackBoxLogRect);
            GUI.Box(new Rect(0, 0, BlackBoxLogRect.width, BlackBoxLogRect.height), "");

            int nXPos = 10;
            int nYPos = 0;
            int nYStep = 25;
            foreach (LogWindowScript.LogWindowMsg Msg in LogMessages)
            {
                GUI.Label(new Rect(nXPos, nYPos, Screen.width, 30), Msg.Message);
                nYPos -= nYStep;
            }

            GUI.EndGroup();
        }
    }

    void UpdateKillMessageWindow()
    {
        KillMessageWindowScript KillLog = GameObject.FindObjectOfType(typeof(KillMessageWindowScript)) as KillMessageWindowScript;
        List<LogWindowScript.LogWindowMsg> LogMessages = KillLog.GetMessages();
        if (LogMessages.Count != 0)
        {
            GUI.BeginGroup(KillLogRect);
            GUI.Box(new Rect(0, 0, KillLogRect.width, KillLogRect.height), "");

            int nXPos = 10;
            int nYPos = 0;
            int nYStep = 25;
            foreach (LogWindowScript.LogWindowMsg Msg in LogMessages)
            {
                GUI.Label(new Rect(nXPos, nYPos, Screen.width, 30), Msg.Message);
                nYPos -= nYStep;
            }

            GUI.EndGroup();
        }
    }

    void UpdateLogMessageWindow()
    {
        LogWindowScript LogWindow = GameObject.FindObjectOfType(typeof(LogWindowScript)) as LogWindowScript;
        List<LogWindowScript.LogWindowMsg> LogMessages = LogWindow.GetMessages();
        if (LogMessages.Count != 0)
        {
            GUI.BeginGroup(LogRect);
            GUI.Box(new Rect(0, 0, LogRect.width, LogRect.height), "Log");

            int nXPos = 25;
            int nYPos = (int)LogRect.height - 30;
            int nYStep = 25;
            foreach (LogWindowScript.LogWindowMsg Msg in LogMessages)
            {
                GUI.Label(new Rect(nXPos, nYPos, Screen.width, 30), Msg.Message);
                nYPos -= nYStep;
            }

            GUI.EndGroup();
        }
    }

    void UpdateFETest()
    {
        FrontEndTestScript GameLogic = GameObject.FindObjectOfType(typeof(FrontEndTestScript)) as FrontEndTestScript;
        if (GameLogic)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);
            GUI.DrawTexture(new Rect(Screen.width / 2 - Logo.width / 2, 0, Logo.width, Logo.height), Logo);
        }
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetButton("ShowScores"))
        {
            DeathMatchShowScores = true;

            BaseGameLogic GameLogic = GameObject.FindObjectOfType(typeof(BaseGameLogic)) as BaseGameLogic;
            if (GameLogic)
            {
                List<BaseGameLogic.GamePlayer> PlayerList = GameLogic.GetPlayerList();
                
                SortedPlayerList.Clear();
                foreach(BaseGameLogic.GamePlayer Player in PlayerList)
                {
                    if (Player.GetType() == typeof(DeathmatchLogicScript.DeathmatchPlayer))
                	    SortedPlayerList.Add((DeathmatchLogicScript.DeathmatchPlayer)Player);
                }
                
                SortedPlayerList.Sort(new DeathmatchLogicScript.DeathmatchPlayer.DeathmatchPlayerCompare());
            }
        }
        else
        {
            DeathMatchShowScores = false;
        }
	}

    IEnumerator UpdatePlayerList()
    {
        while (true)
        {
            Players.Clear();

            GameObject[] Targets;
            Targets = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject Target in Targets)
            {
                if(Target.GetComponent<NetOwnership>() && Target.GetComponent<NetOwnership>().GetOwner() == Network.player)
                {
                    ThisPlayer = Target;
                }

                Players.Add(Target);
            }

            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator UpdateVisiblePlayerList()
    {
        while (true)
        {
            if (ThisPlayer == null || ThisCamera == null)
            {
                yield return new WaitForSeconds(1.0f);
                continue;
            }

            VisiblePlayers.Clear();

            Camera ConversionCamera = ThisCamera;

            GameObject[] Targets;
            Targets = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject Target in Targets)
            {
                // Don't test against ourself
                if (Target == ThisPlayer.transform.gameObject)
                    continue;

                Vector3 TargetDir = Target.transform.position - ThisPlayer.transform.position;
                float Dist = TargetDir.magnitude;
                TargetDir.Normalize();
                Vector3 ScreenPos = ConversionCamera.WorldToScreenPoint(Target.transform.position);

                // Check this is on screen.
                if (ScreenPos.x < 0 ||
                    ScreenPos.y < 0 ||
                    ScreenPos.x > ConversionCamera.pixelWidth ||
                    ScreenPos.y > ConversionCamera.pixelHeight)
                    continue;

                // Ensure the object is in front of us
                if (Vector3.Dot(ThisPlayer.transform.forward, TargetDir) < 0)
                    continue;

                // only accept this target if it is visible.
                RaycastHit Hit;
                int LayerMask = (1 << 8) | (1 << 10);
                LayerMask = ~LayerMask;
                if (Physics.Raycast(ThisPlayer.transform.position, TargetDir, out Hit, Dist, LayerMask))
                {
                    continue;
                }

                VisiblePlayers.Add(Target);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator UpdateCameraList()
    {
        while (true)
        {
            Cameras.Clear();

            Camera[] AllCameras;
            AllCameras = FindObjectsOfType(typeof(Camera)) as Camera[];
            foreach (Camera go in AllCameras)
            {
                if (go.enabled)
                {
                    ThisCamera = go;
                }

                Cameras.Add(go);
            }

            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator UpdatePerk()
    {
        while (true)
        {
            PerkScript[] AllPerks;
            AllPerks = FindObjectsOfType(typeof(PerkScript)) as PerkScript[];
            foreach (PerkScript go in AllPerks)
            {
                if (go.GetOwner() && go.GetOwner().GetComponent<NetOwnership>() && 
                    go.GetOwner().GetComponent<NetOwnership>().GetOwner() == Network.player)
                {
                    Perk = go;
                }
            }

            yield return new WaitForSeconds(2.0f);
        }
    }
}
