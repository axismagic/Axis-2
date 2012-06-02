using UnityEngine;using System.Collections;public class LockOnScript : MonoBehaviour{    private enum eLockOnState    {        eLockedOn = 0,        eLockingOn,    };    private eLockOnState LockOnState = eLockOnState.eLockedOn;    private Quaternion OldRotation;    private Transform LockedTargetObject;
    public Camera ConversionCamera;

    public bool DoLockOn = true;    private float ElapsedLockOnTime = 0.0f;
    public float LockOnTime = 1.0f;

    void OnEnable()
    {
        StartCoroutine("UpdateCameraList");
    }

    void OnDisable()
    {
        StopCoroutine("UpdateCameraList");
    }	// Use this for initialization	void Start () {		}		// Update is called once per frame    void Update()    {        UpdateTargetLock();	}    void UpdateTargetLock()    {        if (!transform.parent)            return;        Vector3 LockedDir = transform.parent.transform.forward;        if (!LockedTargetObject)        {            RaycastHit Hit;
            Transform Parent = transform.root;
            int LayerMask = ((1 << 2) | (1 << 4) | (1 << 9) | (1 << 10));
            LayerMask = ~LayerMask;
            if (Parent && Physics.Raycast(Parent.position, Parent.forward, out Hit, 1000.0f, LayerMask))
            {                Vector3 Dir = Hit.point - transform.position;                Dir.Normalize();                LockedDir = Dir;            }        }        Vector3 TargetLockPos = transform.position + LockedDir * 1.0f;        if (LockedTargetObject)
        {
            // Check For Decoy
            // Does this target have a Decoy?
            Transform Decoy = null;
            if (LockedTargetObject.GetComponent<DecoyScript>()
                && LockedTargetObject.GetComponent<DecoyScript>().GetDecoy())
            {
                Decoy = LockedTargetObject.GetComponent<DecoyScript>().GetDecoy();
                if (Decoy && ConversionCamera)
                {
                    Vector3 DecoyDir = Decoy.transform.position - transform.position;
                    float DecoyDist = DecoyDir.magnitude;
                    DecoyDir.Normalize();
                    Vector3 ScreenPos = ConversionCamera.WorldToScreenPoint(Decoy.transform.position);

                    // Check this is on screen.
                    float ScreenPercentToCheck = 0.9f;
                    if (ScreenPos.x < 0 + ConversionCamera.pixelWidth * (1.0f - ScreenPercentToCheck) ||
                        ScreenPos.y < 0 + ConversionCamera.pixelHeight * (1.0f - ScreenPercentToCheck) ||
                        ScreenPos.x > ConversionCamera.pixelWidth * ScreenPercentToCheck ||
                        ScreenPos.y > ConversionCamera.pixelHeight * ScreenPercentToCheck)
                        Decoy = null;

                    // Ensure the object is in front of us
                    if (Vector3.Dot(transform.forward, DecoyDir) < 0)
                        Decoy = null;

                    // only accept this target if it is visible.
                    RaycastHit Hit;
                    int LayerMask = ((1 << 2) | (1 << 4) | (1 << 8) | (1 << 10));
                    LayerMask = ~LayerMask;
                    if (Physics.Raycast(transform.position, DecoyDir, out Hit, DecoyDist, LayerMask))
                    {
                        Decoy = null;
                    }
                }
            }

            TargetLockPos = Decoy?Decoy.position:LockedTargetObject.transform.position;        }        // Do something for each Lock mode        if (LockOnState == eLockOnState.eLockingOn)        {            ElapsedLockOnTime += Time.deltaTime;            if (ElapsedLockOnTime >= LockOnTime)                LockOnState = eLockOnState.eLockedOn;        }        float ElapsedRatio = ElapsedLockOnTime / LockOnTime;        if (LockOnState == eLockOnState.eLockedOn)        {            ElapsedRatio = 1.0f;        }        transform.LookAt(TargetLockPos, transform.parent.transform.up);        transform.rotation = Quaternion.Slerp(OldRotation, transform.rotation, ElapsedRatio);    }    public void SetLockedTargetObject(Transform LockedObject)
    {
        if (!DoLockOn)
            return;        if (LockedTargetObject == LockedObject)            return;        LockedTargetObject = LockedObject;        OldRotation = transform.rotation;        LockOnState = eLockOnState.eLockingOn;        ElapsedLockOnTime = 0.0f;        CrosshairScript Crosshair = GetComponent<CrosshairScript>();        if (Crosshair)        {            Crosshair.SetTarget(LockedObject, LockOnTime);        }    }    public Transform GetLockedTargetObject() {        return LockedTargetObject;    }

    IEnumerator UpdateCameraList()
    {
        while (true)
        {
            Camera[] AllCameras;
            AllCameras = FindObjectsOfType(typeof(Camera)) as Camera[];
            foreach (Camera go in AllCameras)
            {
                if (go.enabled)
                {
                    ConversionCamera = go;
                }
            }

            yield return new WaitForSeconds(2.0f);
        }
    }}