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
	
	private float diveThrottle;

    public float Dive { get { return diveThrottle; } }

    private bool stopingDive=false;	
	
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
	
	private float deltaDiveThrottle;
	
	private float deltaRollSpeed;
	
	private float deltaYawSpeed;

	private float deltaPitchSpeed;

	public float maxForwardThrottle;
	
	public float maxBackwardThrottle;
	
	public float throttleStep;
	
	public float maxDiveThrottle;
	
	public float diveStep;
	
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
    void Update()
    {
        if (controller!=null&&controller.IsFullStop())
        {
            stopingPitch = true;
            stopingRoll = true;
            stopingYaw = true;
            stopingThrottle = true;
            stopingDive = true;
        }
    }
	
	void FixedUpdate(){
		if(controller==null){
            ApplyMovemment();
			return;
		}
       
       
        if (maxBackwardThrottle != 0 && maxForwardThrottle != 0)
        {
            deltaForwardThrottle = controller.GetForwardThrottle();
            if (deltaForwardThrottle == 0 && stopingThrottle)
            {
                float sign = Mathf.Sign(forwardThrottle);
                deltaForwardThrottle = -sign * throttleStep;
                forwardThrottle += deltaForwardThrottle * Time.fixedDeltaTime;
                if (Mathf.Sign(forwardThrottle) != sign)
                {
                    forwardThrottle = 0;
                }

            }
            else
            {
                stopingThrottle = false;
                deltaForwardThrottle *= throttleStep;
                forwardThrottle += deltaForwardThrottle * Time.fixedDeltaTime;

            }
            forwardThrottle = Mathf.Clamp(forwardThrottle, maxBackwardThrottle, maxForwardThrottle);
               
        }
        if (maxDiveThrottle != 0)
        {
            deltaDiveThrottle = controller.GetDiveThrottle();
            if (deltaDiveThrottle == 0 && stopingDive)
            {
                float sign = Mathf.Sign(diveThrottle);
                deltaDiveThrottle = -sign * throttleStep;
                diveThrottle += deltaDiveThrottle * Time.fixedDeltaTime;
                if (Mathf.Sign(diveThrottle) != sign)
                {
                    diveThrottle = 0;
                }

            }
            else
            {
                stopingDive = false;
                deltaDiveThrottle *= throttleStep;
                diveThrottle += deltaDiveThrottle * Time.fixedDeltaTime;

            }
            diveThrottle = Mathf.Clamp(diveThrottle, -maxDiveThrottle, maxDiveThrottle);
               

        }
            
            if (maxRollSpeed != 0)
            {
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
                else
                {
                    stopingRoll = false;
                    deltaRollSpeed *= rollStep;
                    rollSpeed += deltaRollSpeed * Time.fixedDeltaTime;
                }
                rollSpeed = Mathf.Clamp(rollSpeed, -maxRollSpeed, maxRollSpeed);
             

            }
            if (maxYawSpeed != 0)
            {
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
                yawSpeed = Mathf.Clamp(yawSpeed, -maxYawSpeed, maxYawSpeed);
               
            }
            if (maxPitchSpeed != 0)
            {
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
                pitchSpeed = Mathf.Clamp(pitchSpeed, -maxPitchSpeed, maxPitchSpeed);
               
            }
            ApplyMovemment();
           
        
		
			
		
	}

    public void ApplyMovemment()
    {
        Vector3 forward = Vector3.forward * forwardThrottle;
        if (force.relativeForce != forward)
        {
            force.relativeForce = forward;
        }
        Vector3 up = Vector3.up * diveThrottle;
        if (force.force != up)
        {
            force.force = up;
        }
        Quaternion summaryRoattion = Quaternion.identity;
        summaryRoattion *= Quaternion.AngleAxis(rollSpeed, Vector3.forward);
        summaryRoattion *= Quaternion.AngleAxis(yawSpeed, Vector3.up);
        summaryRoattion *= Quaternion.AngleAxis(pitchSpeed, Vector3.right);
        myTransform.rotation *= summaryRoattion;
    }
  
}