using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void loadThatLevel(string levelName){
//		Application.LoadLevel(levelName);
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (levelName);
	}

	public void byebye(){

		Application.Quit();

	}
}
