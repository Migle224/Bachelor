using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour {

    public GameObject employeeAIObject, freeEmployeeAIObject, turistAIObject, homelessAIObject, loadedAIObject;
    public float spawnTime = 5;

    SortedList<float, GameObject> objectsToWakeUp = new SortedList<float, GameObject>(), objectsToWakeUpNextDay = new SortedList<float, GameObject>();
    GameManager gameManager;
    List<GameObject> destinationWorkPlaces = new List<GameObject>(), destiantionBusStop = new List<GameObject>(), destinationCaffes = new List<GameObject>(), destinationHomes = new List<GameObject>(), destinationTuristPlaces = new List<GameObject>();

    int currentDayOfYear = 0;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER).GetComponent<GameManager>();
        this.LoadSavedAI();
    }

    void Update()
    {
        if (currentDayOfYear < gameManager.dayOfTheYear)
        {
            float index;
            for (int i = 0; i < objectsToWakeUp.Count; i++)
            {
                index = objectsToWakeUp.Keys[i];
            }
            currentDayOfYear = gameManager.dayOfTheYear;
            objectsToWakeUp = new SortedList<float, GameObject>(objectsToWakeUpNextDay);
            objectsToWakeUpNextDay.Clear();
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
            case DestinationType.TuristPlace:
                destinationTuristPlaces.Add(_destination);
                break;
        }
    }

    public void InstantiateAI(Role _role, int _amount)
    {
        StartCoroutine(InstantiateAIWithTime(_role, _amount));
    }

    public void InstantiateAI(string _description, int _amount)
    {
        foreach (ShelduleToSave sheldule in SaveLoad.savedAIs)
        {
            if (sheldule.description == _description)
            {
                StartCoroutine(InstantiateAIWithTime(sheldule.shelduleByplaceList, _amount));
                break;
            }
        }
    }

    IEnumerator InstantiateAIWithTime(List<ShelduleByPlaces> _sheldule, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            new UserCreatedAI(Instantiate(loadedAIObject), _sheldule, destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes, destinationTuristPlaces);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator InstantiateAIWithTime(Role _role, int _amount)
    {
        switch (_role)
        {
            case Role.Turist:
                for (int i = 0; i < _amount; i++)
                {
                    new TuristAI(Instantiate(turistAIObject), destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes, destinationTuristPlaces);
                    yield return new WaitForSeconds(spawnTime);
                }
                break;
            case Role.Employee:
                for (int i = 0; i < _amount; i++)
                {
                    new EmployeeAI(Instantiate(employeeAIObject), destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes);
                    yield return new WaitForSeconds(spawnTime);
                }
                break;
            case Role.FreeEmployee:
                for (int i = 0; i < _amount; i++)
                {
                    new FreeEmployeeAI(Instantiate(freeEmployeeAIObject), destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes);
                    yield return new WaitForSeconds(spawnTime);
                }
                break;
            case Role.Homeless:
                for (int i = 0; i < _amount; i++)
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
        float wakeUpTimeModifierF = 0.000000001f, wakeUpTimeModifier = 0;

        while (retriedTimes <= maxRetryTimes)
        {
            try
            {
                if (wakeUpTime > gameManager.timeOfDay)
                    objectsToWakeUp.Add(wakeUpTime, _gameObject);
                else
                    objectsToWakeUpNextDay.Add(wakeUpTime, _gameObject);

                _gameObject.SetActive(false);                                
                break;
            }
            catch 
            {             
                retriedTimes++;
                wakeUpTimeModifier += wakeUpTimeModifierF * _gameObject.GetInstanceID();
                wakeUpTime = wakeUpTime + wakeUpTimeModifier ;
            }
        }       
    }

    void WakeUpGameObjects(float _timeOfDay)
    {
        float index;
        GameObject objectToWakeUp;

        if (objectsToWakeUp.Count > 0)
        {
            index = objectsToWakeUp.Keys[0];
            while (index < _timeOfDay && objectsToWakeUp.Count > 0)
            {
                objectToWakeUp = objectsToWakeUp[index];
                objectToWakeUp.SetActive(true);
                objectToWakeUp.GetComponent<BaseAIBehaviour>().ResumeMovement();
                
                objectsToWakeUp.Remove(index);
                if (objectsToWakeUp.Count > 0)
                    index = objectsToWakeUp.Keys[0];
            }
        }

    }

    public void SaveUserAI(string _description, List<ShelduleByPlaces> _shelduleByPlaces)
    {
        ShelduleToSave sts = new ShelduleToSave();
        sts.description = _description;
        sts.shelduleByplaceList = _shelduleByPlaces;
        ShelduleToSave.current = sts;
        SaveLoad.Save();
    }

    public void LoadSavedAI()
    {
        SaveLoad.Load();
    }

    public List<string> GetLoadedAINames()
    {
        List<string> names = new List<string>();

        foreach (ShelduleToSave sheldule in SaveLoad.savedAIs)
        {
            names.Add(sheldule.description);
        }

        return names;
    }
}
