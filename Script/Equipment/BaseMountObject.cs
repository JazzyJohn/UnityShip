using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BaseMountObject : ControlledObject{

	public float minYaw;
	
	public float maxYaw;
	
	public bool yawConstraint;
	
	public float minPitch;
	
	public float maxPitch;
	
	public float rotateSpeed;

	protected Transform mTransform;
	
	Quaternion originalRotation;
	
	protected bool underControl = true;
		
	void Awake(){
		mTransform = transform;
	}	

		
	protected void FixedUpdate(){
		if(controller==null){
			return;
		}
		if(underControl){
			Vector3 target  = controller.GetLookTarget();
            Quaternion newRotation = Quaternion.LookRotation(target - mTransform.position);
            Quaternion oldRotation = mTransform.localRotation;
           // Debug.Log(newRotation);
            mTransform.rotation = newRotation;
         //   Debug.Log(mTransform.rotation.eulerAngles);
            Vector3 euler = mTransform.localRotation.eulerAngles;
            euler.x = ClampAngle(euler.x, minPitch, maxPitch);
           
            if(yawConstraint){
                euler.y = ClampAngle(euler.y, minYaw, maxYaw);
            }
            //Debug.Log(euler );
            mTransform.localRotation = Quaternion.Slerp(oldRotation, Quaternion.Euler(euler), Time.fixedDeltaTime * rotateSpeed);
            /*

			Quaternion yawQuaternion = Quaternion.AngleAxis(newRotation.eulerAngles.y, Vector3.up);
			Quaternion pitchQuaternion = Quaternion.AngleAxis(newRotation.eulerAngles.x, Vector3.left);
			Quaternion rollQuat = Quaternion.AngleAxis(newRotation.eulerAngles.z, Vector3.right);
			newRotation = pitchQuaternion * yawQuaternion * rollQuat;
			mTransform.localRotation = Quaternion.Slerp(mTransform.localRotation, newRotation, Time.fixedDeltaTime * rotateSpeed);*/
        }
	}
	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;

        if (angle > 180)
        {
            angle = angle -360 ;
        }
		return Mathf.Clamp (angle, min, max);
	}
	public void SetControl(bool control){
		underControl= control;
	}
	public void ToggleControl(){
		underControl= !underControl;
	}
	public virtual void Activate(){
	
	}
	public virtual bool CanActivate(){
		return underControl;
	}
}