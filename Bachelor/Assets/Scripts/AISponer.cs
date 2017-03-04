using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISponer : MonoBehaviour {

    // Use this for initialization
    public GameObject AIprefab;
    public int aiNumber;
    public float sponTime;

	void Start () {
        StartCoroutine(SponAI());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SponAI()
    {
        for (int i = 0; i < aiNumber; i++)
        {
            yield return new WaitForSeconds(sponTime);
            Instantiate(AIprefab, Vector3.zero, gameObject.transform.rotation);
        }
    }
}
