using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserAICreationUIController : MonoBehaviour {
    //TODO rewrite switc and dropDown menu
    public GameObject[] activities;
    SimulationManager simulationManager;
    public Text descriptionText;


    // Use this for initialization
    void Start () {
        simulationManager = GameObject.FindGameObjectWithTag(Const.tagSIMULATIONMANAGER).GetComponent<SimulationManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateAIShelduleAndSave()
    {
        Text time;
        Dropdown place;
        ShelduleByPlaces shelduleByPlace;
        List<ShelduleByPlaces> sheldule = new List<ShelduleByPlaces>();
        foreach (GameObject activity in activities)
        {           
            time = activity.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Text>();
            place = activity.transform.GetChild(1).GetComponent<Dropdown>();
            shelduleByPlace.time = int.Parse(time.text) * 60;

            activity.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "";

            switch (place.value)
            {
                case 1:
                    shelduleByPlace.destinationType = DestinationType.Home;
                    sheldule.Add(shelduleByPlace);
                    break;
                case 2:
                    shelduleByPlace.destinationType = DestinationType.BusStop;
                    sheldule.Add(shelduleByPlace);
                    break;
                case 3:
                    shelduleByPlace.destinationType = DestinationType.WorkPlace;
                    sheldule.Add(shelduleByPlace);
                    break;
                case 4:
                    shelduleByPlace.destinationType = DestinationType.CoffePlace;
                    sheldule.Add(shelduleByPlace);
                    break;
                case 5:
                    shelduleByPlace.destinationType = DestinationType.TuristPlace;
                    sheldule.Add(shelduleByPlace);
                    break;
            }
            activity.transform.GetChild(1).GetComponent<Dropdown>().value = 0;
        }
        simulationManager.SaveUserAI(descriptionText.text, sheldule);
    }
}
