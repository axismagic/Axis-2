using UnityEngine;
using System.Collections;

public class CrosshairScript : MonoBehaviour {
	
	public Texture2D CrosshairTexture;
	
	Transform TargetTransform;
	Vector3 TargetPosition;
	Vector3 PreviousPosition;
	Vector3 CurrentPosition;
	float TargetTime;
	float ElapsedTime;
	float Ratio;
	
	void Awake() {
		TargetPosition = new Vector3(Screen.width/2, Screen.height/2, 0.0f);
		CurrentPosition = TargetPosition;
		PreviousPosition = TargetPosition;
		TargetTime = 0.0f;
		ElapsedTime = 0.0f;
		Ratio = 1.0f;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!transform.root.GetComponent<NetOwnership>())
            return;

		// We are not the owner of this ship, do not process the update.
		if(!transform.root.GetComponent<NetOwnership>().IsOwner(Network.player)) {
			return;
		}
		
		ElapsedTime += Time.deltaTime;
		Ratio = ElapsedTime / TargetTime;
		if(Ratio > 1.0f)
			Ratio = 1.0f;

        Transform TheTargetTransform = TargetTransform;
        if (TheTargetTransform && TheTargetTransform.GetComponent<DecoyScript>()
            && TheTargetTransform.GetComponent<DecoyScript>().GetDecoy())
        {
            TheTargetTransform = TheTargetTransform.GetComponent<DecoyScript>().GetDecoy();
        }

        if (TheTargetTransform)
        {
			Transform go = transform.root.Find("Camera");
			Camera OurCamera = go.GetComponent<Camera>();
			Vector3 Target2DPosition;
            Target2DPosition = OurCamera.WorldToScreenPoint(TheTargetTransform.position);
			Target2DPosition.y = Screen.height - Target2DPosition.y;
			
			// TD - Ensure the target position is still on screen
			if(	Target2DPosition.x < 0 || Target2DPosition.x > OurCamera.pixelWidth ||
				Target2DPosition.y < 0 || Target2DPosition.y > OurCamera.pixelHeight)
				return;
				
			TargetPosition = Target2DPosition;
		}
	}
	
	void OnGUI() {
        if (!transform.root.GetComponent<NetOwnership>())
            return;

		// We are not the owner of this ship, do not process the update.
		if(!transform.root.GetComponent<NetOwnership>().IsOwner(Network.player)) {
			return;
		}
		
		CurrentPosition = Vector3.Lerp(PreviousPosition, TargetPosition, Ratio);
		
		GUI.Label(new Rect(CurrentPosition.x - 10, CurrentPosition.y - 10, 20, 20), CrosshairTexture);
	}
	
	public void SetTarget(Transform TargetTrans, float Time) {
		PreviousPosition = CurrentPosition;
		TargetTransform = TargetTrans;	
		if(TargetTrans) {	
			TargetPosition = TargetTransform.position;
		}
		else {
			TargetPosition = new Vector3(Screen.width/2, Screen.height/2, 0.0f);
		}
		
		TargetTime = Time;
		ElapsedTime = 0.0f;
		Ratio = 0.0f;
	}
}
