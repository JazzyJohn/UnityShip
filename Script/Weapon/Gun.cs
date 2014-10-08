using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Gun : BaseMountObject{

	public float ammo;
	
	public float maxAmmo;
	
	public float fireInterval;
	
	public float reloadTime;
	
	float _reloadTime;
	
	float _fireTimer;
	
	bool reloading;
	
	bool firing;

    public LineRenderer render;
    protected void FixedUpdate()
    {
        base.FixedUpdate();
        render.SetPosition(0, mTransform.position);
        render.SetPosition(1, mTransform.position + mTransform.forward * 100);

    }
	public override void Activate(){
		
	}
	public override bool CanActivate(){
		return base.CanActivate()&&!reloading&&!firing&&_fireTimer<0;
	}
}
