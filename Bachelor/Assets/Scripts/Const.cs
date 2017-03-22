﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AICounter
{
    Role role;
    int amount;
}

public enum DestinationType
{
    WorkPlace = 1,
    CoffePlace = 2,
    BusStop = 3,
    Home = 4
}

public enum Role
{
    None = 0,
    Employee = 1,
    Turist = 2,
    Child = 3,
    Homeless = 4,
    FreeEmplyee = 5
}

public struct ShelduleInfo
{
    public int time;
    public GameObject destination;
}
public struct RoleInfo
{
    public Role role;
    public List<ShelduleInfo> sheldule;

}

public struct DestinationInformation
{
    public float probabillity;
    public GameObject destiantion;
}



public static class Const  {

    public const string BUSSTOP = "BusStop",
                CHATEDRAL = "Chatedral",
                GEDIMINASHILL = "GediminasHill",
                LUKISKESSQUARE = "LukiskesSquare",
                MUSEUM  = "Museum",
                KUDIRKASQUARE = "KudirkaSquare",
                PARLAMENT = "Parlament",
                WORKPLACE = "WorkPlace",
                tagDESTINATION = "Destination",
                tagGAMECONTROLLER = "GameController";

    public const float SPONTIME = 0.2f;
                           
}