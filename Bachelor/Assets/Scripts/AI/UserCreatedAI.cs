using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class UserCreatedAI : BaseAI 
{
    List<GameObject> destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes, destinationsTuristPlace;
    List<ShelduleByPlaces> shelduleByPlaces;


    public UserCreatedAI(GameObject _go, 
                        List<ShelduleByPlaces> _sheldule, 
                        List<GameObject> _destinationWorkPlaces, 
                        List<GameObject> _destiantionBusStop, 
                        List<GameObject> _destinationCaffes, 
                        List<GameObject> _destinationHomes, 
                        List<GameObject> _destinationTuristPlace)
    {

        this.destiantionBusStop = _destiantionBusStop;
        this.destinationCaffes = _destinationCaffes;
        this.destinationHomes = _destinationHomes;
        this.destinationWorkPlaces = _destinationWorkPlaces;
        this.destinationsTuristPlace = _destinationTuristPlace;
        this.shelduleByPlaces = _sheldule;
        this.go = _go;
        this.InitSheldule();
        baseAI = this.go.AddComponent<BaseAIBehaviour>();
        baseAI.ai = this;
        this.role = Role.LoadedAI;
    }

    public override void InitSheldule()
    {
        ShelduleInfo shelduleInfo;
        GameObject workPlace = null, home = null;

        foreach (ShelduleByPlaces infoByPlace in shelduleByPlaces)
        {
            switch (infoByPlace.destinationType)
            {
                case DestinationType.BusStop:
                    shelduleInfo.destination = destiantionBusStop[Random.Range(0, destiantionBusStop.Count - 1)];
                    shelduleInfo.time = infoByPlace.time;
                    sheldule.Add(shelduleInfo);
                    break;
                case DestinationType.CoffePlace:
                    shelduleInfo.destination = destinationCaffes[Random.Range(0, destinationCaffes.Count - 1)];
                    shelduleInfo.time = infoByPlace.time;
                    sheldule.Add(shelduleInfo);
                    break;
                case DestinationType.Home:
                    if (home == null)
                        home = destinationWorkPlaces[Random.Range(0, destinationWorkPlaces.Count - 1)];

                    shelduleInfo.destination = home;
                    shelduleInfo.time = infoByPlace.time;
                    sheldule.Add(shelduleInfo);
                    break;
                case DestinationType.TuristPlace:
                    shelduleInfo.destination = destinationsTuristPlace[Random.Range(0, destinationsTuristPlace.Count - 1)];
                    shelduleInfo.time = infoByPlace.time;
                    sheldule.Add(shelduleInfo);
                    break;
                case DestinationType.WorkPlace:
                    if(workPlace == null)
                        workPlace = destinationWorkPlaces[Random.Range(0, destinationWorkPlaces.Count - 1)];

                    shelduleInfo.destination = workPlace;
                    shelduleInfo.time = infoByPlace.time;
                    sheldule.Add(shelduleInfo);
                    break;

            }
        }

        
    }
}
