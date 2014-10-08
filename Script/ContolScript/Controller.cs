using UnityEngine;
using System;

public interface Controller{

	float GetForwardThrottle();
	
	float GetDiveThrottle();

	float GetRollSpeed();
	
	float GetYawSpeed();
	
	float GetPitchSpeed();
	
	Vector3 GetLookTarget();

    bool IsFullStop();
    
	void InitObj(ControlledObject obj);
}