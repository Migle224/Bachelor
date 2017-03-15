using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationInfo : MonoBehaviour {

    public string destinationName;
    public Material destinationMaterial;
	// Use this for initialization
	void Start () {
        destinationMaterial = this.gameObject.GetComponent<Renderer>().material;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
