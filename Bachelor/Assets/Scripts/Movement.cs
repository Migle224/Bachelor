using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {

    Transform goal;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       // agent.destination = goal.position;
       // goal = GameObject.FindGameObjectWithTag("Destination").transform;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            goal = GameObject.FindGameObjectWithTag("Destination").transform;
            agent.destination = goal.position;
        }
    }
}
