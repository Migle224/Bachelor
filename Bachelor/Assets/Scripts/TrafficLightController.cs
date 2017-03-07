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

                if (string.Compare(light.GetComponent<Renderer>().material.name.Replace(" (Instance)",""), trafficLightStopMaterial.name) == 0)
                    light.GetComponent<Renderer>().material = trafficLightGoMaterial;
                else
                    light.GetComponent<Renderer>().material = trafficLightStopMaterial;
                
            }
        }
    }
}
