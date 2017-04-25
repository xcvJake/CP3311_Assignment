using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScalingController : MonoBehaviour {

    float cycloneMassLimit;

    public float cycloneMaxSize = 10000;

    float previousRescaleSize = 1;
    public float rescaleIncrement = 1;
    public float rescalePercent = 0.9f;



    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        cycloneMassLimit = PlayerControllerJunkRewrite.cycloneMassLimit; 

        if(cycloneMassLimit >= (previousRescaleSize + rescaleIncrement))
        {
            transform.localScale = transform.localScale * rescalePercent;
            previousRescaleSize = cycloneMassLimit;
        }
	}
}
