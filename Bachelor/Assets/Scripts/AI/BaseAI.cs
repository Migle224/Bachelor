using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI
{
    ShelduleInfo currentDestination, nextDestination;
    GameManager gameManager;
    protected List<ShelduleInfo> sheldule = new List<ShelduleInfo>();
    protected NavMeshAgent agent;
    Material trafficLightStopMaterial;
    Collider trafficLightCollider;
    TrafficLightController trafficLightController;
    protected GameObject go;
    protected BaseAIBehaviour baseAI;
    SimulationManager simulationManager;
    bool waitForNextDay = false, checkTrafficLight = false;
    string description;
    private List<Observer> observers = new List<Observer>();
    protected Role role;


    float distanceToSlow = 5f;
    public float distance;
    float speedModifier = 0.04f, startSpeed;
    public bool slow = false, speed = false;
    float rotationDifference = 5f;

    public BaseAI()
    {
        this.role = Role.None;
        this.FindObservers();
    }

    void FindObservers()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag(Const.tagOBSERVER);

        foreach (GameObject ob in obj)
            observers.Add(ob.GetComponent<Observer>());
    }
    public void Awake()
    {
      
    }

    public virtual void Start()
    {
        this.AddComponents();
        gameManager = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER).GetComponent<GameManager>();
        this.SetDestination(gameManager.timeOfDay);
        trafficLightController = GameObject.FindGameObjectWithTag(Const.tagTRAFFICLIGHTCONTROLLER).GetComponent<TrafficLightController>();
        trafficLightStopMaterial = trafficLightController.trafficLightStopMaterial;
        simulationManager = GameObject.FindGameObjectWithTag(Const.tagSIMULATIONMANAGER).GetComponent<SimulationManager>();
        go.gameObject.transform.position = currentDestination.destination.transform.position;
        this.NotifyObservers(this.role, true);
        checkTrafficLight = false;

        startSpeed = agent.speed;
        //    this.NotifyObservers(true);
    }

    void AddComponents()
    {
        agent = go.AddComponent<NavMeshAgent>();
        go.AddComponent<Rigidbody>().isKinematic = true;
    }

    void SetDestination(float _timeOfDay)
    {//TODO: rewrite logic for the next destiantion
        //TODO: combine this and other SetDestianion method
        ShelduleInfo last = sheldule[0];
        foreach (ShelduleInfo info in sheldule)
        {
            if (info.time > _timeOfDay)
            {
                agent.SetDestination(last.destination.transform.position);
                currentDestination = last;
                nextDestination = info;

                break;
            }
            else
            {
                last = info;
            }
        }

        if (currentDestination.Equals(sheldule[sheldule.Count - 1]))
        {
            nextDestination = sheldule[0];
            waitForNextDay = true;
        }

        if (currentDestination.destination == null)
        {
            currentDestination = sheldule[0];
            nextDestination = sheldule[1];
        }

        //go.GetComponent<Renderer>().material = currentDestination.destination.GetComponent<DestinationInfo>().destinationMaterial;
    }

    void NotifyObservers(GameObject _destination)
    {
        foreach (Observer ob in observers)
        {
            ob.UpdateDestination(DestinationType.None, _destination.GetComponent<DestinationInfo>().destiantionType);
        }
    }

    void NotifyObservers(GameObject _destinationFrom, GameObject _destinationTo)
    {
        foreach (Observer ob in observers)
        {
            ob.UpdateDestination(_destinationFrom.GetComponent<DestinationInfo>().destiantionType, _destinationTo.GetComponent<DestinationInfo>().destiantionType);
        }
    }

    void NotifyObservers(Role _role, bool _created)
    {
        foreach (Observer ob in observers)
        {
            ob.UpdateCount(_role, _created);
        }
    }

    void NotifyObservers(bool _enabled)
    {
        foreach (Observer ob in observers)
        {
            ob.UpdateEnabledState(_enabled);
        }
    }

    void SetDestination()
    {
        this.NotifyObservers(currentDestination.destination, nextDestination.destination);
        currentDestination = nextDestination;
        agent.SetDestination(currentDestination.destination.transform.position);


        if (currentDestination.Equals(sheldule[sheldule.Count - 1]))
        {
            nextDestination = sheldule[0];
            waitForNextDay = true;
        }
        else
        {
            nextDestination = sheldule[sheldule.FindIndex(isCurrent) + 1];
        }

        //go.GetComponent<Renderer>().material = currentDestination.destination.GetComponent<DestinationInfo>().destinationMaterial;

    }

    private bool isCurrent(ShelduleInfo name)
    {
        return (name.Equals(currentDestination));
    }

    public virtual void Update()
    {
        if (waitForNextDay && gameManager.timeOfDay > 0 && gameManager.timeOfDay < 60)
            waitForNextDay = false;

        if (nextDestination.time < gameManager.timeOfDay && !waitForNextDay)
            this.SetDestination();

        if (Input.GetKeyDown(KeyCode.Space))
            this.ResumeMovement();

        if (checkTrafficLight && (Time.frameCount % 5 == 0))
            this.CheckTrafficLightSignal();

       /* {
            RaycastHit hit;
            if (Physics.Raycast(go.gameObject.transform.position, go.gameObject.transform.forward, out hit))
            {
                distance = hit.distance;
                if (hit.collider.gameObject.tag == "Agent")
                {
                    if (((int)hit.collider.transform.rotation.y % 360)  >= (int)go.gameObject.transform.rotation.y % 5 - rotationDifference
                       &&  ((int)hit.collider.transform.rotation.y % 360) <= (int)go.gameObject.transform.rotation.y % 5 + rotationDifference)
                    {
                        if (distance < distanceToSlow)
                        {
                            slow = true;
                            speed = false;
                        }
                        else
                        {
                            speed = true;
                            slow = false;
                        }
                    }
                    else
                    {
                        speed = true;
                        slow = false;
                    }
                }
                else
                {
                    speed = true;
                    slow = false;
                }

            }
            else
            {
                speed = true;
                slow = false;
            }

        }

        if (speed && agent.speed < startSpeed)
        {
            agent.speed += speedModifier;
        }
        if (slow)
        {
            agent.speed -= speedModifier;
        }
        */
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case Const.tagZEBRA:
                this.checkTrafficLight = true;
                this.trafficLightCollider = other;
                this.StartZebraPosition();
                break;
            case Const.tagDESTINATION:
                if (other.gameObject.GetInstanceID() == currentDestination.destination.GetInstanceID())
                {
                    float nextDestinaitonTime = nextDestination.time;
                    this.SetDestination();
                    simulationManager.AddToWakeUpList(this.go.gameObject, nextDestinaitonTime);
                }
                break;
        }
    }

    void StartZebraPosition()
    {
        Material trafficLightMaterial = trafficLightCollider.gameObject.GetComponent<Renderer>().material;
        if (string.Compare(trafficLightMaterial.name.Replace(" (Instance)", ""), trafficLightStopMaterial.name) == 0)
            agent.isStopped = true;
    }

    public virtual void InitSheldule() { }

    public void OnTriggerStay(Collider other)
    {
       /* switch (other.gameObject.tag)
        {
            case "Zebra":
                this.CheckTrafficLightSignal();
                break;
            case Const.tagDESTINATION:
                if (other.gameObject.GetInstanceID() == currentDestination.destination.GetInstanceID())
                {
                    float nextDestinaitonTime = nextDestination.time;
                    this.SetDestination();
                    simulationManager.AddToWakeUpList(this.go.gameObject, nextDestinaitonTime);
                }
                break;
        }*/
    }

    public void OnTriggerExit(Collider other)
    {
        agent.isStopped = false;
    }

    void CheckTrafficLightSignal()
    {
        Material trafficLightMaterial = trafficLightCollider.gameObject.GetComponent<Renderer>().material;

        if (string.Compare(trafficLightMaterial.name.Replace(" (Instance)", ""), trafficLightStopMaterial.name) != 0)
        {
            agent.isStopped = false;
            checkTrafficLight = false;
        }
    }

    public void ResumeMovement()
    {
        this.NotifyObservers(true);//TODO write this somewhere else. On enabled
        if(agent.isActiveAndEnabled)
            agent.SetDestination(currentDestination.destination.transform.position);
    }

    public void attach(Observer observer)
    {
        observers.Add(observer);
    }

    public void OnEnable()
    {
        this.NotifyObservers(true);
    }

    public void OnDestroy()
    {
        this.NotifyObservers(this.role, false);
    }

    public void OnDisable()
    {
        this.NotifyObservers(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case Const.tagBUS:
                go.gameObject.transform.parent = collision.gameObject.transform;
                collision.gameObject.GetComponent<BusController>().AddToBus(go);
                go.SetActive(false);
                break;

        }
    }
}
