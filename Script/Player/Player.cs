using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic

public class Player : MonoBehaviour,Controller{
	
	List<ControlledObject> allMyObjects =new List<ControlledObject>();
	
	List<BaseMountObject> allUseableObject =new List<BaseMountObject>();
	Ship myShip;

	Vector3 lookTarget = Vector3.zero;
	Transform curLookTarget;
	void Update(){
		UpdateLook();
	}
	
    public float GetForwardThrottle(){
		InputManager.instance.GetMouseAxis("ForwardThrottle") ;
	}

	public float GetRollSpeed(){
		InputManager.instance.GetMouseAxis("RollSpeed") ;
	}
	
	public float GetYawSpeed(){
		InputManager.instance.GetMouseAxis("YawSpeed") ;
	}
	
	public float GetPitchSpeed(){
		InputManager.instance.GetMouseAxis("PitchSpeed") ;
	}
	
	public Vector3 GetLookTarget(){
		lookTarget;
	}
	
	public void Init(ControlledObject obj){
		allMyObjects.Add(obj);
		BaseMountObject mounted = obj as BaseMountObject;
		if(mounted!=null){
			allUseableObject.Add(mounted);
		}
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
				if (hitInfo.collider == myCollider || hitInfo.transform.IsChildOf(myShip.myTransform) || hitInfo.collider.isTrigger)
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
				animator.WeaponDown(false);
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