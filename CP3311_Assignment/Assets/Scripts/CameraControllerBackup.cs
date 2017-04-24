using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, 10, -20);
        // offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 playerPosition = (player.transform.localPosition - player.transform.forward); 

        transform.position = playerPosition + offset;
        transform.LookAt(player.transform);
        
        //transform.position = Vector3.Lerp(, transform.position, 0.5f);
        //transform.position = player.transform.position + offset;
    }
}
