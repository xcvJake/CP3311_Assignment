using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class LandWaterRaycaster : MonoBehaviour {

	public float Health = 100;
	public float MaxHealth = 100;
	public float DamageRate = 1;
	public float HealRate = 10;
	private RaycastHit hitInfo;


	EllipsoidParticleEmitter[] waterEffects;


	// Use this for initialization
	void Start () {
		waterEffects = GetComponentsInChildren<EllipsoidParticleEmitter> ();
	}


	// Update is called once per frame
	void FixedUpdate () {


		for (int i = 0; i < waterEffects.Length; i++) {
			waterEffects[i].emit = false;
		}

		Health -= DamageRate * Time.deltaTime;

		if (Physics.Raycast (transform.position, -transform.up, out hitInfo, 10)) {
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
		}
	}