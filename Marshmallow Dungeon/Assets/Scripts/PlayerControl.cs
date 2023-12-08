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
    public float turnSpeed = 110;
    public float rotation = 0;
    public bool cooling = false; //for cooldowns
    public bool invulnerable = false;
    public float lives = 3;
    public bool shopping = false;
    public bool shoppable = true; //so you don't get locked in an infinite shop loop
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
    public GameObject bullet;
    public ShopControl shopControl;
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
        IsGrounded();
        //jumps when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }

        //If player hits "C", swing sword and start a cooldown
        if (Input.GetKeyDown(KeyCode.C) && !cooling && item == 1)
        {
            StartCoroutine(Cooldown(1.5f));
            StartCoroutine("Swing");
        }
        //If player hits "C", shoot gun
        if (Input.GetKeyDown(KeyCode.C) && !cooling && item == 2)
        {
            StartCoroutine(Cooldown(0.5f));
            Instantiate(bullet, transform.GetChild(2).transform.position, Quaternion.Euler(90, rotation, 0));
        }



        Dead();
    }

    
    private void FixedUpdate()
    {
        if (!shopping)
        {
            Movement();
        }
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

        //Checks if player is on the ground, stops x and z movement when grounded
        if (isGrounded)
        {
            rigidbodyRef.velocity = new Vector3(0, rigidbodyRef.velocity.y, 0);
        }
        else
        {
            while(!isGrounded)
            {
                speed = speed / 2;
            }
            speed = speed * 2;
        }
    }

    public void ItemManager()
    {
        //GetChild(i) items
        //0 - empty
        //1 - Sword
        //2 - Gun
        //3 - Shield

        //empty
        if (item == 0)
        {
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
    /// manages player movement, forwards/backwards/rotation
    /// </summary>
    private void Movement()
    {
        //Variables
        float temp = turnSpeed * Time.deltaTime;

        //Rotations for both the player and the camera when pressing A or D (Need to add Time.deltaTime?)
        if (Input.GetKey(KeyCode.A))
        {
            rotation -= temp;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotation += temp;
        }

        //Adds velocity to both the player and the camera when moving
        if (Input.GetKey(KeyCode.W))
        {
            rigidbodyRef.velocity = new Vector3(transform.forward.x * speed, rigidbodyRef.velocity.y, transform.forward.z* speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbodyRef.velocity = new Vector3(-transform.forward.x * speed, rigidbodyRef.velocity.y, -transform.forward.z*speed);
        }

        //Rotating only the y of the player and the camera (since it is childed)
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation,transform.eulerAngles.z);


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
            Respawn();
        }
        if (hp <= 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = spawnPoint;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
        rotation = 0;
        hp = 50;
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

        //small enemy
        if (other.gameObject.tag == "Small Enemy" && !invulnerable)
        {
            hp -= 15;
            StartCoroutine(Invulnerable(0.1f));
        }

        //Shopping zone
        if (other.gameObject.tag == "Shop" && shoppable)
        {
            shopping = true;
            shopControl.Activate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Fan Enemy" && !invulnerable)
        {
            hp -= 15;
            StartCoroutine(Invulnerable(0.1f));
            collision.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// Swings Sword by setting the 2nd sword that is slightly further forwards to active
    /// </summary>
    /// <returns></returns>
    IEnumerator Swing()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        invulnerable = true;
            yield return new WaitForSeconds(1);
        invulnerable = false;
        if (item < 2)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
        }


        Debug.Log("swung");
    }

    IEnumerator Cooldown(float seconds)
    {
        cooling = true;
        yield return new WaitForSeconds(seconds);
        cooling = false;
    }

    IEnumerator Invulnerable(float seconds)
    {
        invulnerable = true;
        yield return new WaitForSeconds(seconds);
        invulnerable = false;
    }

    public void ShopCooldown()
    {
        StartCoroutine("LeaveShop");
    }

    IEnumerator LeaveShop()
    {
        shoppable = false;
        yield return new WaitForSeconds(1);
        shoppable = true;
    }
}
