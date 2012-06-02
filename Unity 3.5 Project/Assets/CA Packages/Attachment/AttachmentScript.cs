using UnityEngine;
using System.Collections;

public class AttachmentScript : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [RPC]
    public void SendAttachToParent(NetworkPlayer NetOwner, NetworkViewID ToParent, string ParentName)
    {
        NetworkView netViewToParent = NetworkView.Find(ToParent);

        Transform ThisPlayer = null;
        GameObject[] Targets;
        Targets = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Target in Targets)
        {
            if (Target.GetComponent<NetOwnership>() && Target.GetComponent<NetOwnership>().GetOwner() == NetOwner)
            {
                ThisPlayer = Target.transform;
            }
        }
        
        AttachmentSystem ParentAttachments = ThisPlayer.root.GetComponent<AttachmentSystem>();
        Transform Parent = ParentAttachments.GetAttachmentPoint(ParentName);

        netViewToParent.transform.position = Parent.transform.position;
        netViewToParent.transform.rotation = Parent.transform.rotation;

        Rotation RotationScript = Parent.transform.root.GetComponent<Rotation>();
        Movement MovementScript = Parent.transform.root.GetComponent<Movement>();

        int ChildCount = Parent.childCount;
        if(ChildCount > 0)
        {
            foreach (Transform child in Parent)
            {
                ParentAttachments.RemoveFromAllAttachments(child.networkView.viewID);

                ShipAnimationScript AnimationScript = child.GetComponentInChildren<ShipAnimationScript>();
                if (AnimationScript)
                {
                    if (RotationScript)
                        RotationScript.RemoveAnimationScript(AnimationScript);
                    if (MovementScript)
                        MovementScript.RemoveAnimationScript(AnimationScript);
                }
            }
        }

        netViewToParent.transform.parent = Parent.transform;
        ParentAttachments.AddToAllAttachments(netViewToParent.viewID, Parent.transform);

        ShipAnimationScript ChildAnimationScript = netViewToParent.transform.GetComponentInChildren<ShipAnimationScript>();
        if (ChildAnimationScript)
        {
            if (RotationScript)
                RotationScript.AddAnimationScript(ChildAnimationScript);
            if (MovementScript)
                MovementScript.AddAnimationScript(ChildAnimationScript);
        }

        // Turn off the trails and VFX for us.
        if (NetOwner == Network.player)
        {
            Debug.Log("Turning off attachments");
            TrailRenderer[] Trails = GetComponentsInChildren<TrailRenderer>();
            for (int n = 0; n < Trails.Length; n++)
            {
                Trails[n].enabled = false;
            }
        }
        else
        {
            Debug.Log("Turning off attachments");
            DeathmatchLogicScript Logic = FindObjectOfType(typeof(DeathmatchLogicScript)) as DeathmatchLogicScript;
            Color Primary = Logic.GetPrimaryColour(NetOwner);

            // Colourise our trail.
            TrailRenderer[] Trails = GetComponentsInChildren<TrailRenderer>();
            for (int n = 0; n < Trails.Length; n++)
            {
                Material trail = Trails[n].material;
                // Set the color of the material to tint the trail.
                trail.SetColor("_TintColor", Primary);
            }
        }

        HideHelperWeapon WeaponHider = GetComponentInChildren<HideHelperWeapon>();
        if (WeaponHider)
            WeaponHider.SpawnCorrectWeapon(Parent.name);
    }
}
