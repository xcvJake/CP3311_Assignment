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
	int maxItemsSpawned = 300;
	// Update is called once per frame
	void Update () {
		if (itemsSpawned < maxItemsSpawned) {
			int i = Random.Range (0, spawnableObjects.Length);
			int j = Random.Range (0, teleportLocations.Length);
			Instantiate (spawnableObjects[i], teleportLocations[j].position, teleportLocations[j].rotation);
			itemsSpawned++;
		}
	}
}
