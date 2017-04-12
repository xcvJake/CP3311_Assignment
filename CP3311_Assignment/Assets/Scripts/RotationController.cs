using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

    public GameObject player;

    bool playerTrigger;


	// Use this for initialization
	void Start () {
        playerTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        playerTrigger = true;
            
    //    }
    //}


    //private void FixedUpdate()
    //{
    //    // If the player has triggered the object, start rotating around the player
    //    if (playerTrigger)
    //    {
    //        transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
    //    }
    //}
}
