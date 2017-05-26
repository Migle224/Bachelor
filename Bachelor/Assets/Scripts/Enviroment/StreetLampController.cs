using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLampController : MonoBehaviour {

    GameObject[] streetLamps;
    List<Light> streetLampsLights = new List<Light>();
    bool isNight = true;
    GameManager gameManager;
    Color nightLampColor;
	// Use this for initialization
	void Start () {
        streetLamps = GameObject.FindGameObjectsWithTag(Const.tagSTREETLAMP);
        foreach (GameObject streetLamp in streetLamps)
            streetLampsLights.Add(streetLamp.transform.GetChild(0).gameObject.GetComponent<Light>());
        gameManager = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER).GetComponent<GameManager>();
        isNight = gameManager.IsNight();
    //    nightLampColor = streetLampsLights[0].color;
    }
	
	// Update is called once per frame
	void Update () {

        if (isNight && !gameManager.IsNight())
        {
            foreach (Light light in streetLampsLights)
                light.color = Color.black;

            isNight = false;
        }

        if(!isNight && gameManager.IsNight())
        {
            foreach (Light light in streetLampsLights)
                light.color = nightLampColor;

            isNight = true;
        }



    }
}
