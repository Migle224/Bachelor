using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour {

    public GameObject studentAIObject;
    // Use this for initialization

    List<GameObject> destinationWorkPlaces = new List<GameObject>(), destiantionBusStop = new List<GameObject>(), destinationCaffes = new List<GameObject>(), destinationHomes = new List<GameObject>();
    void Start () {

       

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            BaseAI ai = new StudentAI(Instantiate(studentAIObject), destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes);
        }

    }

    public void AddDestinationPoint(DestinationType _destinationType, GameObject _destination)
    {
        switch (_destinationType)
        {
            case DestinationType.BusStop:
                destiantionBusStop.Add(_destination);
                break;
            case DestinationType.CoffePlace:
                destinationCaffes.Add(_destination);
                break;
            case DestinationType.Home:
                destinationHomes.Add(_destination);
                break;
            case DestinationType.WorkPlace:
                destinationWorkPlaces.Add(_destination);
                break;

        }
    }
}
