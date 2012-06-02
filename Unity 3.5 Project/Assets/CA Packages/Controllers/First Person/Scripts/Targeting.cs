using UnityEngine;
using System.Collections;

public class Targeting : MonoBehaviour
{
    public string TargetsWithTag = "Player";

    public Camera ConversionCamera;

    NetworkViewID BestTarget;

    void OnEnable()
    {
        StartCoroutine("UpdateBestTarget");
    }

    void OnDisable()
    {
        StopCoroutine("UpdateBestTarget");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!ConversionCamera)
            return;
    }

    NetworkViewID GetBestTarget()
    {
        return BestTarget;
    }

    IEnumerator UpdateBestTarget()
    {
        while (true)
        {
            HealthScript OurHealth = transform.GetComponent<HealthScript>();
            if (OurHealth && !OurHealth.IsAlive())
            {
                NetworkViewID NullViewID = NetworkViewID.unassigned;
                if (NullViewID != BestTarget)
                {
                    SetBestTarget(NullViewID);
                    networkView.RPC("SetBestTarget", RPCMode.Server, NullViewID);
                }
                yield return new WaitForSeconds(0.3f);
                continue;
            }

            GameObject CurrentTarget = null;

            GameObject[] Targets;
            Targets = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject Target in Targets)
            {
                // Don't test against ourself
                if (Target == transform.gameObject)
                {
                    continue;
                }

                HealthScript Health = Target.GetComponent<HealthScript>();
                if (Health && !Health.IsAlive())
                    continue;

                Vector3 TargetDir = Target.transform.position - transform.position;
                float Dist = TargetDir.magnitude;
                TargetDir.Normalize();
                Vector3 ScreenPos = ConversionCamera.WorldToScreenPoint(Target.transform.position);

                // Check this is on screen.
                float ScreenPercentToCheck = 0.9f;
                if (ScreenPos.x < 0 + ConversionCamera.pixelWidth * (1.0f - ScreenPercentToCheck) ||
                    ScreenPos.y < 0 + ConversionCamera.pixelHeight * (1.0f - ScreenPercentToCheck) ||
                    ScreenPos.x > ConversionCamera.pixelWidth * ScreenPercentToCheck ||
                    ScreenPos.y > ConversionCamera.pixelHeight * ScreenPercentToCheck)
                    continue;

                // Ensure the object is in front of us
                if (Vector3.Dot(transform.forward, TargetDir) < 0)
                    continue;

                // only accept this target if it is visible.
                RaycastHit Hit;
                int LayerMask = ((1 << 2) | (1 << 4) | (1 << 8) | (1 << 9) | (1 << 10) | (1 << 12));
                LayerMask = ~LayerMask;
                if (Physics.Raycast(transform.position, TargetDir, out Hit, Dist, LayerMask))
                {
                    Debug.Log(Hit.collider);
                    continue;
                }

                if (BestTarget == Target.networkView.viewID)
                {
                    CurrentTarget = Target;
                    break;
                }

                if (CurrentTarget)
                {
                    Vector3 CurrTargetDir = CurrentTarget.transform.position - transform.position;
                    float CurrDist = CurrTargetDir.magnitude;
                    if (CurrDist > Dist)
                        CurrentTarget = Target;
                }
                else
                {
                    CurrentTarget = Target;
                }
            }

            // If we change best target, let the server know.
            NetworkViewID ViewID = CurrentTarget ? CurrentTarget.networkView.viewID : NetworkViewID.unassigned;
            if (ViewID != BestTarget)
            {
                SetBestTarget(ViewID);
                Debug.Log("Set Best Target + " + ViewID);
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    [RPC]
    public void SetBestTarget(NetworkViewID ID)
    {
        BestTarget = ID;
        InventoryManagerScript InvMan = transform.GetComponentInChildren<InventoryManagerScript>();
        if (InvMan)
        {
            if (BestTarget == NetworkViewID.unassigned)
            {
                InvMan.SetBestTarget(null);
            }
            else
            {
                NetworkView netView = NetworkView.Find(BestTarget);
                InvMan.SetBestTarget(netView.transform);
            }
        }
    }
}