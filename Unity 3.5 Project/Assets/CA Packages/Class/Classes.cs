using UnityEngine;
using System.Collections;

public class Classes : MonoBehaviour {
	
	enum eClass
	{
		eScout = 0,
		eMedium,
		eHeavy,
		eMax,
	}
	
	private eClass Class = eClass.eMedium;
	
	public float DefaultModifier = 0.0f;
	
	public float ScoutSpdModValue = 0.2f;
	public int ScoutHealthModValue = -25;
	public float ScoutAtkModPercent = -0.1f;
	
	public float MediumSpdModValue = 0.0f;
    public int MediumHealthModValue = 0;
	public float MediumAtkModPercent = 0.0f;
	
	public float HeavySpdModValue = -0.2f;
    public int HeavyHealthModValue = 25;
	public float HeavyAtkModPercent = 0.1f;
	
	public float AdditionalSpdModifier = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float GetClassSpd()
	{
		switch(Class)
		{
			case eClass.eScout:
				return ScoutSpdModValue + AdditionalSpdModifier;
			case eClass.eMedium:
				return MediumSpdModValue + AdditionalSpdModifier;	
			case eClass.eHeavy:
				return HeavySpdModValue + AdditionalSpdModifier;
			default:
				return DefaultModifier + AdditionalSpdModifier;
		}
	}
	
	public float GetClassAtk()
	{
		switch(Class)
		{
			case eClass.eScout:
				return ScoutAtkModPercent;
			case eClass.eMedium:
				return MediumAtkModPercent;	
			case eClass.eHeavy:
				return HeavyAtkModPercent;
			default:
				return DefaultModifier;
		}
	}
	
	public int GetClassHealth()
	{
		switch(Class)
		{
			case eClass.eScout:
				return ScoutHealthModValue;
			case eClass.eMedium:
				return MediumHealthModValue;	
			case eClass.eHeavy:
				return HeavyHealthModValue;
			default:
				return (int)DefaultModifier;
		}
	}
	
	public void SetClass(int NewClass) {
		networkView.RPC("ClientSetClass", RPCMode.All, NewClass); 
	}
	
	[RPC]
	void ClientSetClass(int NewClass)
	{
		Class = (eClass)NewClass;
	}
}
