using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*****
*
*	Attach this script component to anything you want to have health.
*
*	Logic is handled on the server, so, this won't process anything if
*	it is on a clients GameObject.
*
*****/

public class HealthScript : MonoBehaviour {
	
	public class DamageIndicator {
		public Transform InstigatorLoc;
		public float ElapsedFadeTime;
	};
	
	public Camera ConversionCamera;
	
	public int TotalHealth = 100;
	public int CurrentHealth = 100;

    private int ModifiedTotalHealth = 100;
	public float HitFadeTime = 2.0f;
	
	private bool Invulnerable = false;
	private List<DamageIndicator> DamageIndicators = new List<DamageIndicator>();

    public List<DamageIndicator> GetIndicators()
    {
        return DamageIndicators;
    }

    public float GetModifiedTotalHealth()
    {
        return ModifiedTotalHealth;
    }

	// Use this for initialization
	void Start () {
        CurrentHealth = TotalHealth;
	}
	
	// Update is called once per frame
    void Update()
    {
        // Modify the Damage Based on our class.
        int HealthMod = 0;
        Classes Class = GetComponent<Classes>();
        if (Class)
            HealthMod = Class.GetClassHealth();

        int TempModifiedTotalHealth = TotalHealth + HealthMod;

        if (ModifiedTotalHealth != TempModifiedTotalHealth)
        {
            ModifiedTotalHealth = TempModifiedTotalHealth;
            CurrentHealth = ModifiedTotalHealth;
        }
    }

    public void DoDamageVisualisation(Transform Instigator, float Damage) {
        if (Invulnerable) {
            return;
        }

        NetOwnership OwnerScript = GetComponent<NetOwnership>();
        if (OwnerScript && OwnerScript.IsOwner(Network.player)) {
            DoDamageHUD(Instigator);
        }
    }

    public void DoDamage(float Damage, NetworkPlayer Instigator){
        if (!Network.isServer)
        {
            return;
        }
        
		if(CurrentHealth <= 0) {
			return;
		}	

		if(Invulnerable) {
			return;
		}
		
		CurrentHealth -= (int)Damage;
		
		if(CurrentHealth <= 0) {
			CurrentHealth = 0;
			
			DeathScript Death = GetComponent<DeathScript>();
            if (Death)
            {
                Death.Kill(Instigator);
			}
		}
	}
	
	void DoDamageHUD(Transform Instigator) {
		DamageIndicator Indicator = new DamageIndicator();
		
		if(DamageIndicators.Count > 0)
			return;
		
		Indicator.InstigatorLoc = Instigator;
		Indicator.ElapsedFadeTime = 0.0f;
		
		// Check if we do not have this instigator in our list already.
		foreach (DamageIndicator CurrentIndicator in DamageIndicators) {
			if(CurrentIndicator.InstigatorLoc == Instigator)
				return;
		}
		
		DamageIndicators.Add(Indicator);
	}
	
	public void Respawn() {
        CurrentHealth = ModifiedTotalHealth;
	}
	
	public bool IsAlive() {
		return (CurrentHealth > 0);
	}
	
	public void Restore() {
        CurrentHealth = ModifiedTotalHealth;
	}

    public void RestoreHealth(float SomeHealth, bool PastLimit)
    {
        if (!PastLimit && CurrentHealth > ModifiedTotalHealth)
            SomeHealth = 0.0f;
        else if (!PastLimit && CurrentHealth + SomeHealth > ModifiedTotalHealth)
            SomeHealth = ModifiedTotalHealth - CurrentHealth;
        else if (PastLimit && CurrentHealth + SomeHealth > ModifiedTotalHealth * 2.0f)
            SomeHealth = (ModifiedTotalHealth * 2.0f) - CurrentHealth;

        CurrentHealth += (int)SomeHealth;
    }
	
	public void SetInvulnerable(bool Enable) {
		Invulnerable = Enable;
	}
	
	void OnSerializeNetworkView(BitStream Stream, NetworkMessageInfo Info) {
		if (Stream.isWriting) {
			int Health = CurrentHealth;		
			Stream.Serialize(ref Health);
		}
		else {
			int Health = CurrentHealth;
			Stream.Serialize(ref Health);
			CurrentHealth = Health;
		}
	}
}
