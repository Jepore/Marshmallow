using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//11/28/2023
//This script will manage player movement and collisions

public class PlayerControl : MonoBehaviour
{
    //Variables:
    public float coins = 0;
    public float hp = 50;
    public bool isGrounded = true;
    public float jumpForce = 10;
    public float speed = 7;
    public float turnSpeed = 50;
    //GetChild(i) items
    //0 - empty
    //1 - Sword
    //2 - Gun
    //3 - Shield
    public int item = 0;


    //Important Variables
    public Rigidbody rigidbodyRef;
    public CameraControl mainCam;
    public Vector3 spawnPoint;
    private Vector3 startPos;
    private Vector3 change;


    void Start()
    {
        spawnPoint = transform.position;
        rigidbodyRef = GetComponent<Rigidbody>();
        Vector3 startPos = transform.position;
        ItemManager();

    }

    // Update is called once per frame
    void Update()
    {
        //jumps when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }
        Dead();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// Checks if player is on the ground
    /// </summary>
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


    /// <summary>
    /// manages player movement, forwards/backwards/rotation/cameracontrol
    /// </summary>
    private void Movement()
    {
        //Variables
        float temp = turnSpeed * Time.deltaTime;
        Vector3 change = transform.position;

        //Checks if player is on the ground, stops x and z movement when grounded
        IsGrounded();
        if (isGrounded)
        {
            rigidbodyRef.velocity = new Vector3(0, rigidbodyRef.velocity.y, 0);
        }

        //Rotations for both the player and the camera when pressing A or D (Need to add Time.deltaTime?)
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -temp, 0), Space.World);
            mainCam.Rotating(-temp);

            /*if (Input.GetKey(KeyCode.W))
            {
                transform.Rotate(new Vector3(0, -temp, 0), Space.World);
                mainCam.Rotating(-temp);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Rotate(new Vector3(0, temp, 0), Space.World);
                mainCam.Rotating(temp);
            }
            else
            {
                transform.Rotate(new Vector3(0, -temp, 0), Space.World);
                mainCam.Rotating(-temp);
            }*/
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, temp, 0), Space.World);
            mainCam.Rotating(temp);

            /*if (Input.GetKey(KeyCode.W))
            {
                transform.Rotate(new Vector3(0, temp, 0), Space.World);
                mainCam.Rotating(temp);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Rotate(new Vector3(0, -temp, 0), Space.World);
                mainCam.Rotating(-temp);
            }
            else
            {
                transform.Rotate(new Vector3(0, temp, 0), Space.World);
                mainCam.Rotating(temp);
            }*/

        }

        //Adds velocity to both the player and the camera when moving
        if (Input.GetKey(KeyCode.W))
        {
            rigidbodyRef.velocity = new Vector3(transform.forward.x*speed, rigidbodyRef.velocity.y, transform.forward.z* speed);
            //change = transform.position;
            //Debug.Log("startpos" + startPos + "change" + change);
            //mainCam.GetComponent<CameraControl>().Moving(startPos - change);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbodyRef.velocity = new Vector3(-transform.forward.x*speed, rigidbodyRef.velocity.y, -transform.forward.z*speed);
            //rigidbodyRef.velocity = -transform.forward * speed;
            //change = transform.position;
            //mainCam.GetComponent<CameraControl>().Moving(startPos - change);
        }

        //Adjusts Camera position when player moves forwards/backwards
        if (startPos != change)
        {
            Vector3 cameraCorrect = change - startPos;
            mainCam.Moving(cameraCorrect);
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
    /// Checks when player should die, and acts accordingly
    /// </summary>
    private void Dead()
    {
        if (transform.position.y <= -15)
        {
            mainCam.Offset();
            transform.position = spawnPoint;
        }
    }

    private void ItemManager()
    {
        //GetChild(i) items
        //0 - empty
        //1 - Sword
        //2 - Gun
        //3 - Shield

        //empty
        if (item == 0)
        {
            Debug.Log("Yuh");
            //hides any visible items
            for (int i = 0; i < 4; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        //sword
        if (item == 1)
        {
            //hides any visible items
            for (int i = 0; i < 4; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(1).gameObject.SetActive(true);
        }

        //gun
        if (item == 2)
        {
            //hides any visible items
            for (int i = 0; i < 4; i++)
            {

                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(2).gameObject.SetActive(true);

        }

        //shield
        if (item == 3)
        {
            //hides any visible items
            for (int i = 0; i < 4; i++)
            {

                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(3).gameObject.SetActive(true);

        }
    }

    /// <summary>
    /// manages collisions
    /// </summary>
    /// <param name="other"> trigger that player collided with </param>
    private void OnTriggerEnter(Collider other)
    {
        //coins add points
        if (other.gameObject.tag == "Coin")
        {
            coins++;
            other.gameObject.SetActive(false);
        }

        //Sets new spawnpoint and teleports player
        if (other.gameObject.tag == "Portal")
        {
            Portal tempPortal = other.gameObject.GetComponent<Portal>();
            transform.position = tempPortal.tempPortalLocation.transform.position;
            spawnPoint = transform.position;
        }

        //sword pickup
        if (other.gameObject.tag == "Sword Pickup")
        {
            other.gameObject.SetActive(false);
            item = 1;
            ItemManager();
        }

        //gun pickup
        if (other.gameObject.tag == "Gun Pickup")
        {
            other.gameObject.SetActive(false);
            item = 2;
            ItemManager();
        }

        //shield pickup
        if (other.gameObject.tag == "Shield Pickup")
        {
            other.gameObject.SetActive(false);
            item = 3;
            ItemManager();
        }
    }


}
