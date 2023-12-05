using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanControl : MonoBehaviour
{
    public float rotationSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spinning();
    }

    private void Spinning()
    {
        transform.Rotate(new Vector3(rotationSpeed*Time.deltaTime, 0, 0), Space.World);
    }
}
