using UnityEngine;
using System.Collections;

public class HideHelperWeapon : MonoBehaviour
{
    public GameObject WeaponToSpawn0;
    public GameObject WeaponToSpawn1;

    public GameObject WeaponToReplace;
    
    GameObject SpawnedWeapon;
    
    public GameObject GetSpawnedWeapon()
    {
    	return SpawnedWeapon;
    }

	// Use this for initialization
	void Awake () {
        Destroy(WeaponToReplace);
    }

    public void SpawnCorrectWeapon(string Name)
    {
        if (Name.Contains("0") && WeaponToSpawn0)
        {
            SpawnedWeapon = Instantiate(WeaponToSpawn0, transform.position, transform.rotation) as GameObject;
            if (SpawnedWeapon)
                SpawnedWeapon.transform.parent = transform;
        }
        else if (Name.Contains("1") && WeaponToSpawn1)
		{	
			// Fix up the positions for our children.
        	foreach (Transform child in transform)
        	{
        	    RecurseChildren(child);
			}
			
            SpawnedWeapon = Instantiate(WeaponToSpawn1, transform.position, transform.rotation) as GameObject;
			
            if (SpawnedWeapon)
                SpawnedWeapon.transform.parent = transform;
        }
    }
	   
	void RecurseChildren(Transform parent)
    {
		// Flip the child X position.
		float XPos = parent.transform.localPosition.x;
		parent.transform.localPosition = new Vector3(XPos * -1.0f, parent.transform.localPosition.y, parent.transform.localPosition.z);
		
        foreach (Transform child in parent)
        {
            RecurseChildren(child);
		}
    }
}
