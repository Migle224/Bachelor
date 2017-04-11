using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FreeEmployeeAI : BaseAI 
{   
    List<GameObject> destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes;


    public FreeEmployeeAI(GameObject _go, List<GameObject> _destinationWorkPlaces, List<GameObject> _destiantionBusStop, List<GameObject> _destinationCaffes, List<GameObject> _destinationHomes)
    {
        this.destiantionBusStop = _destiantionBusStop;
        this.destinationCaffes = _destinationCaffes;
        this.destinationHomes = _destinationHomes;
        this.destinationWorkPlaces = _destinationWorkPlaces;
        this.go = _go;
        this.InitSheldule();
        baseAI = this.go.AddComponent<BaseAIBehaviour>();
        baseAI.ai = this;
        this.role = Role.FreeEmployee;

    }

    public override void InitSheldule()
    {
        ShelduleInfo shelduleInfo;
        int workDayStart = Random.Range(6, 11);
        GameObject workPlace;

        shelduleInfo.time = (workDayStart - 2) * 60;
        shelduleInfo.destination = destiantionBusStop[Random.Range(0, destiantionBusStop.Count - 1)];
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = workDayStart * 60;
        workPlace = destinationWorkPlaces[Random.Range(0, destinationWorkPlaces.Count - 1)];
        shelduleInfo.destination = workPlace;
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = (workDayStart + 4) * 60;
        shelduleInfo.destination = destinationCaffes[Random.Range(0, destinationCaffes.Count - 1)];
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = (workDayStart + 5) * 60;
        shelduleInfo.destination = workPlace;
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = (workDayStart + 9) * 60;
        shelduleInfo.destination = destiantionBusStop[Random.Range(0, destiantionBusStop.Count - 1)];
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = (workDayStart + 12) * 60;
        shelduleInfo.destination = destinationHomes[Random.Range(0, destinationHomes.Count - 1)];
        sheldule.Add(shelduleInfo);
    }
}
