using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]
public class DeathScript : MonoBehaviour {

    public GameObject DeathPrefab;

    public GameObject DropPrefab;

    private float DeathTime = 0.0f;
    public float DeathTimer = 10.0f;

    public float GetDeathTime()
    {
        return DeathTime;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(DeathTime > 0.0f)
        {
            DeathTime -= Time.deltaTime;
        }
	}

    public void Kill(NetworkPlayer Instigator)
    {
        NetOwnership RecieverNetOwnership = gameObject.GetComponent<NetOwnership>();

        networkView.RPC("KillClient", RPCMode.All, Instigator, RecieverNetOwnership.GetOwner());

        if (DropPrefab)
        {
            GameObject go = Network.Instantiate(DropPrefab, transform.position, Quaternion.identity, 0) as GameObject;
            if (go && go.GetComponent<BlackBox>())
            {
                BlackBox BB = go.GetComponent<BlackBox>();
                BB.SetOwner(transform.gameObject);
            }
        }
	}
	
	[RPC]
    void KillClient(NetworkPlayer Killer, NetworkPlayer Victim)
    {
        InventoryManagerScript invMan = transform.root.GetComponentInChildren<InventoryManagerScript>();
        if (invMan)
        {
            invMan.StopFire(0);
            invMan.StopFire(1);
        }

        DeathTime = DeathTimer;

        // Notify everybody that we killed something
        foreach (DeathmatchLogicScript go in FindObjectsOfType(typeof(DeathmatchLogicScript)))
        {
            go.OnPlayerDied(Killer, Victim);
            go.OnPlayerKilled(Killer, Victim);
        }

        if (DeathPrefab)
        {
            GameObject go = Instantiate(DeathPrefab, transform.position, transform.rotation) as GameObject;
            if (go)
                go.transform.parent = transform;
        }

		if(rigidbody) {
		}
		
		gameObject.layer = 12;
		if(Killer == Network.player)
			Screen.lockCursor = false;

        Movement MovementScript = GetComponent<Movement>();
        if (MovementScript)
            MovementScript.StopSync();
        Rotation RotationScript = GetComponent<Rotation>();
        if (RotationScript)
            RotationScript.StopSync();

        AttachmentSystem Attachments = GetComponent<AttachmentSystem>();
        if (Attachments)
            Attachments.DetachAndApplyImpulse();
	}
}
