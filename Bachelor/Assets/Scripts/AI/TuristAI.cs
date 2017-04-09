using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TuristAI : BaseAI 
{   
    List<GameObject> destinations = new List<GameObject>();


    public TuristAI(GameObject _go, List<GameObject> _destinationWorkPlaces, List<GameObject> _destiantionBusStop, List<GameObject> _destinationCaffes, List<GameObject> _destinationHomes, List<GameObject> _destinationTuristPlace)
    {
        destinations.AddRange(_destinationCaffes);
        destinations.AddRange(_destiantionBusStop);
        destinations.AddRange(_destinationTuristPlace);

        this.go = _go;
        this.InitSheldule();
        baseAI = this.go.AddComponent<BaseAIBehaviour>();
        baseAI.ai = this;       
    }

    public override void InitSheldule()
    {
        ShelduleInfo shelduleInfo;

        shelduleInfo.time = 9 * 60;
        shelduleInfo.destination = destinations[Random.Range(0, destinations.Count - 1)];
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = 12 * 60;
        shelduleInfo.destination = destinations[Random.Range(0, destinations.Count - 1)];
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = 15 * 60;
        shelduleInfo.destination = destinations[Random.Range(0, destinations.Count - 1)];
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = 18 * 60;
        shelduleInfo.destination = destinations[Random.Range(0, destinations.Count - 1)];
        sheldule.Add(shelduleInfo);

        shelduleInfo.time = 21 * 60;
        shelduleInfo.destination = destinations[Random.Range(0, destinations.Count - 1)];
        sheldule.Add(shelduleInfo);
    }
}
