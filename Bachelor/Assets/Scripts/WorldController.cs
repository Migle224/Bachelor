using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    /*List<GameObject> workPlaces = new List<GameObject>(), turistPlaces = new List<GameObject>(), busStops = new List<GameObject>();
    GameObject[] destinations;

	// Use this for initialization
	void Start () {
        destinations = GameObject.FindGameObjectsWithTag(Const.tagDESTINATION);
        this.SplitDestinations();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateAI(GameObject _ai, Role _role, int _amount)
    {
        StartCoroutine(SponAI(_ai, _role, _amount));
    }

    IEnumerator SponAI(GameObject _ai, Role _role, int _amount)
    {
        GameObject ai;
        GameObject sponPoint = busStops[Random.Range(0, busStops.Count)];

        

        for (int i = 0; i < _amount; i++)
        {
            yield return new WaitForSeconds(Const.SPONTIME);

            ai = Instantiate(_ai, sponPoint.transform.position, sponPoint.transform.rotation);

            ai.GetComponent<AIController>().role = _role;

            ai.GetComponent<AIController>().InitSheldule(busStops, workPlaces, turistPlaces);

        }
    }

    void SplitDestinations()
    {
        DestinationInfo di;
        foreach (GameObject destination in destinations)
        {
            di = destination.GetComponent<DestinationInfo>();
            switch (di.destinationName)
            {
                case Const.BUSSTOP:
                    this.busStops.Add(destination);
                    break;
                case Const.WORKPLACE:
                case Const.PARLAMENT:
                    this.workPlaces.Add(destination);
                    break;
                case Const.CHATEDRAL:
                case Const.GEDIMINASHILL:
                case Const.KUDIRKASQUARE:
                case Const.LUKISKESSQUARE:
                case Const.MUSEUM:
                default:
                    this.turistPlaces.Add(destination);
                    break;
            }
        }
    }*/
}
