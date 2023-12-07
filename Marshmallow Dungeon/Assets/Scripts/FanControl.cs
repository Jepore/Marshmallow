using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//12/06/2023
//Spins the fan

public class FanControl : MonoBehaviour
{
    //Variables
    public float rotationSpeed = 50;
    public bool broken = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spinning();
    }


    /// <summary>
    /// spins the fan when not broken
    /// </summary>
    private void Spinning()
    {
        if (!broken)
        {
            transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0, 0), Space.World);
        }
    }
}
