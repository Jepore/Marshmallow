using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//12/06/2023
//Adds Velocity to bullet and manages collisions with whatever it hits


public class BulletControl : MonoBehaviour
{
    //Variables
    public float bulletSpeed = 13;
    public Rigidbody bulletRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize rigidbody and add velocity
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.up * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fan Enemy")
        {
            collision.gameObject.SetActive(false);
        }

        //So the bullet doesn't collide with the player accidentally
        if (collision.gameObject.tag != "Player")
        {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Manages collisions with the sword item
    /// </summary>
    /// <param name="other"> GameObject that was collided with </param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Small Enemy")
        {
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Fan Enemy")
        {
            other.gameObject.SetActive(false);
        }


    }

}
