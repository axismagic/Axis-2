using UnityEngine;
using System.Collections;

public class AfterBurnerScript : PerkScript {
	
	public float SpeedIncrease = 0.2f;
	
	void OnDestroy()
	{
        if (!GetOwner())
            return;
            
        Classes Class = GetOwner().GetComponent<Classes>();
        if(Class)
        	Class.AdditionalSpdModifier = 0.0f;
	}
	
    [RPC]
    public override void ActivatePerk()
    {
        if (!GetOwner())
            return;
            
        Classes Class = GetOwner().GetComponent<Classes>();
        if(Class)
        	Class.AdditionalSpdModifier = SpeedIncrease;

        base.ActivatePerk();
    }
    
    [RPC]
    public override void SendPerkTimeout()
    {
    	Debug.Log("Timeout");
        if (!GetOwner())
            return;
            
        Classes Class = GetOwner().GetComponent<Classes>();
        if(Class)
        	Class.AdditionalSpdModifier = 0.0f;

        base.SendPerkTimeout();
    }
}
