using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mod : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.triangles.Length/3 );
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
