using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    AIController aiController;
    public WorldController wordController;

    public GameObject aiPrefab;

    // Use this for initialization
    void Start () {

        this.AssignAISheldule();
        	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateAI(Role _role, int _amount)
    {
        wordController.GenerateAI(aiPrefab, _role, _amount);
    }

    void AssignAISheldule() { }
}
