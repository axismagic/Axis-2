using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {
	
	public float RespawnTime = 0.0f;
	public GameObject RespawnVisualisation;
	
	private GameObject RespawnVisualisationInstance;
	private float ElapsedRespawnTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ElapsedRespawnTime += Time.deltaTime;
				
		if(RespawnVisualisationInstance) {
			if(ElapsedRespawnTime >= RespawnTime) {
				ParticleEmitter Emitter = RespawnVisualisationInstance.GetComponent<ParticleEmitter>();
				if(Emitter) {
					Emitter.emit = false;
				
					HealthScript Health = GetComponent<HealthScript>();
					if(Health) {
						Health.SetInvulnerable(false);
					}
				}
			}
		}
	}
	
	public void StartRespawnServer() {
		Debug.Log("Respawning");
		
		HealthScript Health = GetComponent<HealthScript>();
		if(Health) {
			Health.Restore();
			Health.SetInvulnerable(true);
		}
	}
	
	public void StartRespawnClient(Vector3 Position, Quaternion Rotation) {
    	networkView.RPC("RespawnClient", RPCMode.All, Position, Rotation);
	}
	
	[RPC]
    public void RespawnClient(Vector3 Position, Quaternion Rotation)
    {
		if(RespawnVisualisationInstance) {
			Destroy(RespawnVisualisationInstance);
		}
		
		ElapsedRespawnTime = 0.0f;
		
		if(RespawnVisualisation) {
			RespawnVisualisationInstance = Instantiate(RespawnVisualisation, transform.position, transform.rotation) as GameObject;
			RespawnVisualisationInstance.transform.parent = transform;
		}
		
		gameObject.layer = 8;

        transform.position = Position;
        transform.rotation = Rotation;
        Movement MovementScript = GetComponent<Movement>();
        if (MovementScript)
            MovementScript.StartSync();
        Rotation RotationScript = GetComponent<Rotation>();
        if (RotationScript)
            RotationScript.StartSync();

        AttachmentSystem Attachments = GetComponent<AttachmentSystem>();
        if (Attachments)
            Attachments.Reattach();
	}
}
