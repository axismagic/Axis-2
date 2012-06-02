using UnityEngine;
using System.Collections;

public class SpawningPickup : PickupScript
{
    public GameObject PrefabToSpawn;
    GameObject SpawnedPrefab = null;

    public float CooldownBeforePickup = 2.0f;
    float ElapsedCooldownBeforePickup = 0.0f;

    Transform Instigator = null;

    public void SetInstigator(Transform OurInstigator)
    {
        Instigator = OurInstigator;
    }

    public override void DoPickup()
    {
        if (!PrefabToSpawn)
            return;

        SpawnedPrefab = Network.Instantiate(PrefabToSpawn, transform.position, transform.rotation, 0) as GameObject;

        if (SpawnedPrefab && SpawnedPrefab.GetComponentInChildren<DamageScript>())
        {
            SpawnedPrefab.GetComponentInChildren<DamageScript>().SetInstigator(Instigator.gameObject);
        }

        base.DoPickup();

        // Destroy this.
        Network.RemoveRPCs(networkView.viewID);
        Network.Destroy(networkView.viewID);
    }

    public override void Kill()
    {
        Debug.Log("Killing Pickup");

        if (PrefabToSpawn)
        {
            GameObject go = Network.Instantiate(PrefabToSpawn, transform.position, transform.rotation, 0) as GameObject;

            if (go && go.GetComponent<DamageScript>())
                go.GetComponent<DamageScript>().SetInstigator(Instigator.gameObject);
        }

        // Destroy this.
        Network.RemoveRPCs(networkView.viewID);
        Network.Destroy(networkView.viewID);
    }

    public override void Update()
    {
        ElapsedCooldownBeforePickup += Time.deltaTime;
        if (ElapsedCooldownBeforePickup < CooldownBeforePickup)
            return;

        base.Update();
    }
}
