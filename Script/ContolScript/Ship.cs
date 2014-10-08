using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	// Use this for initialization
    public Transform myTransform;
	
	public Dictionary<ROLETYPE,Player> crew = new Dictionary<ROLETYPE,Player>();
	
	public PlayerCamera[] cameras;
	
	public bool isEngineerSlot;
	
	public bool isNavigatorSlot;
	
	public Player engineer { 
		get {
			if(crew.Containskey(ROLETYPE.ENGINEER){
				return crew[ROLETYPE.ENGINEER];
			}
			return null;
		}
	};
	
	public Player pilot{ 
		get {
			if(crew.Containskey(ROLETYPE.PILOT){
				return crew[ROLETYPE.PILOT];
			}
			return null;
		}
	};
	
	public Player shooter{ 
		get {
			if(crew.Containskey(ROLETYPE.SHOOTER){
				return crew[ROLETYPE.SHOOTER];
			}
			return null;
		}
	};
	
	public Player navigator{ 
		get {
			if(crew.Containskey(ROLETYPE.NAVIGATOR){
				return crew[ROLETYPE.NAVIGATOR];
			}
			return null;
		}
	};

    void Awake()
    {
        myTransform = transform;
		cameras= GetComponentsInChildren<PlayerCamera>();
    }
	public ROLETYPE GetRole(Player newCrewman){
		if(pilot==null){
			crew[ROLETYPE.PILOT] = newCrewman;
			return ROLETYPE.PILOT;
		}
		if(shooter==null){
			crew[ROLETYPE.SHOOTER] = newCrewman;
			return ROLETYPE.SHOOTER;
		}
		if(navigator==null){
			crew[ROLETYPE.NAVIGATOR] = newCrewman;
			return ROLETYPE.NAVIGATOR;
		}
		if(engineer==null){
			crew[ROLETYPE.ENGINEER] = newCrewman;
			return ROLETYPE.ENGINEER;
		}
	}
	public ROLETYPE NextRole(Player newCrewman){
		
		int i = (int)newCrewman.role +1;
		while(true){
			if(i==2 &&!isEngineerSlot){
				i++
				continue;
			}
			if(i==3 &&!isNavigatorSlot){
				i++
				continue;
			}
			if(i>=4){
				i=0;
				
			}
			if(crew.ContainsKey((ROLETYPE)i)){
				if(crew[(ROLETYPE)i)]==newCrewman){
					return newCrewman.role;
				}
				i++;
			}else{
				crew.Remove(newCrewman.role);
				return (ROLETYPE)i;
				
			}
			
		}
				
		
	} 
	public List<PlayerCamera> GetCamerasForRole(ROLETYPE role){
		List<PlayerCamera> answer = new List<PlayerCamera>();
		foreach(PlayerCamera camera in cameras){
			if(camera.role==role){
				answer.Add(camera);
			}
		}
		return answer;
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
