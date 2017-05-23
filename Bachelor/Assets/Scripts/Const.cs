using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AICounter
{
    Role role;
    int amount;
}

public enum DestinationType
{
    None = 0,
    WorkPlace = 1,
    CoffePlace = 2,
    BusStop = 3,
    Home = 4,
    TuristPlace = 5
}

public enum Role
{
    None = 0,
    Employee = 1,
    Turist = 2,
    Child = 3,
    Homeless = 4,
    FreeEmployee = 5,
    LoadedAI = 6
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

[System.Serializable]
public struct ShelduleByPlaces
{
    public int time;
    public DestinationType destinationType;
}

[System.Serializable]
public class ShelduleToSave
{
    public static ShelduleToSave current;
    public string description;
    public List<ShelduleByPlaces> shelduleByplaceList;
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
                MUSEUM = "Museum",
                KUDIRKASQUARE = "KudirkaSquare",
                PARLAMENT = "Parlament",
                WORKPLACE = "WorkPlace",
                tagSIMULATIONMANAGER = "SimulationManager",
                tagDESTINATION = "Destination",
                tagGAMEMANAGER = "GameManager",
                tagTRAFFICLIGHTCONTROLLER = "TrafficLightController",
                tagZEBRA = "Zebra",
                tagOBSERVER = "Observer",
                tagBUS = "Bus",
                tabBUSSTOP = "BusStop",
                tagTESTCONTOLLER = "TestController",
                tagSTREETLAMP = "StreetLamp";


    public const float SPONTIME = 0.2f;
                           
}
