using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    public Vector3 startPos;
    public Quaternion startRot;
    public Rigidbody swordRigidbody;
    public float swordSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {  
        startPos = transform.position;
        startRot = transform.rotation;
        swordRigidbody = GetComponent<Rigidbody>();

        //Swing();
    }

    // Update is called once per frame
    void Update()
    {
        //swordRigidbody.velocity = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.C))
        {
            Swing();
        }
    }

    private void Swing()
    {
        for (int i = 0; i < 9; i++)
        {
            transform.Rotate(new Vector3(5, 0, 0), Space.World);
        }
        swordRigidbody.velocity = transform.up * swordSpeed;
        
    }
}
