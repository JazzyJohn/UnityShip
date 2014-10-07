using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic

public class Engine : ControlledObject{

	public Rigidbody mRigidBody;
	
	private ConstantForce force;
	
	private float forwardThrottle;
	
	private float rollSpeed;
	
	private float yawSpeed;

	private float pitchSpeed;
	
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
	
	private float maxRollSpeed;

	private float rollStep;
	
	void Awake(){
		force =mRigidBody.getComponent<ConstantForce>();
	
	}
	
	void FixedUpdate(){
		deltaForwardThrottle = controller.GetForwardThrottle();
		deltaForwardThrottle *=throttleStep;
		forwardThrottle +=deltaForwardThrottle*Time.fixedDeltaTime; 
		Vector3 forward =  Vector3.forward *Mathf.Clamp(forwardThrottle,maxBackwardThrottle,maxForwardThrottle);
		if(force.relativeForce 	!=forward){
			force.relativeForce =forward;
		}
			
		Vector3 summaryTorque = Vector3.zero;
		
		deltaRollSpeed = controller.GetRollSpeed();
		deltaRollSpeed *=rollStep;
		rollSpeed +=deltaRollSpeed*Time.fixedDeltaTime;
		
		summaryTorque+= Vector.forward* Mathf.Clamp(rollSpeed,-maxRollSpeed,maxRollSpeed);
		
		deltaYawSpeed = controller.GetRollSpeed();
		deltaYawSpeed *=yawStep;
		yawSpeed +=deltaYawSpeed*Time.fixedDeltaTime;
		
		summaryTorque+= Vector.up*Mathf.Clamp(yawSpeed,-maxYawSpeed,maxYawSpeed);
		
		deltaPitchSpeed = controller.GetRollSpeed();
		deltaPitchSpeed *=pitchStep;
		pitchSpeed +=deltaPitchSpeed*Time.fixedDeltaTime;
	
		summaryTorque+= Vector.up*Mathf.Clamp(pitchSpeed,-maxPitchSpeed,maxPitchSpeed);
		
		mRigidBody.AddRelativeTorque(summaryTorque);	
		
	}	

}