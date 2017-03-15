using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text timeText;
    int hours, minutes;
    public int speed = 1;
    int timeToShow;
    const float TIMESTARTMODIFIER = 0.2f;
    GameController gameController;

    public InputField employeeAmount, turistAmount, childAmount, homelessAmount, noneAmount, freeEmployeeAmount;

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindGameObjectWithTag(Const.tagGAMECONTROLLER).GetComponent<GameController>();
        speed = gameController.gameObject.GetComponent<GameValues>().speed;
	}
	
	// Update is called once per frame
	void Update () {
        timeToShow = (int)(Time.timeSinceLevelLoad * speed * TIMESTARTMODIFIER);
        hours = (timeToShow /  60) % 24;
        minutes = timeToShow % 60;
        timeText.text = hours +" hours " + minutes + "min.";
	}

    public void GanerateAI()
    {
        gameController.GenerateAI(Role.Employee, int.Parse(employeeAmount.text));
        gameController.GenerateAI(Role.FreeEmplyee, int.Parse(freeEmployeeAmount.text));
        gameController.GenerateAI(Role.Homeless, int.Parse(homelessAmount.text));
        gameController.GenerateAI(Role.Turist, int.Parse(turistAmount.text));
        gameController.GenerateAI(Role.Child, int.Parse(childAmount.text));
    }
}
