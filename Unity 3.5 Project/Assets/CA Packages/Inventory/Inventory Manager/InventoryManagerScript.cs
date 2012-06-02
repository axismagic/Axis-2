using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManagerScript : MonoBehaviour {
    private List<Transform> WeaponAttachments = new List<Transform>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Remove(NetworkView netView)
    {
        Debug.Log("Removing Item");
        for (int n = WeaponAttachments.Count - 1; n >= 0; n--)
        {
            if (WeaponAttachments[n].networkView == netView)
                WeaponAttachments.RemoveAt(n);
        }
    }

    public void ClearWeapons()
    {
        WeaponAttachments.Clear();
    }

    public void AddWeapon(Transform Weapon)
    {
        Debug.Log("Adding Weapon");
        WeaponAttachments.Add(Weapon);
    }
	
	public void StartFire(int Weapon) {
		if(Weapon < WeaponAttachments.Count) {
			Gun ThisGun = WeaponAttachments[Weapon].GetComponentInChildren<Gun>();
            if (ThisGun)
            {
                // Check to see if we are currently firing, before starting to fire.
                if(!ThisGun.IsFiring())
                    ThisGun.ClientStartFire();
            }
    	}
	}
	
	public void StopFire(int Weapon) {
		if(Weapon < WeaponAttachments.Count) {
			Gun ThisGun = WeaponAttachments[Weapon].GetComponentInChildren<Gun>();
            if (ThisGun)
            {
                // Check to see if we are currently firing, before starting to fire.
                if(ThisGun.IsFiring())
                    ThisGun.ClientStopFire();
            }
    	}
	}
	
	public void SetBestTarget(Transform Target) {
		foreach (Transform Attachment in WeaponAttachments) {
            LockOnScript WeaponObj = Attachment.GetComponentInChildren<LockOnScript>();
            if (WeaponObj)
                WeaponObj.SetLockedTargetObject(Target);
		}
	}
}
