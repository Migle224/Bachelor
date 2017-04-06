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
    bool waitForNextDay = false;
    string description;

    public BaseAI() {
    }

    public virtual void Start()
    {
        this.AddComponents();
        gameManager = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER).GetComponent<GameManager>();
        this.SetDestination(gameManager.timeOfDay);
        trafficLightController = GameObject.FindGameObjectWithTag(Const.tagTRAFFICLIGHTCONTROLLER).GetComponent<TrafficLightController>();
        trafficLightStopMaterial = trafficLightController.trafficLightStopMaterial;
        simulationManager = GameObject.FindGameObjectWithTag(Const.tagSIMULATIONMANAGER).GetComponent<SimulationManager>();
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

        if (currentDestination.Equals( sheldule[sheldule.Count - 1]))
        {
            nextDestination = sheldule[0];
            waitForNextDay = true;
        }

        if (currentDestination.destination == null)
        {
            currentDestination = sheldule[0];
            nextDestination = sheldule[1];
        }

        go.GetComponent<Renderer>().material = currentDestination.destination.GetComponent<DestinationInfo>().destinationMaterial;
    }


    void SetDestination()
    {
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

        go.GetComponent<Renderer>().material = currentDestination.destination.GetComponent<DestinationInfo>().destinationMaterial;

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
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case Const.tagZEBRA:
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
        switch (other.gameObject.tag)
        {
            case "Zebra":
                this.CheckTrafficLightSignal();
                break;
        }
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
        }
    }

    public void ResumeMovement()
    {
        this.SetDestination();
    }
}

