using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; 

public class GameOverTVTubeScript : MonoBehaviour {

	VideoPlayer videoPlayer; 
	bool videoPlayed;
	public float playerMassLimit = 100f;

	void Start()
	{
		videoPlayer = gameObject.GetComponent<VideoPlayer>();
		videoPlayer.isLooping = false;
		videoPlayed = false;
	}

	void Update () {
		if (PlayerControllerJunkRewrite.cycloneMassLimit > playerMassLimit && !videoPlayed) {
			videoPlayed = true;
			videoPlayer.Play ();
		} else if (videoPlayed && !videoPlayer.isPlaying) {
			
			// Game over screen goes here.
			//TODO: 
		}
	}
}