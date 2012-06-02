using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipAnimationScript : MonoBehaviour {
	
	public GameObject BodyToAnimate;
	
	AnimationState RollLeft;
	AnimationState RollRight;
	
	AnimationState TurnLeft;
	AnimationState TurnRight;
	
	AnimationState TurnUp;
	AnimationState TurnDown;
	
	AnimationState StrafeLeft;
	AnimationState StrafeRight;
	
	AnimationState Forwards;
	
	AnimationState StrafeUp;
	AnimationState StrafeDown;

    void Awake()
    {
		RollLeft = BodyToAnimate.animation["RollLeft"];
		if(RollLeft)
			SetupAdditive(RollLeft);
		
		RollRight = BodyToAnimate.animation["RollRight"];
		if(RollRight)
			SetupAdditive(RollRight);
		
		TurnLeft = BodyToAnimate.animation["TurnLeft"];
		if(TurnLeft)
			SetupAdditive(TurnLeft);
		
		TurnRight = BodyToAnimate.animation["TurnRight"];
		if(TurnRight)
			SetupAdditive(TurnRight);
		
		TurnUp = BodyToAnimate.animation["TurnUp"];
		if(TurnUp)
			SetupAdditive(TurnUp);
		
		TurnDown = BodyToAnimate.animation["TurnDown"];
		if(TurnDown)
			SetupAdditive(TurnDown);
		
		StrafeLeft = BodyToAnimate.animation["StrafeLeft"];
		if(StrafeLeft)
			SetupAdditive(StrafeLeft);
		
		StrafeRight = BodyToAnimate.animation["StrafeRight"];
		if(StrafeRight)
			SetupAdditive(StrafeRight);
		
		StrafeUp = BodyToAnimate.animation["StrafeUp"];
		if(StrafeUp)
			SetupAdditive(StrafeUp);
		
		StrafeDown = BodyToAnimate.animation["StrafeDown"];
		if(StrafeDown)
			SetupAdditive(StrafeDown);
		
		Forwards = BodyToAnimate.animation["Forwards"];
		if(Forwards)
			SetupAdditive(Forwards);
		
   		BodyToAnimate.animation["Idle"].wrapMode = WrapMode.Loop;
   		BodyToAnimate.animation.Play("Idle");
    }
	
	void SetupAdditive(AnimationState Additive)
	{
		// New Layers for additive blending
		Additive.layer = 10;
		Additive.blendMode = AnimationBlendMode.Additive;
		Additive.wrapMode = WrapMode.ClampForever;
		Additive.enabled = true;
		Additive.weight = 1.0f;
		Additive.normalizedSpeed = 0.0f;
	}
	
	public void ProcessMovementInput(Vector3 Movement)
	{	
		float Strafe = Movement.x;
		float Forward = Movement.z;
		float Vertical = Movement.y;
		
		if(StrafeLeft)
		{
			StrafeLeft.normalizedTime = -Strafe;
		}
		if(StrafeRight)
		{
			StrafeRight.normalizedTime = Strafe;
		}
		
		if(Forwards)
		{
			Forwards.normalizedTime = Forward;
		}
		
		if(StrafeUp)
		{
			StrafeUp.normalizedTime = Vertical;
		}
		if(StrafeDown)
		{
			StrafeDown.normalizedTime = -Vertical;
		}
	}
	
	public void ProcessRotationInput(float X, float Y, float Z)
	{
		float TurnY = Y;
        float TurnX = X;
		
		if(RollLeft && RollRight)
		{
			float Roll = Z;
		
			if (Roll > 0)
			{
				RollLeft.normalizedTime = 0;
				RollRight.normalizedTime = Roll;
			}
			else
			{
				RollLeft.normalizedTime = -Roll;
				RollRight.normalizedTime = 0;
			}
		}
		
		if(TurnLeft)
		{
			TurnLeft.normalizedTime = -TurnY;
		}
		if(TurnRight)
		{
			TurnRight.normalizedTime = TurnY;
		}
		
		if(TurnUp)
		{
			TurnUp.normalizedTime = -TurnX;
		}
		if(TurnDown)
		{
			TurnDown.normalizedTime = TurnX;
		}
	}
}
