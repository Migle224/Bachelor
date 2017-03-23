using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Text timeText;
    int hours, minutes;
    public int speed = 1;
    int timeToShow;
    const float TIMESTARTMODIFIER = 0.2f;
    GameManager gameManager;

    public InputField employeeAmount, turistAmount, childAmount, homelessAmount, noneAmount, freeEmployeeAmount;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER).GetComponent<GameManager>();
        speed = gameManager.speed;
	}
	
	// Update is called once per frame
	void Update () {
        timeToShow = (int)(Time.timeSinceLevelLoad * this.speed * TIMESTARTMODIFIER);
        hours = (timeToShow /  60) % 24;
        minutes = timeToShow % 60;
        timeText.text = hours +" hours " + minutes + "min.";
	}

}
