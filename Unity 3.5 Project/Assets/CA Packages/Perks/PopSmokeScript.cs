using UnityEngine;
using System.Collections;

public class PopSmokeScript : PerkScript
{
    public GameObject DecoyToSpawn;

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

        GameObject go = null;
        if (DecoyToSpawn)
            go = Network.Instantiate(DecoyToSpawn, SpawnPosition, Quaternion.identity, 0) as GameObject;

        if(GetOwner().GetComponent<DecoyScript>())
        {
            GetOwner().GetComponent<DecoyScript>().SetDecoy(go.transform);
        }
    }

}
