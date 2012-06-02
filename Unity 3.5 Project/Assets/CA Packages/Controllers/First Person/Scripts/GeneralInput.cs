using UnityEngine;
using System.Collections;

public class GeneralInput : MonoBehaviour
{
    bool ThirdPerson = false;
    public Vector3 CamOffset = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        bool PressedRight = false;
        PressedRight = Input.GetButton("FireLeft") || Input.GetAxis("FireLeft") > 0.1f;

        if(Input.GetButtonDown("SwitchCamera"))
        {
            ThirdPerson = !ThirdPerson;

            Camera OurCamera = GetComponentInChildren<Camera>();
            if(OurCamera)
                if (ThirdPerson)
                    OurCamera.transform.position += (OurCamera.transform.rotation * CamOffset);
                else
                    OurCamera.transform.position -= (OurCamera.transform.rotation * CamOffset);
        }

		HealthScript Health = GetComponent<HealthScript>();
		if(Health && Health.IsAlive()) {
			return;
		}

        ShipCustomisationScript Custom = FindObjectOfType(typeof(ShipCustomisationScript)) as ShipCustomisationScript;

        if (Input.GetButton("ShowCustomisation"))
        {
            Custom.ShowCustomisation(true);
        }

        DeathScript Death = GetComponent<DeathScript>();
        if (!Death || Death.GetDeathTime() > 0.0f)
            return;

        if (!Custom.IsShowingCusomisation())
        {
            if (PressedRight)
            {
                Custom.DoSpawn(false);
		    }
        }
	}
}
