using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 6f;            // The speed that the player will move at.
    public int junkRotationSpeed = 500;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    GameObject[] floatingJunk;
    int junkIndex;
    

    private void Start()
    {
        floatingJunk = new GameObject[10];
        junkIndex = 0; 
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
            other.transform.parent = transform;
            //transform.parent = other.transform;

            if(floatingJunk[junkIndex] != null)
            {
                // Could also delete object
                floatingJunk[junkIndex].gameObject.SetActive(false);

                // Add the new piece of junk to the array
                floatingJunk[junkIndex] = other.gameObject;
            }

            if (junkIndex >= 9)
            {
                junkIndex = 0;
            }
            else
            {
                junkIndex++;
            }
            

        
            //other.gameObject.SetActive(false);
        }
    }

    private void rotateJunk()
    {
        foreach (Transform child in transform)
        {
            //child is your child transform
            if (child.gameObject.tag == "Building")
            {

                child.RotateAround(transform.position, new Vector3(0, 3, 0), junkRotationSpeed*Time.deltaTime);

                child.Translate(new Vector3(0, 0.1f, 0));
                
            }
        }

        //foreach (GameObject junk in floatingJunk)
        //{
        //    if (junk != null)
        //    {
        //        junk.transform.RotateAround(Vector3.zero, Vector3.up, 20);
        //    }
        //}
    }
}
