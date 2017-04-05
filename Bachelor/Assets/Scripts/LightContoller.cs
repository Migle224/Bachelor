using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightContoller : MonoBehaviour {

    Transform lightTransform;
    float changingSpeed = 0.05f;
	// Use this for initialization
	void Start () {
        lightTransform = this.gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        lightTransform.Rotate(Vector3.up * Time.deltaTime * changingSpeed);
       // lightTransform.Rotate(Vector3.right * Time.deltaTime * changingSpeed);
    }
}
