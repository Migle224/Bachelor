using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationInfo : MonoBehaviour {

    public string destinationName;
    public Material destinationMaterial;
    public DestinationType destiantionType;
    SimulationManager simulationManager;
	// Use this for initialization
	void Start () {
        simulationManager = GameObject.FindGameObjectWithTag("SimulationManager").GetComponent<SimulationManager>();

        simulationManager.AddDestinationPoint(this.destiantionType, this.gameObject);

        destinationMaterial = this.gameObject.GetComponent<Renderer>().material;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
