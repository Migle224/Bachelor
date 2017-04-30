using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, ObserverUI
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

    public Text rolesCount, destinationsCount, enabledCount, disabledCount;

    Observer observer;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER).GetComponent<GameManager>();
        employeeAmount.text = "0";
        turistAmount.text = "0";
        homelessAmount.text = "0";
        freeEmployeeAmount.text = "0";
        this.LoadDropDownAI();

        rolesCount.text = "line1 \n line2 \n line 3";

        observer = GameObject.FindGameObjectWithTag(Const.tagOBSERVER).GetComponent<Observer>();
        observer.AddUI(this);
    }
	
	void Update () {
        timeToShow = (int)gameManager.timeOfDay;
        hours = (timeToShow /  60) % 24;
        minutes = timeToShow % 60;
        timeText.text = hours +" : " + minutes + " h";
	}

    public void UpdateObserverUI()
    {
        this.UpdateCountUI(observer.GetDestianionsStatistics());
        this.UpdateEnabledUI(observer.GetEnabledStatistics());
        this.UpdateRoleUI(observer.GetRolesStatistics());
    }

    void UpdateCountUI(SortedList<DestinationType, int> _destinationsCount)
    {
        if (destinationsCount != null)
            destinationsCount.text = "";

        foreach (KeyValuePair<DestinationType, int> destination in _destinationsCount)
        {
            if (destinationsCount != null)
                destinationsCount.text += destination.Key + " : " + destination.Value + "\n";
        }
        
    }

    void UpdateRoleUI(SortedList<Role, int> _rolesCount)
    {
        if (rolesCount != null)
            rolesCount.text = "";

        foreach (KeyValuePair<Role, int> roles in _rolesCount)
        {
            if (rolesCount != null)
                rolesCount.text += roles.Key + " : " + roles.Value + "\n";
        }
        
    }

    void UpdateEnabledUI(SortedList<bool, int> _enabledCount)
    {
       /* if(_enabledCount.ContainsKey(true) && _enabledCount != null)
            enabledCount.text = "Enabled: " + _enabledCount[true];

        if(_enabledCount.ContainsKey(false) && _enabledCount != null)
            disabledCount.text = "Disabled: " + _enabledCount[false];*/
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
