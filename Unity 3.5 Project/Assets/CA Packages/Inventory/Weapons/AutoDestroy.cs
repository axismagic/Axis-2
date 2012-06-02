using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public float DestroyTime = 4.0f;
	private float ElapsedTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ElapsedTime += Time.deltaTime;
		
		if(ElapsedTime > DestroyTime)
			Destroy(gameObject);
	}
}
