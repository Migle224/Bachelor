using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour {

    public GameObject employeeAIObject, freeEmployeeAIObject, turistAIObject, homelessAIObject;
    public float spawnTime = 5;

    SortedList<float, GameObject> objectsToWakeUp = new SortedList<float, GameObject>();
    GameManager gameManager;
    // Use this for initialization

    List<GameObject> destinationWorkPlaces = new List<GameObject>(), destiantionBusStop = new List<GameObject>(), destinationCaffes = new List<GameObject>(), destinationHomes = new List<GameObject>();
    void Start () {
        gameManager = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER).GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            BaseAI ai = new EmployeeAI(Instantiate(employeeAIObject), destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes);
        }

        this.WakeUpGameObjects(gameManager.timeOfDay);
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

    public void AddToWakeUpList(GameObject _gameObject, float _wakeUpTime)
    {
        float wakeUpTime = _wakeUpTime;
        int maxRetryTimes = 10;
        int retriedTimes = 0;
        float wakeUpTimeModifier = 0.0000001f;

        while (retriedTimes <= maxRetryTimes)
        {
            try
            {
                //objectsToWakeUp.Add(wakeUpTime, _gameObject);
                objectsToWakeUp.Add(_gameObject.GetInstanceID(), _gameObject);
                Debug.Log("Added id: " + _gameObject.GetInstanceID() + " and time: " + _wakeUpTime);
                _gameObject.SetActive(false);
                
                
                break;
            }
            catch 
            {
                Debug.Log("Key = " + _gameObject.GetInstanceID() + " is found. Retrying for : " + retriedTimes + " time. uniqID: " + _gameObject.GetInstanceID() + ", time : " + _wakeUpTime) ;
                retriedTimes++;
                wakeUpTimeModifier += wakeUpTimeModifier * _gameObject.GetInstanceID();
                wakeUpTime = wakeUpTime + wakeUpTimeModifier * _gameObject.GetInstanceID();
            }
        }       
    }

    void WakeUpGameObjects(float _timeOfDay)
    {
        int i = 0;
        float index;
        GameObject objectToWakeUp;

        if (objectsToWakeUp.Count > 0)
        {
            index = objectsToWakeUp.Keys[0];
            while (index < _timeOfDay && objectsToWakeUp.Count > 0)
            {
                objectToWakeUp = objectsToWakeUp[index];
                objectToWakeUp.SetActive(true);
                objectsToWakeUp.Remove(index);
                if (objectsToWakeUp.Count > 0)
                    index = objectsToWakeUp.Keys[0];
            }
        }

    }
}
