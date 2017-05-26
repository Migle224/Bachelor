using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour {

    SortedList<int, BaseAIBehaviour> aiToNotify = new SortedList<int, BaseAIBehaviour>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Const.tagAGENT)
            aiToNotify.Add(other.gameObject.GetInstanceID(), other.gameObject.GetComponent<BaseAIBehaviour>());
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Const.tagAGENT)
        {

            
                int index = other.gameObject.GetInstanceID();
                if (aiToNotify.ContainsKey(index) && other.gameObject.active)
                    aiToNotify.Remove(index);
            

           
        }
    }

    public void TrafficSignalChanged(TrafficSignals _signal)
    {
        foreach (KeyValuePair<int, BaseAIBehaviour> ai in aiToNotify)
            ai.Value.CheckTrafficLightSignal();
    }
}
