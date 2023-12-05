using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//12/05/2023
//this script shows on how the 

public class Lava : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;
    private Vector3 leftPos;
    private Vector3 rightPos;
    public int speed;
    public bool goingLeft;

    // Start is called before the first frame update
    void Start()
    {
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (goingLeft)
        {
            if (transform.position.y <= leftPos.y)
            {
                goingLeft = false;
            }
            else
            {
                transform.position += Vector3.down * Time.deltaTime * speed;
            }
        }
        else
        {
            if (transform.position.y >= rightPos.y)
            {
                goingLeft = true;
            }
            else
            {
                transform.position += Vector3.up * Time.deltaTime * speed;
            }
        }
    }
}
