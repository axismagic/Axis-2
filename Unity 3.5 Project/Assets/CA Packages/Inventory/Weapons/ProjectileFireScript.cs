using UnityEngine;
using System.Collections;

public class ProjectileFireScript : MonoBehaviour {
	
	public GameObject FiringProjectile;
	
	public Transform[] SpawnPoints;
	
	public void InitFire()
	{
		
	}
	
	public void Fire()
	{
		// Use the parents forward, so we will shoot at the centre - (poop)
		Quaternion SpawnRotation = transform.rotation;
		
		if(SpawnPoints.Length == 0)
		{
			// Instantiate this, if we need to do something with it, it must be a Game object
        	GameObject go = Instantiate(FiringProjectile, transform.position, SpawnRotation) as GameObject;
        	if(go && go.GetComponent<ProjectileScript>())
        	{
        	    go.GetComponent<ProjectileScript>().SetLauncher(transform.root.gameObject);
        	}

			if(go) {
				ProjectileScript Projectile = go.GetComponent<ProjectileScript>();
				if(Projectile && transform.root.collider) {
					Projectile.SetIgnoreCollision(transform.root);
           		}
			}
		}

		for(int n = 0; n < SpawnPoints.Length; n++)
		{
			// Instantiate this, if we need to do something with it, it must be a Game object
        	GameObject go = Instantiate(FiringProjectile, SpawnPoints[n].position, SpawnRotation) as GameObject;
        	if(go && go.GetComponent<ProjectileScript>())
            {
        	    go.GetComponent<ProjectileScript>().SetLauncher(transform.root.gameObject);
        	}

			if(go) {
				ProjectileScript Projectile = go.GetComponent<ProjectileScript>();
				if(Projectile && transform.root.collider) {
					Projectile.SetIgnoreCollision(transform.root);
           		}
			}
		}

	}
	
}
