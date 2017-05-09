using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnabler : MonoBehaviour {

	public string nextLevel;
	public int flashingLimit;
	public GameObject LoadingScreenObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerControllerJunkRewrite.cycloneMassLimit > flashingLimit) { //If it is time for the clouds to start flashing
			foreach (Transform child in transform) {
				child.GetComponent<Animator> ().enabled = true;

			}
		}
	}


	public void Ouch(Collider A){
		if (A.CompareTag ("Player") && PlayerControllerJunkRewrite.cycloneMassLimit > flashingLimit) {
			print ("LOADING");
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (nextLevel);
			LoadingScreenObject.SetActive (true);
		}
	}
}
