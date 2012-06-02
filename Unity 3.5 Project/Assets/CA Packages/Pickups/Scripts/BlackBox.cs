using UnityEngine;
using System.Collections;

public class BlackBox : MonoBehaviour {

    GameObject Owner = null;

    public GameObject BBPickupAudioPrefab;
 
    public float HealthToRegen = 100.0f;
    public int CashToAttain = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetOwner(GameObject TheOwner)
    {
        Owner = TheOwner;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!Network.isServer)
            return;

        if(!other.gameObject)
            return;

        if(!other.gameObject.networkView) {
            Debug.Log("Pickup Instigator has no network View");
            return;
        }

        HealthScript Health = other.gameObject.GetComponent<HealthScript>();
        if (!Health)
            return;

        if(!Health.IsAlive())
            return;

        if(!other.GetComponentInChildren<NetOwnership>()) {
            Debug.Log("Pickup Instigator is not a player");
            return;
        }

        PickupObject(other.networkView.viewID);
    }

    void PickupObject(NetworkViewID PickupInstigator)
    {
        NetworkViewID OwnerID = NetworkViewID.unassigned;
        if(Owner && Owner.networkView)
        {
            OwnerID = Owner.networkView.viewID;
        }

        DoServerPickup(PickupInstigator, OwnerID);
    }

    void DoServerPickup(NetworkViewID PickupInstigator, NetworkViewID PickupOwner)
    {
        NetworkView Instigator = NetworkView.Find(PickupInstigator);
        NetOwnership Ownership = Instigator.GetComponentInChildren<NetOwnership>();
        NetworkPlayer NetInstigator = Ownership.GetOwner();

        NetworkPlayer NewNetPlayer = new NetworkPlayer();
        if (PickupOwner != NetworkViewID.unassigned)
        {
            NetworkView Owner = NetworkView.Find(PickupOwner);
            Ownership = Owner.GetComponentInChildren<NetOwnership>();
            NewNetPlayer = Ownership.GetOwner();
        }

        HealthScript Health = Instigator.GetComponent<HealthScript>();
        if (Health)
        {
            Debug.Log("Restoring health");
            if(Health.IsAlive())
                Health.RestoreHealth(HealthToRegen, false);
        }

        if (BBPickupAudioPrefab)
        {
            Instantiate(BBPickupAudioPrefab, transform.position, transform.rotation);
        }

        foreach (DeathmatchLogicScript go in FindObjectsOfType(typeof(DeathmatchLogicScript)))
        {
            go.OnBlackBoxPickedup(NetInstigator, NewNetPlayer, CashToAttain);
        }

        Network.RemoveRPCs(GetComponent<NetworkView>().viewID);
        Network.Destroy(GetComponent<NetworkView>().viewID);
    }
}
