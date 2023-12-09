using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 12/05/2023
//this script shows how the enemy will move back and forth.

public class BackAndForth : MonoBehaviour
{
    //Variables
    public GameObject leftPoint;
    public GameObject rightPoint;
    private Vector3 leftPos;
    private Vector3 rightPos;
    public int speed;
    public bool goingLeft;
    public bool xAxis;


    // Start is called before the first frame update
    void Start()
    {
        //initialize
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //this will make the enemy move left or right 
    private void Move()
    {
        if (xAxis)
        {
            if (goingLeft)
            {
                if (transform.position.x <= leftPos.x)
                {
                    goingLeft = false;
                }
                else
                {
                    transform.position += Vector3.left * Time.deltaTime * speed;
                }
            }
            else
            {
                if (transform.position.x >= rightPos.x)
                {
                    goingLeft = true;
                }
                else
                {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                }
            }
        }
        else
        {
            if (goingLeft)
            {
                if (transform.position.z <= leftPos.z)
                {
                    goingLeft = false;
                }
                else
                {
                    transform.position += Vector3.back * Time.deltaTime * speed;
                }
            }
            else
            {
                if (transform.position.z >= rightPos.z)
                {
                    goingLeft = true;
                }
                else
                {
                    transform.position += Vector3.forward * Time.deltaTime * speed;
                }
            }
        }
    }

}
