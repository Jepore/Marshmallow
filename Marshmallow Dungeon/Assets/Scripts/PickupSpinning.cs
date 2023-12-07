using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//12/06/2023
//rotates pickups

public class PickupSpinning : MonoBehaviour
{

    //Variables
    public float rotSpeed = 90;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0), Space.World);
    }
}
