using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
[RequireComponent(typeof(RegenerationScript))]
public class PerkScript : MonoBehaviour {

    public AudioPlaybackArray PerkActivatedAudio;
    public AudioPlaybackArray PerkReadyAudio;
    
    public float Timeout = -1.0f;
    float CurrentTimeout = 0.0f;

	bool HasTimedOut = true;
    bool CanActivate = true;
    Transform Owner;

    public Transform GetOwner()
    {
        return Owner;
    }

    public void SetOwner(Transform thisOwner)
    {
        networkView.RPC("NetSetOwner", RPCMode.AllBuffered, thisOwner.networkView.viewID);
    }

	// Use this for initialization
	void Start () {
        CanActivate = true;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!Owner)
            return;

        NetOwnership Ownership = Owner.GetComponent<NetOwnership>();
        if (Ownership && Ownership.GetOwner() == Network.player)
            OwnerUpdate();

        if (!Network.isServer)
            return;
            
        if(!HasTimedOut)
        {
        	if(Timeout > 0.0f)
        	{
        		CurrentTimeout += Time.deltaTime;
        		if(CurrentTimeout > Timeout)
        			networkView.RPC("SendPerkTimeout", RPCMode.All);
        	}
        }

        if (CanActivate)
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

        // Tell Clients we can activate this perk
        networkView.RPC("SetCanActivate", RPCMode.All);
	}

    void OwnerUpdate()
    {
		// Check we are alive
		if(!GetOwner() || !GetOwner().GetComponent<HealthScript>() || !GetOwner().GetComponent<HealthScript>().IsAlive())
			return;
		
        // Tell everyone that we have activated this perk
        if (CanActivate && Input.GetButton("ActivatePerk"))
            networkView.RPC("ActivatePerk", RPCMode.All);
    }

    [RPC]
    void SetCanActivate()
    {
        CanActivate = true;
        CurrentTimeout = 0.0f;

        Debug.Log("Perk Ready");
        if (PerkReadyAudio)
        {
            AudioPlaybackArray go = Instantiate(PerkReadyAudio, transform.position, transform.rotation) as AudioPlaybackArray;
            go.PlayAtPoint(GetOwner().position);
        }
    }

    [RPC]
    public virtual void ActivatePerk()
    {
		Debug.Log("Perk Activated : " + name);
        CanActivate = false;

        if (PerkActivatedAudio)
        {
            AudioPlaybackArray go = Instantiate(PerkActivatedAudio, transform.position, transform.rotation) as AudioPlaybackArray;
            go.PlayAtPoint(GetOwner().position);
        }

        // Tell the Regeneration Script we've been used.
        if (GetComponent<RegenerationScript>())
        {
            GetComponent<RegenerationScript>().Use();
        }
        
        HasTimedOut = false;
    }
    
    [RPC]
    public virtual void SendPerkTimeout()
    {
    	HasTimedOut = true;
    }

    [RPC]
    void NetSetOwner(NetworkViewID OwnerViewID)
    {
        NetworkView NetViewOwner = NetworkView.Find(OwnerViewID);
        Owner = NetViewOwner.transform;
    }
}
