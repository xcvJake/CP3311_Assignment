using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuScaleSingleItems : MonoBehaviour {

	public float targetScale;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//var A = new Vector3 (targetScale, targetScale, targetScale);
		foreach (Transform child in transform) {
			child.localScale = targetScale * child.localScale;
		} 
	}
}
