using UnityEngine;
using System.Collections;

/*****
*	
*	This component deals with turning scripts on and off
*	if we own this object
*	
*****/

public class NetOwnership : MonoBehaviour {
	
	public MonoBehaviour[] AssociatedScripts;
	
	private bool Owned = false;
	
	private NetworkPlayer Owner;

	void Awake(){
		// Disable this until we are told otherwise.
		EnableScripts(false);
		
		transform.Find("Camera").camera.enabled = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void EnableScripts(bool Enable) {
		foreach (MonoBehaviour Script in AssociatedScripts) {
			if(Script) {
				if(Enable)
					Script.enabled = true;
				else {
					Script.enabled = false;
				}
			}
		}
	}
	
	public bool IsOwned() {
		return Owned;
	}
	
	public bool IsOwner(NetworkPlayer Player) {
		return (Owner == Player);
	}
	
	public NetworkPlayer GetOwner() {
		return Owner;
	}
	
	[RPC]
	public void SetPlayer(NetworkPlayer Player){
		if(Player == Network.player){
			// This is us, enable all scripts
			EnableScripts(true);
            Transform Camera = transform.Find("Camera");
            Camera.camera.enabled = true;

            AudioListener Listener = Camera.GetComponent<AudioListener>();
            Listener.enabled = true;
			
			Owner = Player;
			Owned = true;
			
		}
		else {
			
			Owner = Player;
			Owned = false;
		}
	}
}