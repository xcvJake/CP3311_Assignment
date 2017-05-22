using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCloud : MonoBehaviour {
	CloudEnabler parent;
	// Use this for initialization
	void Start () {
		parent = transform.parent.GetComponent<CloudEnabler> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider B){

		parent.Ouch (B);

	}

}
