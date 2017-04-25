using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJunkRewrite : MonoBehaviour {

	public int junkRotationSpeed = 250;
//	public float junkHeightMax = 6f;
	public float junkHeightMin = 1f;
	public static float cycloneMassLimit = 1f; 

	Transform orbitingJunk;
	CapsuleCollider cycloneCollider;

	public int junkCount = 100;
	public float junkPercentMultiplier = 0.1f;

	public float turningSpeed = 100;
	public float movementSpeed = 10;


	private void Start()
	{
		orbitingJunk = transform.FindChild("OrbitingJunk");
		cycloneCollider = transform.GetComponent<CapsuleCollider> ();
	}

	void Awake()
	{

	}

	void FixedUpdate()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		// Move the player around the scene.
		Move(h, v);

		// Rotate the Junk around the player
		rotateJunk();
		spinJunk();
	}

	void Update()
	{
		cleanupCyclone (); //This must be in Update if framerate depending cleanup is wanted
	}

	void cleanupCyclone(){
		// delete junk. This CANT be a while, since childCount takes time to update, and by then objects have been deleted and unity freezes/crashes PC, ask me how I know
//		if (orbitingJunk.transform.childCount >= junkCount)
//		{
//			Destroy(orbitingJunk.GetChild(0).gameObject);
//		}
		if (1.0f/Time.deltaTime < 30 && orbitingJunk.transform.childCount > 10) //If FPS < 30, start removing items. So higher end pcs can enjoy a large number of items
		{
			Destroy(orbitingJunk.GetChild(0).gameObject);
		}

	}

	void Move(float h, float v)
	{

		float horizontal = h * turningSpeed * Time.deltaTime;
		transform.Rotate(0, horizontal, 0);

		float verticalTranslate = v * movementSpeed * Time.deltaTime;
		float horizontalTranslate = 0;
		transform.Translate(horizontalTranslate, 0, verticalTranslate);


	}

	void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "pickup") && (other.attachedRigidbody.mass <= cycloneMassLimit))
		{
			cycloneMassLimit += other.attachedRigidbody.mass * junkPercentMultiplier; 
			//TODO: Increase size of collider, both width, height and Y offset based off cycloneMass (This will have to sync with increasing partical sizes or whatever)
			//TODO: Increase size of actual cyclone 

			other.isTrigger = false;
			other.attachedRigidbody.useGravity = false;
		
			other.transform.parent = orbitingJunk.transform;

			//Add a random height, makes the orbiting look a little nicer, from the ground to height of collider
			Vector3 randomHeight = other.transform.position + (transform.up.normalized * Random.Range(junkHeightMin, cycloneCollider.height));
			other.transform.position = randomHeight;

		}
	}


	private void spinJunk()
	{
		foreach (Transform child in orbitingJunk.transform) {
			child.Rotate (Random.Range(0f, 5f), Random.Range(0f, 5f), Random.Range(0f, 5f));

		}
	}


	private void rotateJunk()
	{
		Vector3 rotateDirection = new Vector3 (0f, 1f, 0f);
		orbitingJunk.transform.Rotate(rotateDirection * junkRotationSpeed);
	}
}