using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; 
using UnityEngine.Audio; 

public class GameOverTVTubeScript : MonoBehaviour {

	VideoPlayer videoPlayer; 
	bool videoPlayed;
	public float playerMassLimit = 100f;
	public GameObject gameOverScreen;
	public GameObject HUD;
	public GameObject Lightning;
	AudioSource audio;
	public GameObject ambientSound;
	AudioSource ambientAudio;

	void Start()
	{
		videoPlayer = gameObject.GetComponent<VideoPlayer>();
		videoPlayer.isLooping = false;
		videoPlayed = false;
		audio = GetComponent<AudioSource>();
		ambientAudio = ambientSound.GetComponent<AudioSource> ();
	}

	void Update () {
		if (PlayerControllerJunkRewrite.cycloneMassLimit > playerMassLimit && !videoPlayed) {
			videoPlayed = true;
			HUD.SetActive (false);
			Lightning.SetActive (false);
			ambientAudio.Stop (); 
			audio.Play();
			videoPlayer.Play ();
		} else if (videoPlayed && !videoPlayer.isPlaying) {
			if (!gameOverScreen.activeSelf) { 
				
				gameOverScreen.SetActive (true);

				// Game over screen goes here.
				//TODO:

			}
		}
	}
}