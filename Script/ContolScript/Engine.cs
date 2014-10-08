using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Engine : ControlledObject{

	public Rigidbody mRigidBody;
	
	private ConstantForce force;
	
	private float forwardThrottle;

    public float Throttle { get { return forwardThrottle; } }

    private bool stopingThrottle=false;
	
	private float rollSpeed;

    public float Roll { get { return rollSpeed; } }

    private bool stopingRoll = false;
	
	private float yawSpeed;

    public float Yaw { get { return yawSpeed; } }

    private bool stopingYaw = false;

	private float pitchSpeed;

    public float Pitch { get { return pitchSpeed; } }

    private bool stopingPitch = false;
	
	private float deltaForwardThrottle;
	
	private float deltaRollSpeed;
	
	private float deltaYawSpeed;

	private float deltaPitchSpeed;

	public float maxForwardThrottle;
	
	public float maxBackwardThrottle;
	
	public float throttleStep;
	
	public float maxYawSpeed;
	
	public float yawStep;

	public float maxPitchSpeed;
	
	public float pitchStep;

    public float maxRollSpeed;

    public float rollStep;


    Transform myTransform;
	void Awake(){
		force =mRigidBody.GetComponent<ConstantForce>();
        myTransform = mRigidBody.transform;
        FindObjectOfType<EngineGui>().SetEngine(this);  
	}
	
	void FixedUpdate(){
        if (controller.IsFullStop())
        {
            stopingPitch = true;
            stopingRoll = true;
            stopingYaw = true;
            stopingThrottle = true;
        }
        else
        {
            deltaForwardThrottle = controller.GetForwardThrottle();
            if (deltaForwardThrottle == 0 && stopingThrottle)
            {
                float sign =Mathf.Sign(forwardThrottle);
                deltaForwardThrottle  =-  sign* throttleStep;
                forwardThrottle += deltaForwardThrottle * Time.fixedDeltaTime;
                if (Mathf.Sign(forwardThrottle)!=sign)
                {
                    forwardThrottle = 0;
                }

            }else{
                stopingThrottle = false;
                deltaForwardThrottle *= throttleStep;
                forwardThrottle += deltaForwardThrottle * Time.fixedDeltaTime;
               
            }
            Vector3 forward = Vector3.forward * Mathf.Clamp(forwardThrottle, maxBackwardThrottle, maxForwardThrottle);
            if (force.relativeForce != forward)
            {
                force.relativeForce = forward;
            }

              Quaternion summaryRoattion  = Quaternion.identity;

            deltaRollSpeed = controller.GetRollSpeed();
            if (deltaRollSpeed == 0 && stopingRoll)
            {
                float sign = Mathf.Sign(rollSpeed);
                deltaRollSpeed = -sign * rollStep;
                rollSpeed += deltaRollSpeed * Time.fixedDeltaTime;
                if (Mathf.Sign(rollSpeed) != sign)
                {
                    rollSpeed = 0;
                }
            }
            else {
                stopingRoll = false;
                deltaRollSpeed *= rollStep;
                rollSpeed += deltaRollSpeed * Time.fixedDeltaTime;
            }
            summaryRoattion *= Quaternion.AngleAxis(Mathf.Clamp(rollSpeed, -maxRollSpeed, maxRollSpeed), Vector3.forward);



            deltaYawSpeed = controller.GetYawSpeed();
            if (deltaYawSpeed == 0 && stopingYaw)
            {
                float sign = Mathf.Sign(rollSpeed);
                deltaYawSpeed = -sign * yawStep;
                yawSpeed += deltaYawSpeed * Time.fixedDeltaTime;
                if (Mathf.Sign(yawSpeed) != sign)
                {
                    yawSpeed = 0;
                }
            }
            else
            {
                stopingYaw = false;
                deltaYawSpeed *= yawStep;
                yawSpeed += deltaYawSpeed * Time.fixedDeltaTime;
            }
            summaryRoattion *= Quaternion.AngleAxis(Mathf.Clamp(yawSpeed, -maxYawSpeed, maxYawSpeed), Vector3.up);

            deltaPitchSpeed = controller.GetPitchSpeed();
            if (deltaPitchSpeed == 0 && stopingPitch)
            {
                float sign = Mathf.Sign(pitchSpeed);
                deltaPitchSpeed = -sign * pitchStep;
                pitchSpeed += deltaPitchSpeed * Time.fixedDeltaTime;
                if (Mathf.Sign(pitchSpeed) != sign)
                {
                    pitchSpeed = 0;
                }
            }
            else
            {
                stopingPitch = false;
                deltaPitchSpeed *= pitchStep;
                pitchSpeed += deltaPitchSpeed * Time.fixedDeltaTime;
            }
            summaryRoattion *= Quaternion.AngleAxis(Mathf.Clamp(pitchSpeed, -maxPitchSpeed, maxPitchSpeed), Vector3.right);
            myTransform.rotation *= summaryRoattion;
           
        }
		
			
		
	}
  
}