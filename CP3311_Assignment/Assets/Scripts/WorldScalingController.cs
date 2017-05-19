using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScalingController : MonoBehaviour
{

	//public float cycloneMaxSize = 10000;
	//public float rescaleIncrement = 1;
	//public float rescalePercent = 0.9f;
	//float previousRescaleSize = 1;
	//float cycloneMassLimit;


	public Transform player;
	public float scalingAmount = 0.1f;
	//WARNING: This current value results in an exponential curve, not sure if this is what we wanted / warning
	//Might want to replace this with some nice sort of curve, but is kinda fun
	public float smallestScale = 0.1f;
	float startingWeight;
	Vector3 startingWorldScale;
	Transform orbitingStuff;

	public float maxScaleLimit = 200;



	void Start ()
	{
		startingWeight = PlayerControllerJunkRewrite.cycloneMassLimit; //Allows us to start with any mass and not scale the world on start
		startingWorldScale = transform.localScale; //Allows us to start with the map at any scale
		orbitingStuff = player.Find("OrbitingJunk");
	}


	void Update ()
	{
		
		//Scale the world based on the mass of the cyclone.  
		Vector3 newScale = startingWorldScale - (new Vector3 (scalingAmount, scalingAmount, scalingAmount) * ((PlayerControllerJunkRewrite.cycloneMassLimit - startingWeight)));

		if (newScale.x > smallestScale) {
			//Based on the scaling we will do, we will need to move the piviot point, so as to seem like we are scaling around a point
			float RelativeScale = (newScale.x / transform.localScale.x);
			if (RelativeScale != 1) {

				//The maths
				Vector3 vectorBetweenEnvironmentAndPlayer = (transform.position - player.position);
				transform.position = (vectorBetweenEnvironmentAndPlayer * RelativeScale) + player.position;
				transform.localScale = newScale.x < smallestScale ? new Vector3 (smallestScale, smallestScale, smallestScale) : newScale;


				//Also update the items in the cyclone, is bad that it is here, but makes the maths and transfer of information so much easier
				orbitingStuff.transform.localScale = orbitingStuff.transform.localScale * RelativeScale;

			}
			
		} 




	}
}


