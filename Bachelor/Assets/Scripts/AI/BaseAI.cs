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

    public BaseAI() {
        
    }

    public virtual void Start()
    {
        this.AddComponents();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        this.SetDestination(gameManager.timeOfDay);
        trafficLightController = GameObject.FindGameObjectWithTag("TrafficLightController").GetComponent<TrafficLightController>();
        trafficLightStopMaterial = trafficLightController.trafficLightStopMaterial;
        
    }

    void AddComponents()
    {
        agent = go.AddComponent<NavMeshAgent>();
        go.AddComponent<Rigidbody>().isKinematic = true;
    }

    void SetDestination(float _timeOfDay)
    {
        ShelduleInfo last = sheldule[0];

        foreach (ShelduleInfo info in sheldule)
        {
            if (info.time > _timeOfDay)
            {
                agent.SetDestination(last.destination.transform.position);
                currentDestination = last;
                nextDestination = info;
                go.GetComponent<Renderer>().material = currentDestination.destination.GetComponent<DestinationInfo>().destinationMaterial;
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
        }
    }


    void SetDestination()
    {
        currentDestination = nextDestination;
        agent.SetDestination(currentDestination.destination.transform.position);
        go.GetComponent<Renderer>().material = currentDestination.destination.GetComponent<DestinationInfo>().destinationMaterial;

        if (currentDestination.Equals(sheldule[sheldule.Count - 1]))
        {
            nextDestination = sheldule[0];
        }
        else
        {
            nextDestination = sheldule[sheldule.FindIndex(isCurrent) + 1];
        }

        

    }

    private bool isCurrent(ShelduleInfo name)
    {
        return (name.Equals(currentDestination));
    }


    public virtual void Update()
    {
        if (nextDestination.time < gameManager.timeOfDay)
            this.SetDestination();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Zebra":
                this.trafficLightCollider = other;
                this.StartZebraPosition();
                break;
        }
    }

    void StartZebraPosition()
    {
        Material trafficLightMaterial = trafficLightCollider.gameObject.GetComponent<Renderer>().material;
        if (string.Compare(trafficLightMaterial.name.Replace(" (Instance)", ""), trafficLightStopMaterial.name) == 0)
            agent.Stop();
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
        agent.Resume();
    }

    void CheckTrafficLightSignal()
    {
        Material trafficLightMaterial = trafficLightCollider.gameObject.GetComponent<Renderer>().material;

        if (string.Compare(trafficLightMaterial.name.Replace(" (Instance)", ""), trafficLightStopMaterial.name) != 0)
        {
            agent.Resume();
        }
    }
}

