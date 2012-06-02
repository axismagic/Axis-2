using UnityEngine;
using System.Collections;

/*****
*
*	Add this script to anything you want to do damage with.
* 	The receiving object must also have a HealthScript Component
*
*	This component only does damage on the server. Logic
*	is handled on the server.
*
*****/

public class DamageScript : MonoBehaviour {

	public float ContactDamage = 5.0f;

    public bool Falloff = false;
    public float Range = 1.0f;

    GameObject Instigator = null;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetInstigator(GameObject TheInstigator)
    {
        Instigator = TheInstigator;
    }

    public void DoDamage(Collider other, GameObject Instigator)
    {		
        HealthScript Health = other.GetComponent<HealthScript>();

        if (Health)
        {
            Health.DoDamageVisualisation(transform.root, ContactDamage);
        }

        if (!Network.isServer)
        {
            return;
        }

        if (Health)
        {
            float Damage = ContactDamage;
            if (Falloff)
            {
                Vector3 ToObj = (other.transform.root.position - transform.position);
                float Dist = ToObj.sqrMagnitude;

                float Ratio = Dist / (Range * Range);
                Ratio = Mathf.Min(1.0f, Ratio);
                Ratio = Mathf.Max(0.0f, Ratio);

                Ratio = 1.0f - Ratio;

                Damage *= Ratio;
            }

            // grab our net player from the instigator
            if(Instigator)
            {
            	// Modify the Damage Based on our class.
        		float AtkMod = 0.0f;
        		Classes Class = Instigator.GetComponent<Classes>();
        		if(Class)
        			AtkMod = Class.GetClassAtk();
        		
        		Damage = Damage + (Damage * AtkMod);
        	
            	NetOwnership InstigatorNetOwnership = Instigator.GetComponent<NetOwnership>();
            	if (InstigatorNetOwnership)
            	    Health.DoDamage(Damage, InstigatorNetOwnership.GetOwner());
            }
            else
            {
            	NetworkPlayer Player = new NetworkPlayer();
            	Health.DoDamage(Damage, Player);	
            }
        }
    }
	
	void OnTriggerEnter(Collider other) {
        DoDamage(other, Instigator);
	}
}
