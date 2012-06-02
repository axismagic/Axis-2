using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Control/Rotation")]
public class Rotation : MonoBehaviour
{
    public Dictionary<int, Quaternion> RotHistory = new Dictionary<int, Quaternion>();

    float AngleErrorDiff = 0.0f;
    Quaternion RotationDiff = Quaternion.identity;

    public int NumberRotationsBeforSync = 30;
    public int MaxInputsBeforeBuffer = 10;
    int InputsHandled = 0;

    public AnimationCurve HorizontalAccelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    public float MaxHorizontalVelocity = 1.0f;
    public float HorizontalAccelerationTime = 1.0f;

    public AnimationCurve VerticalAccelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    public float MaxVerticalVelocity = 1.0f;
    public float VerticalAccelerationTime = 1.0f;

    public AnimationCurve RollAccelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    public float RollMaxVelocity = 1.0f;
    public float RollAccelerationTime = 1.0f;

    public Vector3 BufferedInput = Vector3.zero;

    //Vector3 PrevDir = Vector3.zero;
    Vector3 Acceleration = Vector3.zero;

    Vector3 Velocity = Vector3.zero;

    float RollVelocity = 0.0f;
    float RollAcceleration = 0.0f;

    bool LockCursor = false;

    bool Accelerating = false;

    bool Sync = false;

    public float Sensitivity = 0.5f;

    private ShipCustomisationScript Customisation;
    private GUINetConnection NetConnection;

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
        RotHistory.Clear();
    }

    void Start()
    {
        NetConnection = FindObjectOfType(typeof(GUINetConnection)) as GUINetConnection;
        Customisation = FindObjectOfType(typeof(ShipCustomisationScript)) as ShipCustomisationScript;

        if (!Application.isEditor)
            LockCursor = true;

        Debug.Log("Starting NetSyncRot");
        StartCoroutine("NetSyncRot");
    }

    void OnDisable()
    {
        Debug.Log("Stopping NetSyncRot");
        StopCoroutine("NetSyncRot");
    }

    void Update()
    {
        // We are not the owner of this ship, do not process the update.
        if (!GetComponent<NetOwnership>().IsOwner(Network.player))
        {
            return;
        }
    }

    void FixedUpdate()
    {
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

        if (Input.GetButtonDown("LockMouseToggle"))
            LockCursor = !LockCursor;

        float RotateY = Input.GetAxis("LookX") > 0.01f || Input.GetAxis("LookX") < -0.01f ? Input.GetAxis("LookX") : (Input.GetAxis("MouseLookX") * Customisation.Sensitivity);
        float RotateX = Input.GetAxis("LookY") > 0.01f || Input.GetAxis("LookY") < -0.01f ? Input.GetAxis("LookY") : (Input.GetAxis("MouseLookY") * Customisation.Sensitivity);
        float RotateZ = Input.GetAxis("Roll");

        bool MouseInput = false;
        if (Input.GetAxis("MouseLookX") > 0.01f || Input.GetAxis("MouseLookX") < -0.01f ||
            Input.GetAxis("MouseLookY") > 0.01f || Input.GetAxis("MouseLookY") < -0.01f)
            MouseInput = true;

        RotateY *= 100.0f;
        RotateX *= 100.0f;

        if (Customisation.InvertY)
            RotateX *= -1.0f;

        if (NetConnection.ShowingDisconnect())
            Screen.lockCursor = false;
        else
            Screen.lockCursor = LockCursor;

        // The Rotation update happens on the server, only the server can own this netview		
        LocalHandleInput((int)(RotateX), (int)(RotateY), (int)(RotateZ), MouseInput);
    }

    void LocalHandleInput(int RotateX, int RotateY, int RotateZ, bool MouseInput)
    {
        // We buffer our position for error checking later, also, if we buffer the position, tell the server to buffer too.
        bool BufferPos = false;
        bool ClearBuffer = false;
        int CurrentBufferSize = RotHistory.Count;

        InputsHandled++;
        if (InputsHandled > MaxInputsBeforeBuffer)
        {
            InputsHandled = 0;
            BufferPos = true;
        }

        // If we are clearing the buffer, buffer the next Pos.
        if (RotHistory.Count == 0)
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
            ProcessInput(RotateX, RotateY, RotateZ, MouseInput, BufferPos, ClearBuffer, CurrentBufferSize);
        }
        else
        {
            ProcessInput(RotateX, RotateY, RotateZ, MouseInput, BufferPos, ClearBuffer, CurrentBufferSize);

            if (networkView)
                networkView.RPC("SendProcessRotationalInput", RPCMode.Server, RotateX, RotateY, RotateZ, MouseInput, BufferPos, ClearBuffer, CurrentBufferSize);
        }
    }

    void ProcessInput(int RotateX, int RotateY, int RotateZ, bool MouseInput, bool BufferPos, bool ClearBuffer, int Key)
    {
        float LinearRange = 0.95f;

        float XAccelRate = RotateX / 100.0f;
        float YAccelRate = RotateY / 100.0f;
        float ZAccelRate = RotateZ;

        // If it's not mouse Input, we do some acceleration and deceleration.
        if (!MouseInput)
        {
            Vector2 Dir = new Vector2(XAccelRate, YAccelRate);
            float Mag = Dir.magnitude;

            if (RotateX == 0 && RotateY == 0)
            {
                if (Velocity.magnitude < 0.1f)
                    Accelerating = false;

                Acceleration = Vector3.zero;
            }

            if (Dir.magnitude > 0.01f)
            {
                //                 if (Vector3.Dot(PrevDir, PrevDir) < 0.5f)
                //                 {
                //                     Acceleration = Vector3.zero;
                //                     Velocity = Vector3.zero;
                //                 }

                // ReNormalise
                if (Dir.magnitude < LinearRange && !Accelerating)
                {
                    Vector2 NewDir = Dir;

                    NewDir.x *= NewDir.x;
                    NewDir.y *= NewDir.y;

                    if (Dir.x < 0.0f)
                        NewDir.x *= -1.0f;
                    if (Dir.y < 0.0f)
                        NewDir.y *= -1.0f;

                    // One to One Stick mapping
                    Velocity = NewDir;
                    Acceleration.x = NewDir.magnitude;
                }
                else
                {
                    // Renormalise
                    Mag = (Mag - LinearRange) / (1.0f - LinearRange);

                    float XMaxAccel = 0.075f;

                    float XSampleValue = HorizontalAccelerationCurve.Evaluate(Mag) * XMaxAccel;

                    Acceleration.x += XSampleValue;

                    Velocity.x = Dir.x * Acceleration.x;
                    Velocity.y = Dir.y * Acceleration.x;

                    if (!Accelerating && Dir.magnitude > LinearRange)
                        Accelerating = true;

                    // Cap Velocity
                    float XMaxVel = 3.0f;
                    float YMaxVel = 3.0f;
                    Velocity.x = Mathf.Min(Velocity.x, XMaxVel);
                    Velocity.x = Mathf.Max(Velocity.x, -XMaxVel);
                    Velocity.y = Mathf.Min(Velocity.y, YMaxVel);
                    Velocity.y = Mathf.Max(Velocity.y, -YMaxVel);
                }
            }
            else
            {
                Velocity *= 0.9f;
            }

            //PrevDir = Dir;

            // Set the Velocity from the input.
            XAccelRate = Velocity.x;
            YAccelRate = Velocity.y;
        }

        if (ZAccelRate > 0.0f || ZAccelRate < 0.0f)
        {
            RollAcceleration += 0.05f;

            float ZSampleValue = HorizontalAccelerationCurve.Evaluate(RollAcceleration) * RollMaxVelocity;
            RollVelocity = ZAccelRate < 0.0f ? ZSampleValue * -1.0f : ZSampleValue * 1.0f;
        }
        else
        {
            RollVelocity *= 0.9f;
            RollAcceleration = 0.0f;
        }

        ZAccelRate = RollVelocity;

        transform.Rotate(XAccelRate, YAccelRate, ZAccelRate);

        foreach (ShipAnimationScript ShipAnimation in ShipAnims)
        {
            ShipAnimation.ProcessRotationInput(XAccelRate / MaxHorizontalVelocity, YAccelRate / MaxVerticalVelocity, ZAccelRate / RollMaxVelocity);
        }

        if (ClearBuffer)
        {
            RotHistory.Clear();
        }

        // If this is a buffer position packet, buffer the position
        if (BufferPos)
        {
            RotHistory.Add(Key, transform.rotation);
        }

        if (Network.isServer)
        {
            if (networkView)
                networkView.RPC("ClientUpdatePlayerRotate", RPCMode.Others, transform.rotation);
        }
    }

    [RPC]
    public void SendProcessRotationalInput(int RotateX, int RotateY, int RotateZ, bool MouseInput, bool BufferPos, bool ClearBuffer, int CurrentBufferSize, NetworkMessageInfo Info)
    {
        ProcessInput(RotateX, RotateY, RotateZ, MouseInput, BufferPos, ClearBuffer, CurrentBufferSize);

        if (networkView)
            networkView.RPC("ClientUpdatePlayerRotate", RPCMode.Others, transform.rotation);
    }

    [RPC]
    public void ClientUpdatePlayerRotate(Quaternion Rotation)
    {
        // If we own this ship, don't update our info (ClientSide Prediction)
        if (GetComponent<NetOwnership>().IsOwner(Network.player))
            return;

        transform.rotation = Rotation;
    }

    // This function calculates the error in our position and updates it
    IEnumerator NetSyncRot()
    {
        while (true)
        {
            if (Network.isServer)
            {
                int Key = -1;
                foreach (KeyValuePair<int, Quaternion> Rot in RotHistory)
                {
                    if (Rot.Key >= NumberRotationsBeforSync)
                    {
                        Key = Rot.Key;
                        break;
                    }
                }

                if (Key != -1)
                {
                    Quaternion Rot = RotHistory[Key];
                    if (networkView)
                        networkView.RPC("ServerSendRotateSyncInfo", RPCMode.Others, Key, Rot.x, Rot.y, Rot.z, Rot.w);
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    [RPC]
    public void ServerSendRotateSyncInfo(int Key, float X, float Y, float Z, float W, NetworkMessageInfo Info)
    {
        // If we don't have control over this ship, do not process this information
        if (!GetComponent<NetOwnership>().IsOwner(Network.player))
            return;

        if (!RotHistory.ContainsKey(Key))
        {
            return;
        }

        // Calculate the error
        Quaternion LocalRot = (RotHistory[Key]);
        Quaternion ServerRot = new Quaternion(X, Y, Z, W);

        AngleErrorDiff = Quaternion.Angle(ServerRot, LocalRot);
        RotationDiff = ServerRot * Quaternion.Inverse(LocalRot);

        if (AngleErrorDiff > 3.0f)
        {
            AngleErrorDiff = 0.0f;
            transform.rotation = ServerRot;
        }

        if (AngleErrorDiff > 0.0001f)
            transform.rotation *= Quaternion.Slerp(Quaternion.identity, RotationDiff, 0.35f);

        RotHistory.Clear();
    }
}
