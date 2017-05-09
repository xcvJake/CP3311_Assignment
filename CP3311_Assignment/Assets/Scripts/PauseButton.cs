using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {
	public GameObject DesiredPauseMenu;
	public GameObject OptionsMenuGlitchRemoval;

	// Use this for initialization
	void Start () {
		
	}

	bool paused = false;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {

			if (paused) {
				//Unpause
				Time.timeScale = 1.0f;
				paused = false;
				DesiredPauseMenu.SetActive (false);
				OptionsMenuGlitchRemoval.SetActive (false);

			} else {
				//Pause
				Time.timeScale = 0.0f;
				paused = true;
				DesiredPauseMenu.SetActive (true);

			}
		}
	}
}
