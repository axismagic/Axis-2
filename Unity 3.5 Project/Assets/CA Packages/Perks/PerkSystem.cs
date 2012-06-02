using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerkSystem : MonoBehaviour
{
    public GameObject[] Perks;

    private List<GameObject> ExistingPerks = new List<GameObject>();

    void OnDestroy()
    {
        ClearPerks();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddPerk(int Index)
    {
        GameObject Perk = Network.Instantiate(Perks[Index], transform.position, transform.rotation, 0) as GameObject;

        Debug.Log("Add Perk : " + Perk);

        if (Perk.GetComponent<PerkScript>())
            Perk.GetComponent<PerkScript>().SetOwner(transform.root);
        
        ExistingPerks.Add(Perk);
    }

    public void ClearPerks()
    {
        foreach (GameObject Perk in ExistingPerks)
        {
            if (Perk && Perk.networkView)
            {
                Network.RemoveRPCs(Perk.networkView.viewID);
                Network.Destroy(Perk.networkView.viewID);
            }
        }

        ExistingPerks.Clear();
    }
}
