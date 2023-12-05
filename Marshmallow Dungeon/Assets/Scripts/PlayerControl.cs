using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//11/28/2023
//This script will manage player movement and collisions

public class PlayerControl : MonoBehaviour
{
    //Variables:
    public int coins = 0;
    public int hp = 50;
    public bool isGrounded = true;
    public float jumpForce = 10;
    public float speed = 7;


    //Important Variables
    public Rigidbody rigidbodyRef;
    public GameObject mainCam;
    private Vector3 startPos;
    private Vector3 change;

    void Start()
    {
        rigidbodyRef = GetComponent<Rigidbody>();
        Vector3 startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.3f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


    private void Movement()
    {
        //Variables
        float temp = 9 * Time.deltaTime * speed;
        Vector3 change = transform.position;


        IsGrounded();
        if (isGrounded)
        {
            rigidbodyRef.velocity = Vector3.zero;
        }

        //jumps when space is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            HandleJump();
        }


        //Rotations for both the player and the camera when pressing A or D (Need to add Time.deltaTime?)
        if (Input.GetKey(KeyCode.A))
        {
            temp = 9 * Time.deltaTime * speed;
            transform.Rotate(new Vector3(0, -temp, 0), Space.World);
            mainCam.GetComponent<CameraControl>().Rotating(-temp);

        }
        if (Input.GetKey(KeyCode.D))
        {
            temp = 9 * Time.deltaTime * speed;
            transform.Rotate(new Vector3(0, temp, 0), Space.World);
            mainCam.GetComponent<CameraControl>().Rotating(temp);
        }

        //Adds velocity to both the player and the camera when moving
        if (Input.GetKey(KeyCode.W))
        {
            rigidbodyRef.velocity += transform.forward * speed;
            //change = transform.position;
            //Debug.Log("startpos" + startPos + "change" + change);
            //mainCam.GetComponent<CameraControl>().Moving(startPos - change);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbodyRef.velocity += -transform.forward * speed;
            //change = transform.position;
            //mainCam.GetComponent<CameraControl>().Moving(startPos - change);
        }

        //Adjusts Camera position when player moves forwards/backwards
        if (startPos != change)
        {
            Vector3 cameraCorrect = change - startPos;
            mainCam.GetComponent<CameraControl>().Moving(cameraCorrect);
            startPos = change;
        }


    }


    /// <summary>
    /// if player is on the ground, add upwards velocity
    /// </summary>
    private void HandleJump()
    {
        if (isGrounded)
        {
            rigidbodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


    /// <summary>
    /// manages collisions
    /// </summary>
    /// <param name="other"> trigger that player collided with </param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);

        }    
    }

}
