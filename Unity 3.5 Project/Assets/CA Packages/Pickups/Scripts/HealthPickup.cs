using UnityEngine;
using System.Collections;

public class HealthPickup : PickupScript
{
    public float HealthToRegen = 100.0f;

    public override void DoPickup()
    {
        NetworkViewID Instigator = GetInstigator();

        if (Instigator == NetworkViewID.unassigned)
            return;

        NetworkView InstigatorView = NetworkView.Find(Instigator);
        if (!InstigatorView)
            return;

        HealthScript Health = InstigatorView.GetComponent<HealthScript>();
        if (Health)
        {
            Debug.Log("Pickup Restoring health");
            if (Health.IsAlive())
                Health.RestoreHealth(HealthToRegen, true);
        }

        base.DoPickup();
    }
}
