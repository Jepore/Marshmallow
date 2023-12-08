using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Liebert, Jasper
// 11/30/2023
// Manages camera movement and rotations

public class CameraControl : MonoBehaviour
{
    //Variables
    public GameObject playerPos;
    public Vector3 offset;
    

    void Start()
    {
        //only called here, when player dies, and when player teleports
        ResetCamera();
    }

    void Update()
    {

    }

    /// <summary>
    /// Offset camera by a vector3, only to be used when rotation is 0,0,0
    /// </summary>
    /// <param name="offset"> the offset vector3 </param>
    public void ResetCamera()
    {
        transform.eulerAngles = new Vector3(playerPos.transform.eulerAngles.x + 15, playerPos.transform.eulerAngles.y, playerPos.transform.eulerAngles.z);
        transform.position = playerPos.transform.position + offset;
    }


    //A BUNCH OF BULL
    ///// <summary>
    ///// Add velocity to move the camera forwards with the player
    ///// </summary>
    ///// <param name="direction"> transform.forward or -transform.forward</param>
    ///// <param name="speed"> how fast it moves, same as player </param>
    //public void Moving(Vector3 change)
    //{
    //    transform.position += change;
    //}

    //public void Rotating(float degree)
    //{
    //    degree -= transform.eulerAngles.y;
    //    transform.RotateAround(playerPos.transform.position, new Vector3(0, 1, 0), degree);
    //}
}
