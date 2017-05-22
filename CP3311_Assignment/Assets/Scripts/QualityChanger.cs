using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QualityChanger : MonoBehaviour {

	public Transform qualitySlider; //SET ME
	Slider slider;


	void Start () {
		string[] names = QualitySettings.names; //Grab the names of all the quality settings
		slider = qualitySlider.GetComponent<Slider> (); //We are only using that info to see how many options there are for the slider notches
		slider.maxValue = names.Length - 1;

		slider.value = QualitySettings.GetQualityLevel (); //Make sure the slider matches the quality settings on boot

		slider.onValueChanged.AddListener (OnSliderChange); //Add a listener so we only change quality when the slider moves.
	}
	

	void OnSliderChange(float wow){
		QualitySettings.SetQualityLevel((int)wow, true); //Change that shit 
	}
}
