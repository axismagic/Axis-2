using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameScript : MonoBehaviour {

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }
	
	public void ServerSetName(string Name) {
		if(!Network.isServer)
			return;
		
		transform.root.name = Name;
		networkView.RPC("ClientSetName", RPCMode.OthersBuffered, Name);
	}
	
	[RPC]
	public void ClientSetName(string Name) {
		transform.root.name = Name;
	}
}
