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

    //Important Variables
    public float speed;
    public Rigidbody rigidbodyRef;
    public GameObject mainCam;


    void Start()
    {
        rigidbodyRef = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //Variables
        float temp = 9 * Time.deltaTime * speed;
        rigidbodyRef.velocity = new Vector3(0, 0, 0);
        Vector3 startPos = transform.position;
        Vector3 change;

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
            rigidbodyRef.velocity = transform.forward * speed;
            change = transform.position;
            Debug.Log("startpos" + startPos + "change" + change);
            mainCam.GetComponent<CameraControl>().Moving(startPos - change);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbodyRef.velocity = -transform.forward * speed;
            change = transform.position;
            mainCam.GetComponent<CameraControl>().Moving(startPos - change);
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);

        }    
    }

}
