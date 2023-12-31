using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Liebert, Jasper
//11/28/2023
//This script will manage player movement, attacks, collisions, triggers, damage, deaths, items, and sometimes scene switching

public class PlayerControl : MonoBehaviour
{
    //Temp variables
    public float temp = 0;
    public float tempVel = 0;

    //Variables:
    static public float coins = 0;
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
    public bool swinging;
    public bool damaged; //for stopping the invulnerable player model from appearing when you get hit
    public bool dangerous;
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
    public UIManger uiManager;
    public ShopControl shopControl;
    private Vector3 startPos;
    private Vector3 change;


    void Start()
    {
        //initialize
        transform.GetChild(6).gameObject.SetActive(false);
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

        //checks if attack button is pressed
        Attacking();

        //checks if player is invulnerable
        Armored();

        Dead();
    }

    /// <summary>
    /// Updates Movement with FixedUpdate rather than Update
    /// </summary>
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

    }

    /// <summary>
    /// Switches item being held to int "item" value and starts the coroutine that will disable them
    /// </summary>
    public void ItemManager()
    {
        //GetChild(i) items
        //0 - empty
        //1 - Sword
        //2 - Gun
        //3 - Shield

        //just in case :)
        invulnerable = false;
        dangerous = false;

        //hides any visible items
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        //sword
        if (item == 1)
        {
            uiManager.itemTemp = 30;
            uiManager.itemTimer = 30;
            StartCoroutine(ItemPickup(1, 30));
        }

        //gun
        if (item == 2)
        {
            uiManager.itemTemp = 20;
            uiManager.itemTimer = 20;
            StartCoroutine(ItemPickup(2, 20));
        }


        //shield
        if (item == 3)
        {
            invulnerable = true;
            dangerous = true;
            uiManager.itemTemp = 20;
            uiManager.itemTimer = 20;
            StartCoroutine(ItemPickup(3, 20));
        }
    }

    /// <summary>
    /// manages player movement, forwards/backwards/rotation
    /// </summary>
    private void Movement()
    {
        //Variables
        float turning = turnSpeed * Time.deltaTime;
        float speedCap = 28f;


        //Rotations for both the player and the camera when pressing A or D (Need to add Time.deltaTime?)
        if (Input.GetKey(KeyCode.A))
        {
            temp -= turning;
        }
        if (Input.GetKey(KeyCode.D))
        {
            temp += turning;
        }

        //Adds velocity to both the player and the camera when moving
        if (Input.GetKey(KeyCode.W))
        {
            tempVel += speed;
            //rigidbodyRef.velocity = new Vector3(transform.forward.x * speed, rigidbodyRef.velocity.y, transform.forward.z* speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            tempVel -= speed;
            //rigidbodyRef.velocity = new Vector3(-transform.forward.x * speed, rigidbodyRef.velocity.y, -transform.forward.z*speed);
        }

        //Rotating only the y of the player and the camera (since it is childed)
        if (temp > speedCap)
        {
            temp = speedCap;
        }
        else if (temp < -speedCap)
        {
            temp = -speedCap;
        }
        else if (temp < 1f && temp > -1f)
        {
            temp = 0;
        }

        temp = temp / 1.44f;

        if (!isGrounded)
        {
            temp = temp / 1.44f;
        }

        //Velocity
        if (tempVel > 18)
        {
            tempVel = 18;
        }
        else if (temp < -18)
        {
            tempVel = -18;
        }
        else if (tempVel < 0.2f && tempVel > -0.2f)
        {
            tempVel = 0;
        }

        tempVel = tempVel / 1.44f;


        rotation += temp;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation,transform.eulerAngles.z);

        rigidbodyRef.velocity = new Vector3(transform.forward.x * tempVel, rigidbodyRef.velocity.y, transform.forward.z * tempVel);

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
    /// if player presses F, use item
    /// </summary>
    private void Attacking()
    {
        //If player hits "C", swing sword and start a cooldown
        if (Input.GetKeyDown(KeyCode.F) && !cooling && item == 1)
        {
            StartCoroutine(Cooldown(1.5f));
            StartCoroutine("Swing");
        }
        //If player hits "C", shoot gun
        if (Input.GetKeyDown(KeyCode.F) && !cooling && item == 2)
        {
            StartCoroutine(Cooldown(0.2f));
            Instantiate(bullet, transform.GetChild(2).transform.position, Quaternion.Euler(90, rotation, 0));
        }
    }

    /// <summary>
    /// Checks when player should die, and acts accordingly
    /// </summary>
    private void Dead()
    {
        if (transform.position.y <= -15)
        {
            coins = 0;
            Respawn();
        }
        if (hp <= 0)
        {
            coins = 0;
            Respawn();
        }
    }

    /// <summary>
    /// Respawns player, resets position, rotation and health
    /// </summary>
    private void Respawn()
    {
        transform.position = spawnPoint;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
        rotation = 0;
        hp = 50;
    }

    /// <summary>
    /// damages player and blinks their character model red
    /// </summary>
    /// <param name="hpLost"> how much hp is lost </param>
    private void Damaged(int hpLost)
    {
        damaged = true;
        hp -= hpLost;
        StartCoroutine(Invulnerable(0.1f));
        StartCoroutine("Blink");
    }

    /// <summary>
    /// changes player material to metal so you can see that you're armored
    /// </summary>
    private void Armored()
    {
        if (invulnerable && !damaged)
        {
            transform.GetChild(7).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(7).gameObject.SetActive(false);

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

        //if it hits the marshmallow princess, end the game
        if (other.gameObject.tag == "Princess")
        {
            Debug.Log("doing the thing");
            //switches to game over screen
            SceneManager.LoadScene(2);
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
        if (other.gameObject.tag == "Small Enemy")
        {
            if (!dangerous)
            {
                if (!invulnerable)
                {
                    Damaged(15);
                }
            }
            else
            {
                other.gameObject.SetActive(false);
            }
        }

        //spawner
        if (other.gameObject.tag == "Spawner")
        {
            if (!dangerous)
            {
                if (!invulnerable)
                {
                    Damaged(10);
                }
            }
            else
            {
                other.GetComponent<Spawner_Script>().CancelInvoke();
                other.gameObject.SetActive(false);
            }

        }

        //small enemy
        if (other.gameObject.tag == "Spikes" && !invulnerable)
        {
            Damaged(10);
        }

        //Lazer
        if (other.gameObject.tag == "Lazer" && !invulnerable)
        {
            Damaged(15);
        }

        //Shopping zone
        if (other.gameObject.tag == "Shop" && shoppable)
        {
            shopping = true;
            shopControl.Activate();
        }
    }

    /// <summary>
    /// Manages Collisions
    /// </summary>
    /// <param name="collision">mostly fans</param>
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Fan Enemy")
        {
            if (!dangerous)
            {
                if (!invulnerable)
                {
                    Damaged(15);
                    collision.gameObject.SetActive(false);
                }
            }
            else
            {
                collision.gameObject.SetActive(false);
            }
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
        dangerous = true;
        invulnerable = true;
            yield return new WaitForSeconds(1);
        invulnerable = false;
        if (item < 2 && item != 0)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
        }

        dangerous = false;
        Debug.Log("swung");
    }

    /// <summary>
    /// Cooldown for attacks and items
    /// </summary>
    /// <param name="seconds"> how long </param>
    /// <returns></returns>
    IEnumerator Cooldown(float seconds)
    {
        cooling = true;
        yield return new WaitForSeconds(seconds);
        cooling = false;
    }

    /// <summary>
    /// makes player invulnerable for a specific amount of time
    /// </summary>
    /// <param name="seconds"> the specific amount of time </param>
    /// <returns></returns>
    IEnumerator Invulnerable(float seconds)
    {
        invulnerable = true;
        yield return new WaitForSeconds(seconds);
        invulnerable = false;
    }

    /// <summary>
    /// stops player from getting into an infinite shopping loop
    /// </summary>
    public void ShopCooldown()
    {
        StartCoroutine("LeaveShop");
    }

    /// <summary>
    /// used in "ShopCooldown()"
    /// </summary>
    /// <returns></returns>
    IEnumerator LeaveShop()
    {
        shoppable = false;
        yield return new WaitForSeconds(1);
        shoppable = true;
    }

    /// <summary>
    /// blinks player red when they take damage
    /// </summary>
    /// <returns></returns>
    IEnumerator Blink()
    {
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(true);
        damaged = false;
    }

    /// <summary>
    /// sets item active, disables it after time and sets item back to 0
    /// </summary>
    /// <param name="item"> index of the item you are equipping</param>
    /// <param name="time"> seconds </param>
    /// <returns></returns>
    IEnumerator ItemPickup(int itemPicked, float time)
    {
        
        transform.GetChild(itemPicked).gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        if (item == itemPicked)
        {
            item = 0;
            ItemManager();
        }

        Debug.Log("Checking");
    }
}
