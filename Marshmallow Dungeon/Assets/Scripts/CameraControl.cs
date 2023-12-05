using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Liebert, Jasper
// 11/30/2023
// Manages camera movement and rotations



public class CameraControl : MonoBehaviour
{
    public GameObject playerPos;
    public Vector3 offset;
    public Rigidbody rigidbodyCam;

    void Start()
    {
        Offset(offset);
        rigidbodyCam = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    /// <summary>
    /// Offset camera by a vector3, only to be used when rotation is 0,0,0
    /// </summary>
    /// <param name="offset"> the offset vector3 </param>
    public void Offset(Vector3 offset)
    {
        transform.position = playerPos.transform.position + offset;
    }

    /// <summary>
    /// Add velocity to move the camera forwards with the player
    /// </summary>
    /// <param name="direction"> transform.forward or -transform.forward</param>
    /// <param name="speed"> how fast it moves, same as player </param>
    public void Moving(Vector3 change)
    {
        //Debug.Log(change);
        transform.position += change;
        //rigidbodyCam.velocity = direction * speed;
    }

    public void Rotating(float degree)
    {
        transform.RotateAround(playerPos.transform.position, new Vector3(0, 1, 0), degree);
    }
}
