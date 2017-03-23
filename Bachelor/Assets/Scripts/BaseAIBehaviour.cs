using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAIBehaviour : MonoBehaviour
{
    public  BaseAI ai;


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

    void OnTriggerStay(Collider other)
    {
        ai.OnTriggerStay(other);
    }

    void OnTriggerExit(Collider other)
    {
        ai.OnTriggerExit(other);
    }
}