using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightContoller : MonoBehaviour {

    Transform lightTransform;
    float changingSpeed = 0.05f;
    float rotateRightSpeed = 0.05f;
    Vector3 rotationVector = Vector3.right ;
    GameManager gameManager;
    float midday;
    bool isNight;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER).GetComponent<GameManager>();
        lightTransform = this.gameObject.GetComponent<Transform>();
        midday = gameManager.midday;
        isNight = gameManager.IsNight();
	}

    // Update is called once per frame
    void Update() {

      /*  lightTransform.Rotate(Vector3.up * Time.deltaTime * changingSpeed, Space.World);

        if (!isNight && gameManager.timeOfDay < midday)
            lightTransform.Rotate(Vector3.right * Time.deltaTime * rotateRightSpeed);

        if (!isNight && gameManager.timeOfDay > midday)
            lightTransform.Rotate(Vector3.left * Time.deltaTime * rotateRightSpeed);

        if (isNight && !gameManager.IsNight())
        {
            this.gameObject.gameObject.GetComponent<Light>().color = Color.white;
            isNight = false;
        }
        if (!isNight && gameManager.IsNight())
        { 
            this.gameObject.gameObject.GetComponent<Light>().color = Color.black;
            isNight = true;
        }*/

    }
}
