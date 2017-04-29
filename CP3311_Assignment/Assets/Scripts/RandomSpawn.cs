using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {

	public Transform[] teleportLocations;
	public GameObject[] spawnableObjects;

	// Use this for initialization
	void Start () {
		
	}

	int itemsSpawned = 0;
	int maxItemsSpawned = 500;

	void FixedUpdate () { //We want to call fixed update since we want the item spawn rate to be uneffected by fps. As we use the intro cyclone for benchmarking
		if (itemsSpawned < maxItemsSpawned) {
			int i = Random.Range (0, spawnableObjects.Length);
			int j = Random.Range (0, teleportLocations.Length);
			Instantiate (spawnableObjects[i], teleportLocations[j].position, teleportLocations[j].rotation);
			itemsSpawned++;
		}
	}
}
