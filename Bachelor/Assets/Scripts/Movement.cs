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
        switch(other.gameObject.tag)
        {
            case "TrafficLight":
                this.trafficLightCollider = other;
                this.CheckTrafficLightSignal();
                break;
            case "Destination":
                this.AssignNewDestination();
                break;
        }

    }

    void AssignNewDestination()
    {
        currentDestination = destinations[Random.Range(0, destinations.Length - 1)].transform;
        timeSinceDestination = Time.timeSinceLevelLoad;
        destinationsList.Add(currentDestination.GetComponent<DestinationInfo>().destinationName);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Zebra")
        {
            this.onZebra = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "TrafficLight":
                this.CheckTrafficLightSignal();
                break;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Zebra")
        {
            this.onZebra = false;
        }
    }


    void CheckTrafficLightSignal()
    {
        Material trafficLightMaterial = trafficLightCollider.gameObject.GetComponent<Renderer>().material;

        if (string.Compare(trafficLightMaterial.name.Replace(" (Instance)", ""), trafficLightStopMaterial.name) == 0
            && !this.onZebra)
        {
            agent.Stop();
        }
        else
        {
            agent.Resume();
        }
    }

}
