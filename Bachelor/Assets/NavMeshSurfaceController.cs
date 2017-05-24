using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSurfaceController : MonoBehaviour {

    GameObject[] navMeshAreas, agents, crossrodes, areas;
    NavMeshSurface navMeshSurface;
	// Use this for initialization
	void Start () {
        navMeshAreas = GameObject.FindGameObjectsWithTag(Const.tagNAVMESHAREA);
        //crossrodes = GameObject.FindGameObjectsWithTag(Const.tagZEBRA);
        navMeshSurface = this.gameObject.GetComponent<NavMeshSurface>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.frameCount % 150 == 0)
       // if(Input.GetKeyDown(KeyCode.F2))
            this.RecalculateNavMesh();
	}

    void RecalculateNavMesh()
    {
        foreach (GameObject go in navMeshAreas)
        {
            CollisionCounter cc = go.GetComponent<CollisionCounter>();
            cc.SetNavMeshArea(this.FindNavMeshArea(cc.GetDencity()));
        }


        this.gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
        this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, -0.075f, this.gameObject.transform.position.z);

        agents = GameObject.FindGameObjectsWithTag(Const.tagAGENT);

        foreach (GameObject go in agents)
        {
        //    Debug.Log("found agent");
            BaseAIBehaviour bab;
            bab = go.GetComponent<BaseAIBehaviour>();
            if (bab)
            {
               // Debug.Log("found bab");
                bab.ResumeMovement();
            }
        }
        
       
    }

    int FindNavMeshArea(float _dencity)
    {
        int dencity = (int)_dencity;

    

        switch (dencity)
        {
            case 0: return 8;
            case 1: return 12;
            case 2: return 11;
            case 3: return 10;
            case 4: return 9;
            case 5: return 7;
            case 6: return 6;
            case 7: return 5;
            case 8: return 4;
            case 9: return 3;
            default: return 8;
        }
    }
}
