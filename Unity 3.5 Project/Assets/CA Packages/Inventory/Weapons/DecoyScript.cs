using UnityEngine;
using System.Collections;

public class DecoyScript : MonoBehaviour
{
    private Transform Decoy = null;

    public Transform GetDecoy()
    {
        return Decoy;
    }

    public void SetDecoy(Transform TheDecoy)
    {
        networkView.RPC("SendSetDecoy", RPCMode.All, TheDecoy.networkView.viewID);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!Decoy)
            return;

        if (!Decoy.GetComponent<HealthScript>())
        {
            Decoy = null;
            return;
        }

        if (!Decoy.GetComponent<HealthScript>().IsAlive())
        {
            Decoy = null;
            return;
        }
	}

    [RPC]
    void SendSetDecoy(NetworkViewID netViewID)
    {
        NetworkView netViewToParent = NetworkView.Find(netViewID);
        Decoy = netViewToParent.transform;
    }
}
