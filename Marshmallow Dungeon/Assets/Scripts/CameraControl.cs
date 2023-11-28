using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject playerPos;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.transform.position+offset;

    }

    public void Rotating(float degree)
    {
        transform.RotateAround(playerPos.transform.position, new Vector3(0,1,0), degree);
    }
}
