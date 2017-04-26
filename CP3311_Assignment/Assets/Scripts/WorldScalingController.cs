using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScalingController : MonoBehaviour {

    /*
     * 
     * /----------------------------------------\
     * |       MAKE SURE THE ENVIRONMENT        |
     * |            GAMEOBJECT IS               |
     * |      AT WORLD CO-ORD: [0,0,0] !!!!     |
     * \----------------------------------------/
     *  why, because fuk u thats why.
     *  also because the cyclone repositioning moves the it towards origin. 
     */

    public float cycloneMaxSize = 10000;
    public float rescaleIncrement = 1;
    public float rescalePercent = 0.9f;
    float previousRescaleSize = 1;
    float cycloneMassLimit;
    public Transform player;

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

            // move le cyclone because the world done shrinky
            player.position = player.position * rescalePercent; 



        } 
        else if (cycloneMassLimit <= (previousRescaleSize - rescaleIncrement))
        {
            transform.localScale = transform.localScale * (2 - rescalePercent);
            previousRescaleSize = cycloneMassLimit;

            // move el cyclone because the world done a grow
            player.position = player.position * (2 - rescalePercent);

        }
    }
}
