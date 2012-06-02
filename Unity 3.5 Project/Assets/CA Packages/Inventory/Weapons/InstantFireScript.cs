using UnityEngine;
using System.Collections;

public class InstantFireScript : MonoBehaviour {
	
	/***
	* gun
	***/
    public float BaseSpread = 0.0f;
    public float SpreadChangeOverTime = 0.0f;
    public float MaxSpread = 0.8f;
    public float MaxDistance = 200.0f;
    
    private float CurrentSpread = 0.0f;
    private float MaxDistanceSq = 0.0f;
    
    /***
    * VFX
    ***/
    public GameObject PlayerImpactPrefab;
    public GameObject SoilImpactPrefab;

	public void Awake()
	{
		MaxDistanceSq = MaxDistance * MaxDistance;
	}
	
	public void InitFire()
	{
		CurrentSpread = BaseSpread;
	}

	public void Fire(int BurstCount)
	{
        // Cast a ray, see if we hit anything
        RaycastHit Hit;
        int LayerMask = ((1 << 2) | (1 << 4) | (1 << 9) | (1 << 10));
        LayerMask = ~LayerMask;

        CurrentSpread += (SpreadChangeOverTime * Time.deltaTime);

		for(int n = 0; n < BurstCount; n++)
		{
        	float ZSpread = CurrentSpread;
        	float YSpread = CurrentSpread;

        	Quaternion YRot = Quaternion.identity;
        	Quaternion ZRot = Quaternion.identity;

        	ZSpread = Random.Range(0, 360.0f);
        	YSpread = Random.Range(-YSpread, YSpread);

        	YSpread = Mathf.Min(YSpread, MaxSpread);
        	YSpread = Mathf.Max(YSpread, -MaxSpread);

        	YRot = Quaternion.AngleAxis(YSpread, transform.up);
        	ZRot = Quaternion.AngleAxis(ZSpread, transform.forward);
        	Vector3 Dir = ZRot * YRot * transform.forward;

        	if (Physics.Raycast(transform.position, Dir, out Hit, MaxDistanceSq, LayerMask))
        	{
        	    Quaternion qRot = Quaternion.LookRotation(Hit.normal) * Quaternion.Euler(90.0f, 0.0f, 0.0f);

            	if (Hit.collider.CompareTag("Player") || Hit.collider.CompareTag("Components"))
            	{
                	if (PlayerImpactPrefab)
                	    Instantiate(PlayerImpactPrefab, Hit.point, qRot);
            	}
                else
            	{
                	if (SoilImpactPrefab)
                    	Instantiate(SoilImpactPrefab, Hit.point, qRot);
            	}
        	}

        	if (Hit.collider && Hit.collider.transform.root.collider)
        	{
                // Don't damage ourself
                if (Hit.collider.transform.root.collider == transform.root.collider)
                    break;

            	DamageScript Damage = GetComponent<DamageScript>();
           		if (Damage)
                	Damage.DoDamage(Hit.collider.transform.root.collider, transform.root.gameObject);
        	}
		}
	}
	
}
