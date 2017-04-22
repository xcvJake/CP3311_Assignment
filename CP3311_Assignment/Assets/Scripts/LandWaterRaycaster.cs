using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class LandWaterRaycaster : MonoBehaviour {

	public float Health = 100;
	public float MaxHealth = 100;
	public float DamageRate = 1;
	public float HealRate = 5;
	private RaycastHit hitInfo;


	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		if(Physics.Raycast(transform.position, -transform.up, out hitInfo , 10)){
			if (hitInfo.transform.tag == "Water") {
				Health += HealRate * Time.deltaTime;
			}
			else {
				Health += -1 * DamageRate * Time.deltaTime;
			}


			if (Health > MaxHealth) {
				Health = MaxHealth;
			}
		}
	}
}
