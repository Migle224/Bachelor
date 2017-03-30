using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float timeOfDay;
    public const int TIMEMODIFIER = 2;
    const float TIMESTARTMODIFIER = 0.2f;
    SimulationManager simulationManager;

    // Use this for initialization
    void Start () {
        simulationManager = GameObject.FindGameObjectWithTag(Const.tagSIMULATIONMANAGER).GetComponent<SimulationManager>();
	}
	
	// Update is called once per frame
	void Update () {
        timeOfDay = (Time.timeSinceLevelLoad * TIMESTARTMODIFIER)  % (24 * 60) ;
	}

    public void SpeedUpTime()
    {
        if(Time.timeScale * TIMEMODIFIER < 100)
            Time.timeScale = Time.timeScale * TIMEMODIFIER;
    }

    public void SlowDownTime()
    {
        if(TIMEMODIFIER > 0)
            Time.timeScale = Time.timeScale / TIMEMODIFIER;
    }

    public void InstantiateAI(Role _role, int _amount)
    {
        simulationManager.InstantiateAI(_role, _amount);
    }
}
