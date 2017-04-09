using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float timeOfDay;
    public const int TIMEMODIFIER = 2;
    const float TIMESTARTMODIFIER = 0.2f;
    SimulationManager simulationManager;
    public float dayStarts = 6 * 60, dayEnds = 18 * 60, midday;
    public int dayOfTheYear;

    // Use this for initialization
    void Start () {
        simulationManager = GameObject.FindGameObjectWithTag(Const.tagSIMULATIONMANAGER).GetComponent<SimulationManager>();
        midday = dayStarts + (dayEnds - dayStarts) / 2;
	}
	
	// Update is called once per frame
	void Update () {
        timeOfDay = (Time.timeSinceLevelLoad * TIMESTARTMODIFIER)  % (24 * 60) ;

        if(timeOfDay > 0 && timeOfDay < 10)
            dayOfTheYear = (int)(Time.timeSinceLevelLoad * TIMESTARTMODIFIER) / (24 * 60);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            simulationManager.LoadSavedAI();
        }
	}

    public bool IsNight()
    {
        if (this.timeOfDay < dayStarts || this.timeOfDay > dayEnds)
            return true;

        return false;
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

    public void InstantiateAI(string _description, int _amount)
    {
        simulationManager.InstantiateAI(_description, _amount);
    }

    public List<string> GetLoadedAINames()
    {
        return simulationManager.GetLoadedAINames();
    }
}
