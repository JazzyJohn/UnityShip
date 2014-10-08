using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour,Controller{
	
	List<ControlledObject> allMyObjects =new List<ControlledObject>();
	
	List<BaseMountObject> allUseableObject =new List<BaseMountObject>();
    public Ship myShip;

    public float aimRange;

	Vector3 lookTarget = Vector3.zero;
	Transform curLookTarget;
	void Update(){
		UpdateLook();
	}
    public bool IsFullStop()
    {
        return InputManager.instance.GetButtonDown("FullStop");
    }
    public float GetForwardThrottle(){
		return InputManager.instance.GetMouseAxis("ForwardThrottle") ;
	}

	public float GetRollSpeed(){
        return InputManager.instance.GetMouseAxis("RollSpeed");
	}
	
	public float GetYawSpeed(){
        return InputManager.instance.GetMouseAxis("YawSpeed");
	}
	
	public float GetPitchSpeed(){
        return InputManager.instance.GetMouseAxis("PitchSpeed");
	}
	
	public Vector3 GetLookTarget(){
		return lookTarget;
	}

    public void InitObj(ControlledObject obj)
    {
		allMyObjects.Add(obj);
		BaseMountObject mounted = obj as BaseMountObject;
		if(mounted!=null){
			allUseableObject.Add(mounted);
		}
        obj.Init(this);
       
	}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(lookTarget, 1);
    }
	void UpdateLook(){
			Camera maincam = Camera.main;
			Ray centerRay = maincam.ViewportPointToRay(new Vector3(.5f, 0.5f, 1f));

			Vector3 targetpoint = Vector3.zero;
			bool wasHit = false;
			float magnitude = aimRange;
			float range = aimRange;
			foreach (RaycastHit hitInfo in Physics.RaycastAll(centerRay, aimRange))
			{
				if ( hitInfo.transform.IsChildOf(myShip.myTransform) || hitInfo.collider.isTrigger)
				{
					continue;
				}
                
				//
				//Debug.DrawRay(centerRay.origin,centerRay.direction);


				if (hitInfo.distance < magnitude)
				{
					magnitude = hitInfo.distance;
				}
				else
				{
					continue;
				}
				wasHit = true;
				targetpoint = hitInfo.point;
				curLookTarget = hitInfo.transform;
				//Debug.Log (curLookTarget);



			}

			if (!wasHit)
			{
				//Debug.Log("NO HIT");
				curLookTarget = null;
//				animator.WeaponDown(false);
				targetpoint = maincam.transform.forward * aimRange + maincam.ViewportToWorldPoint(new Vector3(.5f, 0.5f, 1f));
			}
		
			lookTarget = targetpoint;
	}
	public void ActiveateObject(int index){
		allUseableObject[index].Activate();
	}
	public void ToggleControl(int index){
		allUseableObject[index].ToggleControl();
	}
}