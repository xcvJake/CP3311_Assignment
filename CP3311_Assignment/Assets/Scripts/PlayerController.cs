using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;            // The speed that the player will move at.
    public int junkRotationSpeed = 250;
    public float junkTranslateSpeed = 0.01f;
    public float junkHeightMax = 6f;
    public float junkHeightMin = 3f; 

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    float[] junkHeightArray;
    int[] junktranslateDirectionArray;
    int junkArrayIndex;

    Transform orbitingJunk;

    public int junkCount = 10;


    private void Start()
    {
        junkHeightArray = new float[10];
        junktranslateDirectionArray = new int[10];
        junkArrayIndex = 0;
        orbitingJunk = transform.FindChild("OrbitingJunk");
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

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Building")
        {


            // delete junk
            if (orbitingJunk.transform.childCount >= junkCount)
            {
                Destroy(orbitingJunk.GetChild(0).gameObject);
            }




            other.transform.parent = orbitingJunk.transform; 
            //other.isTrigger = false;
            //other.attachedRigidbody.detectCollisions = false;
            //other.attachedRigidbody.isKinematic = false;
            //other.attachedRigidbody.useGravity = false;









            //transform.parent = other.transform;

            //if(floatingJunk[junkIndex] != null)
            //{
            //    // Could also delete object
            //    floatingJunk[junkIndex].gameObject.SetActive(false);

            //    // Add the new piece of junk to the array
            //    floatingJunk[junkIndex] = other.gameObject;

            //}

            junkHeightArray[junkArrayIndex] = other.transform.position.y; // starts at where it is
            junktranslateDirectionArray[junkArrayIndex] = 1; // Up by default
            
            if (junkArrayIndex >= 9)
            {
                junkArrayIndex = 0;
            }
            else
            {
                junkArrayIndex++;
            }
            

        
            //other.gameObject.SetActive(false);
        }
    }

    private void rotateJunk()
    {
        int junkPieceIndex = 0; 
        foreach (Transform child in orbitingJunk.transform)
        {
            //child is your child transform
            if (child.gameObject.tag == "Building")
            {
                // Rotates Child Junk Piece around Cyclone
                child.Rotate(new Vector3(0.5f, 0.2f, 0.2f));
                child.RotateAround(transform.position, new Vector3(0, 1, 0), junkRotationSpeed*Time.deltaTime);

                // Translate the Junk Piece in a sin/oscillation up and down

                // Is the junk already above the maximum? 
                if(junkHeightArray[junkPieceIndex] >= junkHeightMax)
                {
                    // junk go down
                    junktranslateDirectionArray[junkPieceIndex] = -1; 
                }
                else if (junkHeightArray[junkPieceIndex] <= junkHeightMin)
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

                // next junk piece in array please
                junkPieceIndex++;
            }
        }
    }
}
