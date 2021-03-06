﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOrbit : MonoBehaviour {

    public float speed = 10f;            // The speed that the player will move at.
    public int junkRotationSpeed = 250;
    public float junkTranslateSpeed = 0.01f;
    public float junkHeightMax = 6f;
    public float junkHeightMin = 1f;
    public float cycloneMassLimit = 1f; 
	public GameObject planet;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    float[] junkHeightArray;
    int[] junktranslateDirectionArray;
    int junkArrayIndex;

    Transform orbitingJunk;

    public int junkCount = 100;
    public float junkPercentMultiplier = 0.1f;


    private void Start()
    {
        junkHeightArray = new float[junkCount];
        junktranslateDirectionArray = new int[junkCount];
        junkArrayIndex = 0;
        orbitingJunk = transform.Find("OrbitingJunk");
    }

    void Awake()
    {
        // Set up references.
        playerRigidbody = GetComponent<Rigidbody>();
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
    }

    //private void Update()
    //{
    //    foreach (Transform child in orbitingJunk.transform)
    //    {
    //        //child is your child transform
    //        if (child.gameObject.tag == "Building")
    //        {
    //            child.transform.Rotate(2f, 2f, 5f);
    //        }
    //    }
    //}

    void Move(float h, float v)
    {

		Vector3 normal = ( transform.position - planet.transform.position );
		transform.rotation = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;



        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
		movement = transform.rotation * movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "pickup") && (other.attachedRigidbody.mass <= cycloneMassLimit))
        {
            cycloneMassLimit += other.attachedRigidbody.mass * junkPercentMultiplier; 

            // delete junk
            if (orbitingJunk.transform.childCount >= junkCount)
            {
                Destroy(orbitingJunk.GetChild(0).gameObject);
            }


            other.isTrigger = false;
            //other.attachedRigidbody.detectCollisions = false;
            //other.attachedRigidbody.isKinematic = false;
            other.attachedRigidbody.useGravity = false;

            other.transform.parent = orbitingJunk.transform;

            junkHeightArray[junkArrayIndex] = other.transform.position.y; // starts at where it is
            junktranslateDirectionArray[junkArrayIndex] = 1; // Up by default
            
            if (junkArrayIndex >= junkCount-1)
            {
                junkArrayIndex = 0;
            }
            else
            {
                junkArrayIndex++;
            }
        }
    }

    private void rotateJunk()
    {
        int junkPieceIndex = 0; 
        foreach (Transform child in orbitingJunk.transform)
        {
            //child is your child transform
            if (child.gameObject.tag == "pickup")
            {
                // Rotates Child Junk Piece around Cyclone
                child.RotateAround(transform.position, new Vector3(0, 1, 0), junkRotationSpeed*Time.deltaTime);
                
                if (orbitingJunk.childCount > junkCount)
                {
                    // if there are more junk pieces in the orbit than desired, delete the fuckers
                    Destroy(orbitingJunk.GetChild(0).gameObject);
                }
                else
                {
                    // Else Rotate the junk like normal
                    // Is the junk already above the maximum? 
                    if (junkHeightArray[junkPieceIndex] >= transform.position.y + junkHeightMax)
                    {
                        // junk go down
                        junktranslateDirectionArray[junkPieceIndex] = -1;
                    }
                    else if (junkHeightArray[junkPieceIndex] <= transform.position.y + junkHeightMin)
                    {
                        // junk go up
                        junktranslateDirectionArray[junkPieceIndex] = 1;
                    }

                    // Increase/Decrease the junk height based on direction
                    //junkHeightArray[junkPieceIndex] += (junktranslateDirectionArray[junkPieceIndex] * junkHeightSine);

                    // Move the junk to the new location
                    //child.Translate(new Vector3(0, Mathf.Sin(junkHeightArray[junkPieceIndex])/10, 0));
                    child.Translate(new Vector3(0, (junktranslateDirectionArray[junkPieceIndex] * junkTranslateSpeed), 0));

                    junkHeightArray[junkPieceIndex] = child.transform.position.y;



                    // Pull the junk back into the center
                    child.Translate((transform.GetChild(0).transform.position - child.position) * 0.0001f);




                    // next junk piece in array please
                    junkPieceIndex++;
                }
            }
        }
    }
}
