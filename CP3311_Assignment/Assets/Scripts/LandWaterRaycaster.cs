using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class LandWaterRaycaster : MonoBehaviour {

	public float Health = 100;
	public float MaxHealth = 100;
	public float DamageRate = 2;
	public float HealRate = 10;
	private RaycastHit hitInfo;
	public Slider healthSlider; 


	EllipsoidParticleEmitter[] waterEffects;


	// Use this for initialization
	void Start () {
		waterEffects = GetComponentsInChildren<EllipsoidParticleEmitter> ();
		healthSlider.maxValue = MaxHealth;
	}


	// Update is called once per frame
	void FixedUpdate () {


		for (int i = 0; i < waterEffects.Length; i++) {
			waterEffects[i].emit = false;
		}

		Health -= DamageRate * Time.deltaTime;

		if (Physics.Raycast (transform.position, -transform.up, out hitInfo, 20)) {
			if (hitInfo.transform.tag == "Water") {
				Health += HealRate * Time.deltaTime;

				for (int i = 0; i < waterEffects.Length; i++) {
					waterEffects [i].emit = true;
				}


			}
		}


			
		if (Health > MaxHealth) {
				Health = MaxHealth;
		}

		healthSlider.value = Health;
	}
}