using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float timeOfDay;
    public int speed = 20;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeOfDay = speed * Time.timeSinceLevelLoad % (24 * 60);
	}
}
