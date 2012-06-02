using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	public GameObject WeaponInventory;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
		InventoryManagerScript invMan = WeaponInventory.GetComponent<InventoryManagerScript>();
		if(!invMan)
			return;
			
		HealthScript Health = GetComponent<HealthScript>();
		if(Health && !Health.IsAlive()) {
			return;
		}

        bool HeldFireRight = Input.GetButton("FireRight") || Input.GetAxis("FireRight") > 0.1f;
        bool HeldFiringLeft = Input.GetButton("FireLeft") || Input.GetAxis("FireLeft") > 0.1f;
		
		// Check if we are firing, the boolean is to stop the server from being flooded with messages.
        if (HeldFireRight) 
        {
			invMan.StartFire(0);
		}
        else if (!HeldFireRight)
        {
			invMan.StopFire(0);
		}

        if (HeldFiringLeft) 
        {
			invMan.StartFire(1);
		}
        else if (!HeldFiringLeft)
        {
			invMan.StopFire(1);
		}
	}
}
