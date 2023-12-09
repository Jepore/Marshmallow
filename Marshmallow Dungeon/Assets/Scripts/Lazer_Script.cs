using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//12/08/2023
//This script breakdowns the lazer being pointed from the enemy
public class Lazer_Script : MonoBehaviour
{
    public float speed;
    public bool goingLeft;

    // Update is called once per frame
    void Update()
    {
        if(goingLeft == true)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }
        else
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }
    }
}
