using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



public class ControlledObject : MonoBehaviour{

	protected Controller controller;
	
	public ROLETYPE type;

	public void Init(Controller controller){
		this.controller = controller;	
	}
	public void DeInit(){
		this.controller=null;
	}

}