using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Control/Movement")]
public class Movement : MonoBehaviour
{
    public Dictionary<int, Vector3> PosHistory = new Dictionary<int, Vector3>();

    public int NumberPositionsBeforSync = 30;
    public int MaxInputsBeforeBuffer = 10;
    int InputsHandled = 0;

    public AnimationCurve AccelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    public float MaxVelocity = 1.0f;
    public float AccelerationTime = 1.0f;

    public AnimationCurve VerticalAccelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    public float VerticalMaxVelocity = 1.0f;
    public float VerticalAccelerationTime = 1.0f;

    public Vector3 BufferedInput = Vector3.zero;

    bool Sync = false;
    bool ShouldFallUnderGravity = false;

    List<ShipAnimationScript> ShipAnims = new List<ShipAnimationScript>();
    public void AddAnimationScript(ShipAnimationScript ToAdd)
    {
        if (!ShipAnims.Contains(ToAdd))
            ShipAnims.Add(ToAdd);
    }

    public void RemoveAnimationScript(ShipAnimationScript ToRemove)
    {
        if (ShipAnims.Contains(ToRemove))
            ShipAnims.Add(ToRemove);
    }

    public void StartSync()
    {
        Sync = true;
    }
    public void StopSync()
    {
        Sync = false;
        PosHistory.Clear();
    }
    
    public void FallUnderGravity(bool Fall)
    {
        ShouldFallUnderGravity = Fall;
    }

    void Start()
    {
        Debug.Log("Starting NetSyncPos");
        StartCoroutine("NetSyncPos");
		
		Physics.IgnoreLayerCollision (8, 12, true);
    }

    void OnDisable()
    {
        Debug.Log("Stopping NetSyncPos");
        StopCoroutine("NetSyncPos");
    }

    void Update()
    {
        // We are not the owner of this ship, do not process the update.
        if (!GetComponent<NetOwnership>().IsOwner(Network.player))
        {
            return;
        }

        // Interp the error difference to Zero.
        //ErrorInterpTime += Time.deltaTime;
        //ErrorInterpTime = Math.Min(1.0f, ErrorInterpTime);
        //ErrorInterpTime = Math.Max(0.0f, ErrorInterpTime);

        //Vector3 ToAdd = ErrorDiff - (ErrorDiff * 0.9f);
        //ErrorDiff -= ToAdd;
        //transform.position += ToAdd;
    }

    void FixedUpdate()
    {
        if (ShouldFallUnderGravity)
        {
            CharacterController controller = GetComponent<CharacterController>();
            controller.Move(new Vector3(0.0f, -9.0f*Time.deltaTime, 0.0f));
        }

        // We are not the owner of this ship, do not process the update.
        if (!GetComponent<NetOwnership>().IsOwner(Network.player))
        {
            return;
        }

        HealthScript Health = GetComponent<HealthScript>();
        if (Health && !Health.IsAlive())
        {
            return;
        }

        float HorizontalAxis = Input.GetAxis("Horizontal");
        float VerticalAxis = Input.GetAxis("Vertical");
        float AtAxis = Input.GetAxis("At");

        float MoveRight = (HorizontalAxis / AccelerationTime) * Time.deltaTime;
        float MoveUp = (VerticalAxis / AccelerationTime) * Time.deltaTime;
        float MoveForward = (AtAxis / VerticalAccelerationTime) * Time.deltaTime;

        BufferedInput.x += MoveRight;
        BufferedInput.y += MoveUp;
        BufferedInput.z += MoveForward;

        // THERE MUST BE A BETTER WAY TO DO THIS MATHEMATICALLY. HOWEVER I'M AN IDIOT, AND IT DIDN'T COME TO ME IN TIME.
        // INSTEAD USE IF STATEMENTS, AND MAKE THIS TERRIBLY HIDEOUS CHUNK OF CODE.
        float RightDeceleration = 0.0f;
        if (BufferedInput.x > 0.0f && MoveRight < 0.01f)
        {
            float Dec = -(1.0f / AccelerationTime) * Time.deltaTime;
            RightDeceleration = BufferedInput.x + Dec > 0.0f ? Dec : -BufferedInput.x;
        }
        else if (BufferedInput.x < 0.0f && MoveRight > -0.01f)
        {
            float Dec = (1.0f / AccelerationTime) * Time.deltaTime;
            RightDeceleration = BufferedInput.x + Dec < 0.0f ? Dec : -BufferedInput.x;
        }
        BufferedInput.x += RightDeceleration;

        float UpDeceleration = 0.0f;
        if (BufferedInput.y > 0.0f && MoveUp < 0.01f)
        {
            float Dec = -(1.0f / VerticalAccelerationTime) * Time.deltaTime;
            UpDeceleration = BufferedInput.y + Dec > 0.0f ? Dec : -BufferedInput.y;
        }
        else if (BufferedInput.y < 0.0f && MoveUp > -0.01f)
        {
            float Dec = (1.0f / VerticalAccelerationTime) * Time.deltaTime;
            UpDeceleration = BufferedInput.y + Dec < 0.0f ? Dec : -BufferedInput.y;
        }
        BufferedInput.y += UpDeceleration;

        float ForwardDeceleration = 0.0f;
        if (BufferedInput.z > 0.0f && MoveForward < 0.01f)
        {
            float Dec = -(1.0f / AccelerationTime) * Time.deltaTime;
            ForwardDeceleration = BufferedInput.z + Dec > 0.0f ? Dec : -BufferedInput.z;
        }
        else if (BufferedInput.z < 0.0f && MoveForward > -0.01f)
        {
            float Dec = (1.0f / AccelerationTime) * Time.deltaTime;
            ForwardDeceleration = BufferedInput.z + Dec < 0.0f ? Dec : -BufferedInput.z;
        }
        BufferedInput.z += ForwardDeceleration;

        BufferedInput.x = Math.Max(Math.Min(BufferedInput.x, 1.0f), -1.0f);
        BufferedInput.y = Math.Max(Math.Min(BufferedInput.y, 1.0f), -1.0f);
        BufferedInput.z = Math.Max(Math.Min(BufferedInput.z, 1.0f), -1.0f);

        LocalHandleInput(BufferedInput.x, BufferedInput.y, BufferedInput.z);
    }

    void LocalHandleInput(float HorizontalAxis, float VerticalAxis, float AtAxis)
    {
        // We buffer our position for error checking later, also, if we buffer the position, tell the server to buffer too.
        bool BufferPos = false;
        bool ClearBuffer = false;
        int CurrentBufferSize = PosHistory.Count;

        InputsHandled++;
        if (InputsHandled > MaxInputsBeforeBuffer)
        {
            InputsHandled = 0;
            BufferPos = true;
        }

        // If we are clearing the buffer, buffer the next Pos.
        if (PosHistory.Count == 0)
        {
            ClearBuffer = true;
            BufferPos = true;
        }

        // If we are the server, no need to buffer the position, or clear the position
        if (Network.isServer || !Sync)
        {
            BufferPos = false;
            ClearBuffer = false;
        }

        // If we are not the server, we send the input to the server and also handle the input locally
        if (Network.isServer)
        {
            ProcessInput(HorizontalAxis, VerticalAxis, AtAxis, BufferPos, ClearBuffer, CurrentBufferSize);
        }
        else
        {
            ProcessInput(HorizontalAxis, VerticalAxis, AtAxis, BufferPos, ClearBuffer, CurrentBufferSize);

            if (networkView)
                networkView.RPC("SendProcessInput", RPCMode.Server, HorizontalAxis, VerticalAxis, AtAxis, BufferPos, ClearBuffer, CurrentBufferSize);
        }
    }

    void ProcessInput(float HorizontalAxis, float VerticalAxis, float AtAxis, bool BufferPos, bool ClearBuffer, int Key)
    {
        CharacterController controller = GetComponent<CharacterController>();
        
        float SpeedMod = 0.0f;
        Classes Class = GetComponent<Classes>();
        if(Class)
        	SpeedMod = Class.GetClassSpd();
        	
        float ModifiedMaxVelocity = MaxVelocity + SpeedMod;
        float ModifiedMaxVerticalVelocity = VerticalMaxVelocity + SpeedMod;

        float MoveRight = HorizontalAxis;
        float MoveUp = VerticalAxis;
        float MoveForward = AtAxis;

        float XAccelRate = 0.0f;
        if (MoveRight > 0.0f)
        {
            XAccelRate = AccelerationCurve.Evaluate(MoveRight) * ModifiedMaxVelocity;
        }
        else
        {
            XAccelRate = AccelerationCurve.Evaluate(-1.0f * MoveRight) * ModifiedMaxVelocity;
            XAccelRate *= -1.0f;
        }

        float YAccelRate = 0.0f;
        if (MoveUp > 0.0f)
        {
            YAccelRate = VerticalAccelerationCurve.Evaluate(MoveUp) * ModifiedMaxVerticalVelocity;
        }
        else
        {
            YAccelRate = VerticalAccelerationCurve.Evaluate(-1.0f * MoveUp) * ModifiedMaxVerticalVelocity;
            YAccelRate *= -1.0f;
        }

        float ZAccelRate = 0.0f;
        if (MoveForward > 0.0f)
        {
            ZAccelRate = AccelerationCurve.Evaluate(MoveForward) * ModifiedMaxVelocity;
        }
        else
        {
            ZAccelRate = AccelerationCurve.Evaluate(-1.0f * MoveForward) * ModifiedMaxVelocity;
            ZAccelRate *= -1.0f;
        }

        Vector3 Velocity = new Vector3(XAccelRate, YAccelRate, ZAccelRate);
        Vector3 localVel = transform.TransformDirection(Velocity);
        controller.Move(localVel);
		
        Vector3 AnimRate = new Vector3(XAccelRate / ModifiedMaxVelocity, YAccelRate / ModifiedMaxVerticalVelocity, ZAccelRate / ModifiedMaxVelocity);
        foreach (ShipAnimationScript ShipAnimation in ShipAnims)
		{
			ShipAnimation.ProcessMovementInput(AnimRate);
		}

        if (ClearBuffer)
        {
            PosHistory.Clear();
        }

        // If this is a buffer position packet, buffer the position
        if (BufferPos)
        {
            PosHistory.Add(Key, transform.position);
        }

        if (Network.isServer)
        {
            if (networkView)
                networkView.RPC("ClientUpdatePlayerMove", RPCMode.Others, transform.position);
        }
    }

    [RPC]
    public void SendProcessInput(float MoveRight, float MoveUp, float MoveForward, bool BufferPos, bool ClearBuffer, int CurrentBufferSize, NetworkMessageInfo Info)
    {
        ProcessInput(MoveRight, MoveUp, MoveForward, BufferPos, ClearBuffer, CurrentBufferSize);
    }

    [RPC]
    public void ClientUpdatePlayerMove(Vector3 Position)
    {
        // If we own this ship, don't update our info (ClientSide Prediction)
        if (GetComponent<NetOwnership>().IsOwner(Network.player))
            return;

        transform.position = Position;
    }

    // This function calculates the error in our position and updates it
    IEnumerator NetSyncPos()
    {
        while (true)
        {
            if (Network.isServer)
            {
                int Key = -1;
                foreach (KeyValuePair<int, Vector3> Pos in PosHistory)
                {
                    if (Pos.Key >= NumberPositionsBeforSync)
                    {
                        Key = Pos.Key;
                        break;
                    }
                }

                if (Key != -1)
                {
                    Vector3 Pos = PosHistory[Key];
                    if (networkView)
                        networkView.RPC("ServerSendMoveSyncInfo", RPCMode.Others, Key, Pos.x, Pos.y, Pos.z);
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    [RPC]
    public void ServerSendMoveSyncInfo(int Key, float X, float Y, float Z, NetworkMessageInfo Info)
    {
        // If we don't have control over this ship, do not process this information
        if (!GetComponent<NetOwnership>().IsOwner(Network.player))
            return;

        // Calculate the error
        if (!PosHistory.ContainsKey(Key))
            return;

        Vector3 LocalPos = (PosHistory[Key]);
        Vector3 ServerPos = new Vector3(X, Y, Z);

        Vector3 Diff = ServerPos - LocalPos;

        transform.position += Vector3.Slerp(Vector3.zero, Diff, 0.35f);
		
        if (Diff.sqrMagnitude > 25.0f)
        {
            transform.position = ServerPos;
        }

        PosHistory.Clear();
    }
}
