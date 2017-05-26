using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkTestStartPoint : MonoBehaviour {
    TestController testController;
    GameManager gameManager;
    float testStartTime = 12 * 60 + 30;
    bool testStarted;
    public GameObject destination, prefab;
    WalkTestEndPoint walkTestEndPoint;
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag(Const.tagTESTCONTOLLER);
        if (go)
            testController = go.GetComponent<TestController>();

        go = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER);
        if (go)
            gameManager = go.GetComponent<GameManager>();

        testStarted = false;

        walkTestEndPoint = destination.GetComponent<WalkTestEndPoint>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!this.testStarted && gameManager.timeOfDay > testStartTime)
        {

            GameObject agentObject;
            testStarted = true;
            
            agentObject = Instantiate(prefab, this.gameObject.transform);
            agent = agentObject.GetComponent<NavMeshAgent>();
            agent.SetDestination(destination.transform.position);
            walkTestEndPoint.TestStarted(agentObject.GetInstanceID());
        }
	}
}
