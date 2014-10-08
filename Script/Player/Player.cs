using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public enum ROLETYPE{ PILOT,SHOOTER,ENGINEER, NAVIGATOR};

public class Player : MonoBehaviour,Controller{
	
	List<ControlledObject> allMyObjects =new List<ControlledObject>();
	
	List<BaseMountObject> allUseableObject =new List<BaseMountObject>();
	
	List<PlayerCamera> allRoleCamera = new List<PlayerCamera>();
	
	int curCamera=0;
 
    Ship myShip;

    public float aimRange;
	
	public ROLETYPE role;
	
	Vector3 lookTarget = Vector3.zero;
	Transform curLookTarget;
	void Update(){
		UpdateLook();
		if(InputManager.instance.GetButtonDown("Nextrole")){
			NextRole();
		}
		if(InputManager.instance.GetButtonDown("NextCamera")){
			NextRole();
		}
		if(InputManager.instance.GetButtonDown("PrevCamera")){
			PrevCamera();
		}
		if(InputManager.instance.GetButtonDown("Activate")){
			ActivateAll();
		}
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
	
	public float GetDiveThrottle(){
        return InputManager.instance.GetMouseAxis("DiveThrottle");
	}
	
	public Vector3 GetLookTarget(){
		return lookTarget;
	}
	
	public void InitRole(Ship ship){
		myShip = ship;
		role = ship.GetRole(this);
		SwitchRole();
	
	}
	public void SwitchRole(){
		foreach(ControlledObject obj  in allMyObjects){
			obj.DeInit();
		}
		allMyObjects.Clear();
		allUseableObject.Clear();
		ControlledObject[] allObjects = myShip.GetComponentsInChildren<ControlledObject>();
		foreach(ControlledObject obj in allObjects){
			if(obj.type ==role){
				InitObj(obj);
			}
		}
		allRoleCamera[curCamera].enabled= false;
		
		allRoleCamera =mysShip.GetCamerasForRole(role);
		curCamera=0;
		allRoleCamera[curCamera].enabled =true;
	}
	
	public void NextCamera(){
		
		SwitchCamera(curCamera+1);
	}
	public void PrevCamera(){
		
		SwitchCamera(curCamera-1);
	}
	public void SwitchCamera(int index){
		if(index<0){
			index = allRoleCamera.Count-1;
		}
		if(index>=allRoleCamera.Count){
			index=0;
		}
		allRoleCamera[curCamera].enabled= false;
		curCamera = index;
		allRoleCamera[curCamera].enabled =true;
		
	}
	public void NextRole(){
		ROLETYPE newrole = ship.NextRole(this);
		if(newrole!=role){
			role =newrole;
			SwitchRole();
		}
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
	public void ActivateAll(){
		foreach(BaseMountObject obj in  allUseableObject){
			if(obj.CanActivate()){
				obj.Activate();
			}
		}
	}
}