using UnityEngine;
using System.Collections;

public class MenuInput : MonoBehaviour {
	
	public float LookSmooth = 2f;
	public int Ship = 0;
	public Vector3 PosOffset;
	public GameObject[] ShipScenes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("SelectRight"))
			AlterShip(1);
		else if(Input.GetButton("SelectLeft")) 
			AlterShip(-1);
		
		Vector3 Dir = ShipScenes[Ship].transform.position - transform.position;
		Dir.Normalize();
		
		Quaternion Target = Quaternion.LookRotation(Dir);
    	transform.rotation = Quaternion.Slerp(transform.rotation, Target, Time.deltaTime * LookSmooth);
    	
    	Input.ResetInputAxes();
	}
	
	void AlterShip(int Dir) {
		Ship += Dir;
		if(Ship >= ShipScenes.Length)
			Ship = 0;
		else if(Ship < 0)
			Ship = ShipScenes.Length - 1;
	}
}
