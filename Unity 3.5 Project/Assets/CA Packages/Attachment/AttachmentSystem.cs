using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Attachment/Attachment System")]
public class AttachmentSystem : MonoBehaviour {

	public string GeneralAttachmentPrefix = "att_gen";
	public string WeaponAttachmentPrefix = "att_wep";
	public string EngineAttachmentPrefix = "att_eng";
	
	public Transform[] GeneralAttachments;
	public Transform[] WeaponAttachments;
	public Transform[] EngineAttachments;
	
	private int[] AttachedWeapons = new int[] { -1, -1 };
	
	private List<Transform> GeneralAttachmentsPoints = new List<Transform>();
	private List<Transform> WeaponAttachmentsPoints = new List<Transform>();
	private List<Transform> EngineAttachmentsPoints = new List<Transform>();

    class AttachmentConnection
    {
        public Transform Child;
        public Transform Parent;
    };
    private List<AttachmentConnection> AllAttachments = new List<AttachmentConnection>();
    private List<AttachmentConnection> ObjectsToParent = new List<AttachmentConnection>();

	// Use this for initialization
    void Awake()
    {
		for(int n = 0; n < AttachedWeapons.Length; n++)
		{
			AttachedWeapons[n] = -1;	
		}

        RecurseChildren(transform);

        // Build a list of all attachments
        Debug.Log(GeneralAttachmentsPoints.Count + " General Attachment Points");
        Debug.Log(WeaponAttachmentsPoints.Count + " Weapon Attachment Points");
        Debug.Log(EngineAttachmentsPoints.Count + " Engine Attachment Points");
	}

    void OnDestroy()
    {
        if (!Network.isServer)
            return;

        foreach (AttachmentConnection Attachment in AllAttachments)
        {
            Network.RemoveRPCs(Attachment.Child.GetComponent<NetworkView>().viewID);
            Network.Destroy(Attachment.Child.GetComponent<NetworkView>().viewID);
        }
    }

    public Transform GetAttachmentPoint(string Name)
    {
        foreach (Transform AttachmentPoint in GeneralAttachmentsPoints)
        {
            if (AttachmentPoint.name == Name)
                return AttachmentPoint;
        }
        foreach (Transform AttachmentPoint in WeaponAttachmentsPoints)
        {
            if (AttachmentPoint.name == Name)
                return AttachmentPoint;
        }
        foreach (Transform AttachmentPoint in EngineAttachmentsPoints)
        {
            if (AttachmentPoint.name == Name)
                return AttachmentPoint;
        }
        return null;
    }

    public void RemoveFromAllAttachments(NetworkViewID netViewID)
    {
        for (int n = AllAttachments.Count - 1; n >= 0; n--)
        {
            Transform Child = AllAttachments[n].Child;
            if (Child.networkView.viewID == netViewID)
            {
                Network.RemoveRPCs(Child.networkView.viewID);
                InventoryManagerScript Inv = Child.root.GetComponentInChildren<InventoryManagerScript>();
                if (Inv)
                    Inv.Remove(Child.networkView);

                Destroy(Child.gameObject);
                AllAttachments.Remove(AllAttachments[n]);
            }
        }
    }

    public void AddToAllAttachments(NetworkViewID netViewID, Transform Parent)
    {
        NetworkView netViewToParent = NetworkView.Find(netViewID);
        AttachmentConnection Connection = new AttachmentConnection();
        Connection.Child = netViewToParent.transform;
        Connection.Parent = Parent;
        AllAttachments.Add(Connection);

        for (int n = 0; n < WeaponAttachments.Length; n++)
        {
            if (netViewToParent.transform.GetComponent<CrosshairScript>())
            {
                // If this is in the weapon list, add it to the inventory.
                InventoryManagerScript Inv = Parent.root.GetComponentInChildren<InventoryManagerScript>();
                if (Inv)
                    Inv.AddWeapon(netViewToParent.transform);

                return;
            }
        }
    }
	
	public void SetWeapons(int LeftWeapon, int RightWeapon)
	{
		AttachedWeapons[0] = LeftWeapon;
		AttachedWeapons[1] = RightWeapon;

		// Attach all components
        for (int n = 0; n < AttachedWeapons.Length; n++)
        {
        	if(n >= WeaponAttachments.Length)
        		continue;
        	
			// We've run out of attachment points
            if (n >= WeaponAttachmentsPoints.Count)
				break;
				
			int Weapon = AttachedWeapons[n];

            SpawnAndAttachAttachment(WeaponAttachments[Weapon], WeaponAttachmentsPoints[n]);
		}
	}

    void RecurseChildren(Transform parent)
    {
        if (parent.name.Contains(GeneralAttachmentPrefix))
        {
            GeneralAttachmentsPoints.Add(parent);
        }
        if (parent.name.Contains(WeaponAttachmentPrefix))
        {
            WeaponAttachmentsPoints.Add(parent);
        }
        if (parent.name.Contains(EngineAttachmentPrefix))
        {
            EngineAttachmentsPoints.Add(parent);
        }

        foreach (Transform child in parent)
        {
            RecurseChildren(child);
        }
    }

    void Start()
    {
        if (!Network.isServer)
            return;

        Debug.Log("Attaching General");
        // Attach all components
        for (int n = 0; n < GeneralAttachments.Length; n++)
        {
            // We've run out of attachment points
            if (n >= GeneralAttachmentsPoints.Count)
                break;

            SpawnAndAttachAttachment(GeneralAttachments[n], GeneralAttachmentsPoints[n]);
        }

        Debug.Log("Attaching Engine");
        // Attach all components
        for (int n = 0; n < EngineAttachments.Length; n++)
        {
            // We've run out of attachment points
            if (n >= EngineAttachmentsPoints.Count)
                break;

            SpawnAndAttachAttachment(EngineAttachments[n], EngineAttachmentsPoints[n]);
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!Network.isServer)
            return;

        for (int n = ObjectsToParent.Count - 1; n >= 0; n--)
        {
            Transform Child = ObjectsToParent[n].Child;
            Transform Parent = ObjectsToParent[n].Parent;
            if (Child.networkView)
            {
                Child.networkView.RPC("SendAttachToParent", RPCMode.AllBuffered, Parent.root.GetComponent<NetOwnership>().GetOwner(), Child.networkView.viewID, Parent.name);

                ObjectsToParent.Remove(ObjectsToParent[n]);
            }
        }
    }

    private void SpawnAndAttachAttachment(Transform ToSpawn, Transform Parent)
    {
        Transform go = Network.Instantiate(ToSpawn, transform.position, transform.rotation, 0) as Transform;

        AttachmentConnection Attachment = new AttachmentConnection();
        Attachment.Child = go;
        Attachment.Parent = Parent;
        ObjectsToParent.Add(Attachment);
    }

    public void DetachAndApplyImpulse()
    {
        networkView.RPC("DisconnectAttachments", RPCMode.All);
    }

    public void Reattach()
    {
        networkView.RPC("ReconnectAttachments", RPCMode.All);
    }
    
    [RPC]
    public void DisconnectAttachments()
    {
        Debug.Log("Disconnecting Attachments");
        foreach (AttachmentConnection Attachment in AllAttachments)
        {
            Attachment.Child.transform.parent = null;

            if (Attachment.Child.rigidbody)
            {
                if (Attachment.Child.collider)
                    Attachment.Child.collider.isTrigger = false;

                Attachment.Child.rigidbody.useGravity = true;
                Attachment.Child.rigidbody.isKinematic = false;

                Vector3 ExplosionPosition = Attachment.Parent.position;
                float Impulse = Random.Range(15.0f, 20.0f);
                Attachment.Child.rigidbody.AddExplosionForce(Impulse, ExplosionPosition, 4.0f);
            }
        }

        if (transform.root)
        {
            Movement Controller = transform.root.GetComponent<Movement>();
            if (Controller)
                Controller.FallUnderGravity(true);
        }
    }
    
    [RPC]
    public void ReconnectAttachments()
    {
        Debug.Log("Reconnecting Attachments");
        foreach (AttachmentConnection Attachment in AllAttachments)
        {
            if (Attachment.Child.rigidbody)
            {
                if (Attachment.Child.collider)
                    Attachment.Child.collider.isTrigger = true;

                Attachment.Child.rigidbody.useGravity = false;
                Attachment.Child.rigidbody.isKinematic = true;
            }

            Attachment.Child.position = Attachment.Parent.position;
            Attachment.Child.rotation = Attachment.Parent.rotation;
            Attachment.Child.transform.parent = Attachment.Parent;
        }

        if (transform.root)
        {
            Movement Controller = transform.root.GetComponent<Movement>();
            if (Controller)
                Controller.FallUnderGravity(false);
        }
    }
}
