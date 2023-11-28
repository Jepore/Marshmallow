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
        float temp = 0;
        rigidbodyRef.velocity = new Vector3(0,0,0);
        //Movement
        if (Input.GetKey(KeyCode.A))
        {
            temp = -9 * Time.deltaTime * speed;
            transform.Rotate(new Vector3(0, temp, 0), Space.World);
            mainCam.GetComponent<CameraControl>().Rotating(temp);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 9, 0) * Time.deltaTime * speed, Space.World);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rigidbodyRef.velocity = transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbodyRef.velocity = -transform.forward * speed;

        }
    }

    /*public class Example : MonoBehaviour
    {
        public float angleBetween = 0.0f;
        public Transform target;

        void Update()
        {
            Vector3 targetDir = target.position - transform.position;
            angleBetween = Vector3.Angle(transform.forward, targetDir);
        }
    }*/
}
