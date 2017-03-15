using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {

   //Transform goal;
    NavMeshAgent agent;
    public Material trafficLightStopMaterial;
    Collider trafficLightCollider;
    public bool onZebra;
    GameObject[] destinations;
    public Transform currentDestination;
    public float timeSinceDestination;
    public List<string> destinationsList;
    Material startZebraPosition;

    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       // agent.destination = goal.position;
      //  goal = GameObject.FindGameObjectWithTag("Destination").transform;
        InvokeRepeating("RecalculateDestination", 2.0f, 1.0f);
        destinations = GameObject.FindGameObjectsWithTag("Destination");
        this.AssignNewDestination();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            this.RecalculateDestination();
        }
    }

    void RecalculateDestination()
    {
        agent.destination = currentDestination.position;
       
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Zebra":
                this.trafficLightCollider = other;
                this.onZebra = true;
                this.StartZebraPosition();
                break;
            case "Destination":
                this.AssignNewDestination();
                break;
        }

    }
    void OnTriggerExit(Collider other)
    {
        agent.Resume();
    }

        void AssignNewDestination()
    {
        DestinationInfo destinationInfo = destinations[Random.Range(0, destinations.Length - 1)].GetComponent<DestinationInfo>();
        currentDestination = destinationInfo.gameObject.transform;
        timeSinceDestination = Time.timeSinceLevelLoad;
        destinationsList.Add(destinationInfo.destinationName);
        this.gameObject.GetComponent<Renderer>().material = destinationInfo.destinationMaterial;
    }


    void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Zebra":
                this.CheckTrafficLightSignal();
                break;
        }
    }

    void StartZebraPosition()
    {
        Material trafficLightMaterial = trafficLightCollider.gameObject.GetComponent<Renderer>().material;
        if (string.Compare(trafficLightMaterial.name.Replace(" (Instance)", ""), trafficLightStopMaterial.name) == 0)
            agent.Stop();
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
