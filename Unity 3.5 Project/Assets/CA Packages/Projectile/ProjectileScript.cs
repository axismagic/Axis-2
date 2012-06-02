using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*****
*
*	Projectiles are fire and forget, we launch them forward.
*	If a projectile collides with something, we ignore the 
*	collision unless it happened on the server.
*
*	The server deals with all logic, it also deals with spawning
*	a projectile, destroying it, and inflicting any needed damage.
*
*****/

public class ProjectileScript : MonoBehaviour {
	private Vector3 LaunchDir = new Vector3(0.0f, 0.0f, 1.0f);
	private Vector3 LaunchPos;
	private bool Destroying = false;

    public GameObject WaterImpactParticleEffect;
    public GameObject PlayerImpactPrefab;
    public GameObject SoilImpactPrefab;

    public GameObject ProjectilePrefab;
	
	public float MaxDistanceSq = 100.0f;
	public float LaunchPower = 1.0f;
	public float LightFadeout = 4.0f;
	
	public int NumberOfBounces = 0;
	public float DamageReduction = 0.5f;
	public float LaunchPowerReduction = 0.5f;
	float CurrentNumberOfBounces = 0;

    private GameObject Launcher = null;
	
    void OnEnable()
    {
        StartCoroutine("UpdateRay");
    }

    void OnDisable()
    {
        StopCoroutine("UpdateRay");
    }

    public void SetLauncher(GameObject TheLauncher)
    {
        Launcher = TheLauncher; 

        DamageScript[] Damages = GetComponentsInChildren<DamageScript>() as DamageScript[];
        foreach (DamageScript Damage in Damages)
        {
            Damage.SetInstigator(Launcher);
        }
    }

	// Use this for initialization
	void Start () {
		LaunchPos = transform.position;

        // Spawn the prefab.
        if (ProjectilePrefab)
        {
            GameObject go = Instantiate(ProjectilePrefab, transform.position, transform.rotation) as GameObject;
            if (go)
                go.transform.parent = transform;
        }
	}
	
	// Update is called once per frame
	void Update () {		
		Vector3 LaunchTrans = LaunchDir * LaunchPower * Time.deltaTime;
		transform.Translate(LaunchTrans);
		
		float DistanceTraveledSq = (LaunchPos - transform.position).sqrMagnitude;
		
		if(DistanceTraveledSq > MaxDistanceSq)
			Remove();

        if(!Destroying && GetComponentInChildren<ProximityTrigger>() && GetComponentInChildren<ProximityTrigger>().GetObject())
            Explode(GetComponentInChildren<ProximityTrigger>().GetObject().collider);
	}
	
	void OnCollision(Collider other) {
		if(Destroying)
			return;
			
        if (other)
        {
            if (other.CompareTag("Water"))
            {
                SpawnWaterEffect();
                return;
            }

            if (other.CompareTag("Projectiles"))
                return;

            if (other.CompareTag("Explosion"))
                return;

            if (other.CompareTag("Components"))
                return;
        }
		
        Explode(other);
	}
	
	void Remove() {
        ParticleEmitter[] Emitters = gameObject.GetComponentsInChildren<ParticleEmitter>();

        LaunchPower = 0.0f;
		Destroying = true;
			
		foreach(ParticleEmitter Emitter in Emitters) { 
			Emitter.emit = false;
        	Emitter.transform.parent = null;
		}
        
        Object.Destroy(gameObject);
	}
	
	void SpawnWaterEffect() {
		if(WaterImpactParticleEffect) {
			Instantiate(WaterImpactParticleEffect, transform.position, transform.rotation);
		}
	}
	
	public void SetLaunchDirection(Vector3 vLaunchDir) {
		LaunchDir = vLaunchDir;
	}
	
	public void SetIgnoreCollision(Transform toIgnore) {
		Physics.IgnoreCollision(collider, toIgnore.collider);

        ProximityTrigger Proximity = GetComponentInChildren<ProximityTrigger>();
        if (Proximity && transform.root.collider)
        {
            Physics.IgnoreCollision(Proximity.collider, toIgnore.collider);
        }
	}

    void Explode(Collider Other)
    {
        Remove();
		Debug.Log("Boom " + Other.name);
        
        if(!Other)
        	return;

        AudioSource[] Sources = GetComponentsInChildren<AudioSource>();
        for (int n = 0; n < Sources.Length; n++)
        {
            Sources[n].Stop();
        }

        if (Other && Other.CompareTag("Player"))
        {
            if (PlayerImpactPrefab)
            {
                GameObject go = Instantiate(PlayerImpactPrefab, transform.position, transform.rotation) as GameObject;

                DamageScript[] Damages = go.GetComponentsInChildren<DamageScript>() as DamageScript[];
                foreach(DamageScript Damage in Damages)
                {
                    Damage.SetInstigator(Launcher);
                }
            }
        }
        else
        {
            if (SoilImpactPrefab)
            {
                GameObject go = Instantiate(SoilImpactPrefab, transform.position, transform.rotation) as GameObject;

                DamageScript[] Damages = go.GetComponentsInChildren<DamageScript>() as DamageScript[];
                foreach (DamageScript Damage in Damages)
                {
                    Damage.SetInstigator(Launcher);
                }
            }
        }
             
        if (GetComponent<DamageScript>())
        {
            DamageScript Damage = GetComponent<DamageScript>();
            Damage.SetInstigator(Launcher);
			
            Damage.DoDamage(Other, Launcher);
        }
    }
	
	IEnumerator UpdateRay()
    {
        yield return new WaitForSeconds(0.005f);

        while (true)
        {
        	RaycastHit Hit;
        	int LayerMask = ((1 << 2) | (1 << 4) | (1 << 9) | (1 << 10) | (1 << 12));
        	LayerMask = ~LayerMask;
			
			if (Physics.Raycast(transform.position, transform.forward, out Hit, 9.0f, LayerMask))
        	{
				CurrentNumberOfBounces++;
				
				bool DoBounce = true;
				if(CurrentNumberOfBounces > NumberOfBounces)
					DoBounce = false;
				
				Collider HitCollider = Hit.collider.transform.root.collider ? Hit.collider.transform.root.collider : Hit.collider;
				
				// Don't bounce of players.
        		if (HitCollider.CompareTag("Player"))
        			DoBounce = false;
				
				if(!DoBounce)
					OnCollision(HitCollider);
				else
				{	
					Vector3 FlippedDirVector = Vector3.Reflect(transform.forward, Hit.normal);
					transform.forward = FlippedDirVector;
					
					// Damage Reduction
					DamageScript Damage = GetComponent<DamageScript>();
					if(Damage)
					{
						Damage.ContactDamage *= DamageReduction;
					}
					
					LaunchPower *= LaunchPowerReduction;
					
					// Do the soil impact
					if (SoilImpactPrefab)
            		{
                		Instantiate(SoilImpactPrefab, transform.position, transform.rotation);
		            }
				}
        	}

            yield return new WaitForSeconds(0.05f);
        }
    }
}
