using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class PPTrigger : MonoBehaviour {

    public GameObject ImageEffects;

    Transform AddCamera = null;
    Transform RemoveCamera = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (AddCamera && ImageEffects)
        {
            // Add the Post Process Effects.
            Component[] Components = ImageEffects.GetComponents<Component>();
            foreach (Component thisComponent in Components)
            {
                if (!thisComponent || !ImageEffects.camera)
                    continue;

                if (thisComponent.GetType() == ImageEffects.camera.GetType())
                    continue;

                if (thisComponent.GetType() == transform.GetType())
                    continue;

                Component new_component = AddCamera.gameObject.AddComponent(thisComponent.GetType());
                foreach (FieldInfo f in thisComponent.GetType().GetFields())
                {
                    f.SetValue(new_component, f.GetValue(thisComponent));
                }
            }
            AddCamera = null;
        }

        if (RemoveCamera && ImageEffects)
        {
            // Remove the Post Process Effects.
            Component[] Components = ImageEffects.GetComponents<Component>();
            foreach (Component thisComponent in Components)
            {
                if (!thisComponent || !ImageEffects.camera)
                    continue;

                if (thisComponent.GetType() == ImageEffects.camera.GetType())
                    continue;

                if (thisComponent.GetType() == transform.GetType())
                    continue;

                if (RemoveCamera.gameObject.GetComponent(thisComponent.GetType()))
                    Destroy(RemoveCamera.gameObject.GetComponent(thisComponent.GetType()));
            }
            RemoveCamera = null;
        }
	}
	
	void OnTriggerEnter(Collider other) {
        if(!other.gameObject)
            return;
        
        GameObject go = other.gameObject;
        // If this is the local player
		if(!go.CompareTag("Player"))
			return;
		
        // If it is owned by this machine
        if (!go.GetComponent<NetOwnership>() || go.GetComponent<NetOwnership>().GetOwner() != Network.player)
            return;

        Transform Camera = other.transform.Find("Camera");

        if (!Camera)
            return;

        AddCamera = Camera;
	}

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject)
            return;

        GameObject go = other.gameObject;
        // If this is the local player
        if (!go.CompareTag("Player"))
            return;

        // If it is owned by this machine
        if (!go.GetComponent<NetOwnership>() || go.GetComponent<NetOwnership>().GetOwner() != Network.player)
            return;

        Transform Camera = other.transform.Find("Camera");

        if (!Camera)
            return;

        RemoveCamera = Camera;
	}
}
