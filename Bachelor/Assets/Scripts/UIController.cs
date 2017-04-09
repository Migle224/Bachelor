﻿using System.Collections;
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
    public GameObject createUserAIPanel;

    public InputField employeeAmount, turistAmount, homelessAmount, freeEmployeeAmount;

    public Dropdown usersAIDropdown;
    public InputField userAIAmount;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER).GetComponent<GameManager>();
        employeeAmount.text = "0";
        turistAmount.text = "0";
        homelessAmount.text = "0";
        freeEmployeeAmount.text = "0";
        this.LoadDropDownAI();
    }
	
	void Update () {
        timeToShow = (int)gameManager.timeOfDay;
        hours = (timeToShow /  60) % 24;
        minutes = timeToShow % 60;
        timeText.text = hours +" hours " + minutes + "min.";
	}

    public void SpeedUpTime()
    {
        gameManager.SpeedUpTime();
    }

    public void SlowDownTime()
    {
        gameManager.SlowDownTime();
    }

    public void InstantiateAI()
    {
        gameManager.InstantiateAI(Role.Employee, int.Parse(employeeAmount.text));
        gameManager.InstantiateAI(Role.FreeEmployee, int.Parse(freeEmployeeAmount.text));
        gameManager.InstantiateAI(Role.Homeless, int.Parse(homelessAmount.text));
        gameManager.InstantiateAI(Role.Turist, int.Parse(turistAmount.text));
    }

    public void ShowHideUserAIPanel()
    {
        createUserAIPanel.SetActive(!createUserAIPanel.active);
    }

    public void InstantaiteLoadedAI()
    {
        gameManager.InstantiateAI(usersAIDropdown.transform.GetChild(0).GetComponent<Text>().text, int.Parse(userAIAmount.text));
    }

    void LoadDropDownAI()
    {
        usersAIDropdown.AddOptions(gameManager.GetLoadedAINames());
    }

}
