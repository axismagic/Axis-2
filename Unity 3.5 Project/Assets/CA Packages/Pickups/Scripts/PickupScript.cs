using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupScript : MonoBehaviour
{
    List<NetworkViewID> ViewIDs = new List<NetworkViewID>();

    public float Lifetime = -1.0f;
    private float CurrentLifetime = 0.0f;

    public AudioPlaybackArray PickupSound;
	
	void OnDestroy() {
		Network.RemoveRPCs(networkView.viewID);
	}
	
	void OnNetworkInstantiate(NetworkMessageInfo info) {
        Debug.Log("PickupScript OnNetworkInstantiate hdhdhdgdghsds");
        Network.RemoveRPCs(networkView.viewID);
    }

    public NetworkViewID GetInstigator()
    {
        if (ViewIDs.Count == 0)
            return NetworkViewID.unassigned;

        return ViewIDs[0];
    }

	// Update is called once per frame
    public virtual void Update() 
    {
        // Only update this on the server.
        if (!Network.isServer)
            return;

        if(Lifetime > 0.0f)
        {
            CurrentLifetime += Time.deltaTime;
            if (CurrentLifetime > Lifetime)
            {
                Network.RemoveRPCs(networkView.viewID);
                Network.Destroy(networkView.viewID);
            }
        }

        HealthScript Health = GetComponent<HealthScript>();
        if (Health && !Health.IsAlive())
        {
            Kill();
        }

        if (ViewIDs.Count == 0)
            return;

        // Check if we have a Regen Component if it's ok to pickup this item.
        if (GetComponent<RegenerationScript>())
        {
            // We haven't regenerated, return.
            if (!GetComponent<RegenerationScript>().HasRegenerated())
            {
                return;
            }
        }

        DoPickup();
	}

    public virtual void Kill()
    {
        Debug.Log("Killing Pickup");
        // Destroy this.
        Network.RemoveRPCs(networkView.viewID);
        Network.Destroy(networkView.viewID);
    }

    public virtual void DoPickup()
    {
        Debug.Log("A pickup has happened");

        // Tell the Regeneration Script we've been used.
        if (GetComponent<RegenerationScript>())
        {
            GetComponent<RegenerationScript>().Use();
        }

        if (networkView)
            networkView.RPC("ClientDoPickup", RPCMode.All, GetInstigator());
    }

    void OnTriggerEnter(Collider other)
    {
        if (!Network.isServer)
            return;

        if (!other.gameObject)
            return;

        if (!other.gameObject.networkView)
            return;

        HealthScript Health = other.gameObject.GetComponent<HealthScript>();
        if (!Health)
            return;

        if (!Health.IsAlive())
            return;

        if (!other.GetComponentInChildren<NetOwnership>())
        {
            Debug.Log("Pickup Instigator is not a player");
            return;
        }

        Debug.Log("Player Entered Pickup");
        ViewIDs.Add(other.networkView.viewID);
    }

    void OnTriggerExit(Collider other)
    {
        if (!Network.isServer)
            return;

        if (!other.gameObject)
            return;

        if (!other.gameObject.networkView)
        {
            Debug.Log("Pickup Instigator has no network View");
            return;
        }

        HealthScript Health = other.gameObject.GetComponent<HealthScript>();
        if (!Health)
            return;

        if (!Health.IsAlive())
            return;

        if (!other.GetComponentInChildren<NetOwnership>())
        {
            Debug.Log("Pickup Instigator is not a player");
            return;
        }

        Debug.Log("Player Exit Pickup");
        ViewIDs.Remove(other.networkView.viewID);
    }

    [RPC]
    void ClientDoPickup(NetworkViewID Instigator)
    {
        if (PickupSound)
        {
            Debug.Log("Doing Sound");
            AudioPlaybackArray go = Instantiate(PickupSound, transform.position, transform.rotation) as AudioPlaybackArray;
            go.PlayAtPoint(transform.position);
        }
    }
}
