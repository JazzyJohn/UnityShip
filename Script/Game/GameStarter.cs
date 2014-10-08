using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameStarter : MonoBehaviour{

	public GameObject shipPrefab;

	public GameObject playerPrefab;
	
	void Awake(){
		StartGame();
	}
	
	public void StartGame(){
		GameObject playerGo=  Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		Player player = playerGo.GetComponent<Player>();
		
		GameObject shipGo=  Instantiate(shipPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        ControlledObject[] allObjects = shipGo.GetComponentsInChildren<ControlledObject>();
		foreach(ControlledObject obj in allObjects){
			player.InitObj(obj);
		}
        player.myShip = shipGo.GetComponent<Ship>();
	}
	


}