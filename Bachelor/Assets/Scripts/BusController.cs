using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour {

    List<GameObject> passangers = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddToBus(GameObject _passanger)
    {
        passangers.Add(_passanger);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Const.tagDESTINATION:
                //todo
                foreach (GameObject gj in passangers)
                {                 
                    gj.SetActive(true);
                    gj.transform.parent = null;
                    gj.transform.position = other.transform.position;
                    gj.GetComponent<BaseAIBehaviour>().ResumeMovement();
                }
                passangers.Clear();
                break;
        }
    }
}
