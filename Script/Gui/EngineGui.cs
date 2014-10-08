using UnityEngine;
using System.Collections;

public class EngineGui : MonoBehaviour {

    public UILabel throttle;

    public UILabel roll;

    public UILabel yaw;

    public UILabel pitch;
	
	public UILabel dive;

    public Engine engine;
    public void SetEngine(Engine engine)
    {
        this.engine = engine;
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (engine != null)
        {
            throttle.text = engine.Throttle.ToString("0.0");
            roll.text = engine.Roll.ToString("0.0");
            yaw.text = engine.Yaw.ToString("0.0");
            pitch.text = engine.Pitch.ToString("0.0");
			dive.text = engine.Dive.ToString("0.0");
        }
	}
}
