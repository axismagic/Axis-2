using UnityEngine;
using System.Collections;

public class DecoyBlackBoxScript : PerkScript {
    public GameObject DecoyToSpawn;

    private GameObject PreviousDecoy = null;

    void OnDestroy()
    {
        Debug.Log("Removing Old Black Box");
        if (PreviousDecoy)
        {
            Network.RemoveRPCs(PreviousDecoy.networkView.viewID);
            Network.Destroy(PreviousDecoy.networkView.viewID);
            PreviousDecoy = null;
        }
    }

    [RPC]
    public override void ActivatePerk()
    {
        if (!GetOwner())
            return;

        base.ActivatePerk();
		
		// If we are not the server, do nothing
		if(!Network.isServer)
			return;

        Vector3 SpawnPosition = GetOwner().transform.position + (GetOwner().transform.forward * -1.0f);

        if (PreviousDecoy)
        {
            Network.RemoveRPCs(PreviousDecoy.networkView.viewID);
            Network.Destroy(PreviousDecoy.networkView.viewID);
            PreviousDecoy = null;
        }

        if (DecoyToSpawn)
            PreviousDecoy = Network.Instantiate(DecoyToSpawn, SpawnPosition, Quaternion.identity, 0) as GameObject;
		
		Debug.Log(PreviousDecoy);

        networkView.RPC("SetInstigator", RPCMode.All, PreviousDecoy.networkView.viewID);
    }

    [RPC]
    void SetInstigator(NetworkViewID InstigatorID)
    {
        NetworkView netViewToParent = NetworkView.Find(InstigatorID);

        if (netViewToParent.GetComponent<SpawningPickup>())
        {
            netViewToParent.GetComponent<SpawningPickup>().SetInstigator(GetOwner());
        }
    }
}
