using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//11/28/2023
//This script will manage player movement and collisions

public class PlayerControl : MonoBehaviour
{
    //Variables:
    public float speed;
    private Rigidbody rgidbodyRef;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //Movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
        }
    }
}
