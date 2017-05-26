using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionCounter : MonoBehaviour
{

    int collisionCounter;
    float areaSize;
    NavMeshModifier navMeshModifier;
    // Use this for initialization
    void Start()
    {
        areaSize = this.gameObject.GetComponent<Renderer>().bounds.size.x * this.gameObject.GetComponent<Renderer>().bounds.size.z;
        navMeshModifier = this.gameObject.GetComponent<NavMeshModifier>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        collisionCounter++;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        collisionCounter--;
    }

    public float GetDencity()
    {
        if (collisionCounter == 0) return 0;

        return (areaSize / collisionCounter) / 4;
    }

    public void SetNavMeshArea(int _area)
    {
        navMeshModifier.area = _area;
        //Debug.Log("den " + GetDencity() + " area " + _area);
    }
}
