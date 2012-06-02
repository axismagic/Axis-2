using UnityEngine;
using System.Collections;

public class ShipCustomisationScript : MonoBehaviour
{
    public GUISkin Skin;
    private Rect CharacterCustomisationRect;

    public int ClassSelGridInt = 1;
    public string[] ClassSelStrings = new string[] { "Scout", "Medium", "Heavy" };

    public int PerkSelGridInt = 1;
    public string[] PerkSelStrings = new string[] { "Decoy Black Box", "Pop Smoke", "Afterburner" };

    public int LeftWeaponSelGridInt = 2;
    public int RightWeaponSelGridInt = 0;
    public string[] WeaponselStrings = new string[] { "Rocket Launcher Auto", "RocketLauncher Manual", "Mach Gun Auto", "Mach Gun Manual" };

    public Color PrimaryColor = Color.red;
    public Color SecondaryColor = Color.blue;

    public float Sensitivity = 0.5f;
    public bool InvertY = false;

    AuthServerSpawnPointContainer Spawner;
    DeathmatchLogicScript Logic;

    private GameObject ThisPlayer = null;

    bool ShowCustom = true;

    void Awake()
    {
        Logic = FindObjectOfType(typeof(DeathmatchLogicScript)) as DeathmatchLogicScript;
    }

    void OnEnable()
    {
        ShowCustom = true;
        StartCoroutine("UpdatePlayerList");
    }

    void OnDisable()
    {
        StopCoroutine("UpdatePlayerList");
    }

    public bool IsShowingCusomisation()
    {
        return ShowCustom;
    }

    public void ShowCustomisation(bool Show)
    {
        ShowCustom = Show;
    }

    // Use this for initialization
    void Start()
    {
        CharacterCustomisationRect = new Rect(Screen.width / 2 - 250, 25, 100, 300);

        Spawner = FindObjectOfType(typeof(AuthServerSpawnPointContainer)) as AuthServerSpawnPointContainer;
    }

    void OnGUI()
    {
        if (!IsShowingCusomisation())
            return;

        GUI.skin = Skin;

        if (Network.peerType == NetworkPeerType.Server || Network.peerType == NetworkPeerType.Client)
        {
            bool IsAlive = ThisPlayer && ThisPlayer.GetComponent<HealthScript>() && ThisPlayer.GetComponent<HealthScript>().IsAlive();

            if (!ThisPlayer || !IsAlive)
                CharacterCustomisationRect = GUILayout.Window(0, CharacterCustomisationRect, MakeCustomisationWindow, "CUSTOMISE");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeCustomisationWindow(int ID)
    {
        Screen.lockCursor = false;
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Class");
        ClassSelGridInt = GUILayout.SelectionGrid(ClassSelGridInt, ClassSelStrings, ClassSelStrings.Length, GUILayout.Width(240));
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Left Weapon");
        GUILayout.Label("Right Weapon");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.BeginHorizontal();
        LeftWeaponSelGridInt = GUILayout.SelectionGrid(LeftWeaponSelGridInt, WeaponselStrings, 2);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        RightWeaponSelGridInt = GUILayout.SelectionGrid(RightWeaponSelGridInt, WeaponselStrings, 2);
        GUILayout.EndHorizontal();
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Perks");
        PerkSelGridInt = GUILayout.SelectionGrid(PerkSelGridInt, PerkSelStrings, 2, GUILayout.Width(240));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Perks (Coming SOON)");
        GUILayout.Label("Invisibility", GUILayout.Width(110));
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        Color thisColor = GUI.color;
        GUILayout.BeginHorizontal();
        GUI.color = PrimaryColor;
        GUILayout.Label("Primary Colour (RGB)");
        PrimaryColor.r = GUILayout.HorizontalSlider(PrimaryColor.r, 0.0f, 1.0f, GUILayout.Width(75));
        PrimaryColor.g = GUILayout.HorizontalSlider(PrimaryColor.g, 0.0f, 1.0f, GUILayout.Width(75));
        PrimaryColor.b = GUILayout.HorizontalSlider(PrimaryColor.b, 0.0f, 1.0f, GUILayout.Width(75));
        GUI.color = thisColor;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUI.color = SecondaryColor;
        GUILayout.Label("Secondary Colour (RGB)");
        SecondaryColor.r = GUILayout.HorizontalSlider(SecondaryColor.r, 0.0f, 1.0f, GUILayout.Width(75));
        SecondaryColor.g = GUILayout.HorizontalSlider(SecondaryColor.g, 0.0f, 1.0f, GUILayout.Width(75));
        SecondaryColor.b = GUILayout.HorizontalSlider(SecondaryColor.b, 0.0f, 1.0f, GUILayout.Width(75));
        GUI.color = thisColor;
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("Sensitivity");
        Sensitivity = GUILayout.HorizontalSlider(Sensitivity, 0.0f, 1.0f, GUILayout.Width(240));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Invert Y");
        InvertY = GUILayout.Toggle(InvertY, "", GUILayout.Width(240));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        bool CanGo = false;
        if (!ThisPlayer)
            CanGo = true;

        if (ThisPlayer)
        {
            DeathScript Death = ThisPlayer.GetComponent<DeathScript>();
            CanGo = Death ? Death.GetDeathTime() < 0.01f : false;
        }

        if (CanGo)
        {
            if (GUILayout.Button("GO! ->"))
            {
                DoSpawn(true);
            }
        }
        else
        {
            GUILayout.Label("GO! ->");
        }

        GUILayout.EndHorizontal();
    }

    public void DoSpawn(bool DoCustomisation)
    {
        if (Spawner)
            Spawner.DoSpawn(Network.player);

        if (DoCustomisation)
        {
            networkView.RPC("SetCustomisationData", RPCMode.AllBuffered, Network.player, ClassSelGridInt, PerkSelGridInt, LeftWeaponSelGridInt, RightWeaponSelGridInt,
                PrimaryColor.r, PrimaryColor.g, PrimaryColor.b,
                SecondaryColor.r, SecondaryColor.g, SecondaryColor.b);
        }
        ShowCustom = false;
    }

    [RPC]
    void SetCustomisationData(NetworkPlayer NetPlayer, int Class, int Perk, int LeftWeapon, int RightWeapon,
        float PrimaryColourR, float PrimaryColourG, float PrimaryColourB,
        float SecondaryColourR, float SecondaryColourG, float SecondaryColourB)
    {
        Debug.Log("SetCustomisationBuffer");
        if (Logic)
            Logic.SetCustomisationData(NetPlayer, Class, Perk, LeftWeapon, RightWeapon,
                PrimaryColourR, PrimaryColourG, PrimaryColourB,
                SecondaryColourR, SecondaryColourG, SecondaryColourB);
    }

    IEnumerator UpdatePlayerList()
    {
        while (true)
        {
            GameObject[] Targets;
            Targets = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject Target in Targets)
            {
                if (Target.GetComponent<NetOwnership>() && Target.GetComponent<NetOwnership>().GetOwner() == Network.player)
                {
                    ThisPlayer = Target;
                }
            }

            if (ThisPlayer)
                yield return new WaitForSeconds(2.0f);
            else
                yield return new WaitForSeconds(0.1f);
        }
    }
}
