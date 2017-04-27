using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJunkRewrite : MonoBehaviour
{

	public int junkRotationSpeed = 10;
	//	public float junkHeightMax = 6f;
	public float junkHeightMin = 1f;
	public static float cycloneMassLimit = 1f;

	public int maxItemsForced = 400;
	// public float testingMassAdjust = 1f; //testing thing

	Transform orbitingJunk;
	CapsuleCollider cycloneCollider;

	//public int junkCount = 100;
	public float junkPercentMultiplier = 0.1f;

	public float turningSpeed = 100;
	public float movementSpeed = 10;


	private void Start ()
	{
		orbitingJunk = transform.FindChild ("OrbitingJunk");
		cycloneCollider = transform.GetComponent<CapsuleCollider> ();
	}

	void Awake ()
	{

	}

	void FixedUpdate ()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Move the player around the scene.
		Move (h, v);

		// Rotate the Junk around the player
		rotateJunk ();
		spinJunk ();
	}

	void Update ()
	{
		// Testing thing
		//cycloneMassLimit = testingMassAdjust;

		cleanupCyclone (); //This must be in Update if framerate depending cleanup is wanted
	}

	int numberOfFPSLoops = 0;

	void cleanupCyclone ()
	{

		int FPSLimit = 20; //Note this is not real FPS, it includes drawing time and whatnot, so kinda hovers around half the fps

		if (1.0f / Time.deltaTime < FPSLimit) { //If FPS < 30, start removing items. So higher end pcs can enjoy a large number of items
			numberOfFPSLoops++;
			if (numberOfFPSLoops > FPSLimit / 2) { //Make sure fps drop has been going on longer than half a second
				
				int childKillingPower = FPSLimit - (int)(1.0f / Time.deltaTime);
				//childKillingPower = childKillingPower * childKillingPower; 	//EXPONENTIAL CHILD KILLING POWER WAHAHAHA 
				childKillingPower = childKillingPower * 30; // Ok, note to self, too much child killing, lets tone it down. Murderer.
				childKillingPower = maxItemsForced < transform.childCount ? transform.childCount-maxItemsForced : childKillingPower; //Forced Max Items Check
				childKillingPower = childKillingPower > orbitingJunk.childCount ? orbitingJunk.childCount : childKillingPower; //Dont overkill the cyclone check

				for (int i = 0; i < childKillingPower; i++) {
					Destroy (orbitingJunk.GetChild (i).gameObject);
				}
				numberOfFPSLoops = 0;
			}

		} else {
			numberOfFPSLoops = 0;

		}

	}

	void Move (float h, float v)
	{

		float horizontal = h * turningSpeed * Time.deltaTime;
		transform.Rotate (0, horizontal, 0);

		float verticalTranslate = v * movementSpeed * Time.deltaTime;
		float horizontalTranslate = 0;
		transform.Translate (horizontalTranslate, 0, verticalTranslate);


	}

	void OnTriggerEnter (Collider other)
	{
		if ((other.tag == "pickup") && (other.attachedRigidbody.mass <= cycloneMassLimit)) {
			cycloneMassLimit += other.attachedRigidbody.mass * junkPercentMultiplier; 
			//TODO: Increase size of collider, both width, height and Y offset based off cycloneMass (This will have to sync with increasing partical sizes or whatever)
			//TODO: Increase size of actual cyclone 

			other.isTrigger = false;
			other.attachedRigidbody.useGravity = false;
			other.attachedRigidbody.isKinematic = true;

			other.transform.parent = orbitingJunk.transform;

			//Add a random height, makes the orbiting look a little nicer, from the ground to height of collider
			other.transform.position = new Vector3(other.transform.position.x, transform.position.y,other.transform.position.z);
			Vector3 randomHeight = other.transform.position + (transform.up.normalized * Random.Range (junkHeightMin, cycloneCollider.height));
			other.transform.position = randomHeight;

		}
	}


	private void spinJunk ()
	{
		foreach (Transform child in orbitingJunk.transform) {
			child.Rotate (Random.Range (0f, 5f), Random.Range (0f, 5f), Random.Range (0f, 5f));

		}
	}


	private void rotateJunk ()
	{
		Vector3 rotateDirection = new Vector3 (0f, 1f, 0f);
		orbitingJunk.transform.Rotate (rotateDirection * junkRotationSpeed);
	}
}