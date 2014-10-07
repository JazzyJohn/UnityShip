using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic

public class BaseMountObject : ControlledObject{

	public float minYaw;
	
	public float maxYaw;
	
	public bool yawConstraint;
	
	public float minPitch;
	
	public float maxPitch;
	
	public float rotateSpeed;

	protected Tranform mTransform;
	
	Quaternion originalRotation;
	
	protected underControl = true;
		
	void Awake(){
		mTransform = Transform;
	}	

		
	void FixedUpdate(){
		if(underControl){
			Vector3 target  = controller.GetLookTarget();
			Vector3 newRotation =(mTransform.parent.rotation*Quaternion.LookRotation(targetPos - transform.position)).eulerAngles;
			
			
			pitch = ClampAngle(newRotation.x, minPitch, maxPitch);
			if(yawConstraint){
				yaw = ClampAngle(newRotation.y, minYaw, maxYaw);
			}

			
				
			Quaternion yawQuaternion = Quaternion.AngleAxis(yaw, Vector3.up);
			Quaternion pitchQuaternion = Quaternion.AngleAxis(pitch, Vector3.left);

			mTransform.localRotation = Quaternion.Slerp(mTransform.localRotation,pitchQuaternion * yawQuaternion,Time.fisedDeltaTime*rotateSpeed);
		}
	}
	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
	public void SetControl(bool control){
		underControl= control;
	}
	public void ToggleControl(){
		underControl= !underControl;
	}
	public void Activate(){
	
	}
}