using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAIBehaviour : MonoBehaviour
{
    public BaseAI ai;


    void OnTriggerEnter(Collider other)
    {
        ai.OnTriggerEnter(other);
    }

    void Update()
    {
        ai.Update();
    }

    void Start()
    {
        ai.Start();
    }

    /*void OnTriggerStay(Collider other)
    {
        ai.OnTriggerStay(other);
    }*/

    void OnTriggerExit(Collider other)
    {
        ai.OnTriggerExit(other);
    }

    public void ResumeMovement()
    {
        ai.ResumeMovement();
    }

    /*void OnEnable()
    {
        ai.OnEnable();
    }*/

    void OnDestroy()
    {
        ai.OnDestroy();
    }

    void OnDisable()
    {
        ai.OnDisable();
    }

    /*   void Awake()
       {
           ai.Awake();
       }*/

    void OnCollisionEnter(Collision collision)
    {
        ai.OnCollisionEnter(collision);
    }

}