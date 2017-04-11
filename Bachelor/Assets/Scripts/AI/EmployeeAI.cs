using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EmployeeAI : BaseAI 
{   
    List<GameObject> destinationWorkPlaces, destiantionBusStop, destinationCaffes, destinationHomes;


    public EmployeeAI(GameObject _go, List<GameObject> _destinationWorkPlaces, List<GameObject> _destiantionBusStop, List<GameObject> _destinationCaffes, List<GameObject> _destinationHomes)
    {
        this.destiantionBusStop = _destiantionBusStop;
        this.destinationCaffes = _destinationCaffes;
        this.destinationHomes = _destinationHomes;
        this.destinationWorkPlaces = _destinationWorkPlaces;
        this.go = _go;
        this.InitSheldule();
        baseAI = this.go.AddComponent<BaseAIBehaviour>();
        baseAI.ai = this;
        this.role = Role.Employee;

    }

    public override void InitSheldule()
    {
        ShelduleInfo shelduleInfo;
        GameObject workPlace;

        shelduleInfo.time = 6 * 60;
        shelduleInfo.destination = destiantionBusStop[Random.Range(0, destiantionBusStop.Count - 1)];
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = 8 * 60;
        workPlace = destinationWorkPlaces[Random.Range(0, destinationWorkPlaces.Count - 1)];
        shelduleInfo.destination = workPlace;
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = 12 * 60;
        shelduleInfo.destination = destinationCaffes[Random.Range(0, destinationCaffes.Count - 1)];
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = 13 * 60;
        shelduleInfo.destination = workPlace;
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = 17 * 60;
        shelduleInfo.destination = destiantionBusStop[Random.Range(0, destiantionBusStop.Count - 1)];
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = 20 * 60;
        shelduleInfo.destination = destinationHomes[Random.Range(0, destinationHomes.Count - 1)];
        sheldule.Add(shelduleInfo);
    }
}
