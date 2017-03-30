using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour {

    public GameObject employeeAIObject, freeEmployeeAIObject, turistAIObject, homelessAIObject;
    public float spawnTime = 5;
    // Use this for initialization

    List<GameObject> destinationWorkPlaces = new List<GameObject>(), destiantionBusStop = new List<GameObject>(), destinationCaffes = new List<GameObject>(), destinationHomes = new List<GameObject>();
    void Start () {

       

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            BaseAI ai = new EmployeeAI(Instantiate(employeeAIObject), destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes);
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

    public void InstantiateAI(Role _role, int _amount)
    {
        StartCoroutine(InstantiateAIWithTime(_role, _amount));
    }

    IEnumerator InstantiateAIWithTime(Role _role, int _amount)
    {
        switch (_role)
        {
            case Role.Turist:
                for (int i = 0; i <= _amount; i++)
                {
                    new TuristAI(Instantiate(turistAIObject), destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes);
                    yield return new WaitForSeconds(spawnTime);
                }
                break;
            case Role.Employee:
                for (int i = 0; i <= _amount; i++)
                {
                    new EmployeeAI(Instantiate(employeeAIObject), destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes);
                    yield return new WaitForSeconds(spawnTime);
                }
                break;
            case Role.FreeEmployee:
                for (int i = 0; i <= _amount; i++)
                {
                    new FreeEmployeeAI(Instantiate(freeEmployeeAIObject), destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes);
                    yield return new WaitForSeconds(spawnTime);
                }
                break;
            case Role.Homeless:
                for (int i = 0; i <= _amount; i++)
                {
                    new HomelessAI(Instantiate(homelessAIObject), destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes);
                    yield return new WaitForSeconds(spawnTime);
                }
                break;
        }
    }
}
