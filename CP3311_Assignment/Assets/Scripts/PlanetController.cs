using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

	public float speed = 10f;            // The speed that the player will move at.


	Vector3 movement;                   // The vector to store the direction of the player's movement.



	private void Start()
	{
		
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


	}



	void Move(float h, float v)
	{

				// Set the movement vector based on the axis input.
		movement.Set(-v, 0f, h);  //These are swapped since this is the perspective of the planet not the player ;)

		// Normalise the movement vector and make it proportional to the speed per second.
		transform.Rotate(movement, speed * Time.deltaTime, Space.World);


	}


}
