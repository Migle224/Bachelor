using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour {

    GameObject[] trafficLights;
    public float trafficSignalsTime;
    public Material trafficLightGoMaterial, trafficLightStopMaterial;

    void Start()
    {
        this.AssigneTrafficLights();
        StartCoroutine(ChangeTrafficSignals());
    }

    
    void AssigneTrafficLights()
    {
        trafficLights = GameObject.FindGameObjectsWithTag("TrafficLight");
    }

    IEnumerator ChangeTrafficSignals()
    {
        for (;;)
        {
            yield return new WaitForSeconds(trafficSignalsTime);
            foreach (GameObject light in trafficLights)
            {
                Debug.Log(light.GetComponent<Renderer>().material.name);

                light.GetComponent<Renderer>().material = 
                        light.GetComponent<Renderer>().material == trafficLightStopMaterial
                        ? trafficLightGoMaterial : trafficLightStopMaterial;
                
            }
        }
    }
}
