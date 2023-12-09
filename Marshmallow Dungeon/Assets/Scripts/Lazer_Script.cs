using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//12/08/2023
//This script breakdowns the lazer being pointed from the enemy

public class Lazer_Script : MonoBehaviour
{
    public float speed;
    public bool goingLeft;
    public Vector3 startPos;
    public float distance;

    void Start()
    {
        //initialize
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //makes lazer move
        if(goingLeft == true)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
            if (transform.position.x <= startPos.x - distance)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
            if (transform.position.x >= startPos.x + distance)
            {
                Destroy(this.gameObject);
            }
        }


    }

    /// <summary>
    /// when lazer collides, set lazer inactive
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
    }
}
