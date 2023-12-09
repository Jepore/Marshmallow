using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 12/05/2023
//this script shows how the enemy will move back and forth.

public class UpAndDown : MonoBehaviour
{
    public GameObject botPoint;
    public GameObject topPoint;
    private Vector3 botPos;
    private Vector3 topPos;
    public int speed;
    public bool goingDown;


    // Start is called before the first frame update
    void Start()
    {
        botPos = botPoint.transform.position;
        topPos = topPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //this will make the enemy move left or right 
    private void Move()
    {
        if (goingDown)
        {
            if (transform.position.y <= botPos.y)
            {
                goingDown = false;
            }
            else
            {
                transform.position += Vector3.down * Time.deltaTime * speed;
            }
        }
        else
        {
            if (transform.position.y >= topPos.y)
            {
                goingDown = true;
            }
            else
            {
                transform.position += Vector3.up * Time.deltaTime * speed;
            }
        }
    }


}
