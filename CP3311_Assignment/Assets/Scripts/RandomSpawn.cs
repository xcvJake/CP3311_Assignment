using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {

	public Transform[] teleportLocations;
	public GameObject[] spawnableObjects;
	GameObject parentObject;

	// Use this for initialization
	void Start () {
		parentObject = GameObject.Find ("SpawnedStuff");
	}

	int itemsSpawned = 0;
	int maxItemsSpawned = 400;

	void FixedUpdate () { //We want to call fixed update since we want the item spawn rate to be uneffected by fps. As we use the intro cyclone for benchmarking
		if (itemsSpawned < maxItemsSpawned) {
			int i = Random.Range (0, spawnableObjects.Length);
			int j = Random.Range (0, teleportLocations.Length);

			var A = Instantiate (spawnableObjects[i], teleportLocations[j].position, teleportLocations[j].rotation);
			A.transform.parent = parentObject.transform;
			var B = Instantiate (spawnableObjects[i], teleportLocations[j].position, teleportLocations[j].rotation);
			B.transform.parent = parentObject.transform;
			var C = Instantiate (spawnableObjects[i], teleportLocations[j].position, teleportLocations[j].rotation);
			C.transform.parent = parentObject.transform;
			itemsSpawned++;
		}
	}




}
