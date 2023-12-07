using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    public Vector3 offset;
    public Quaternion startRot;
    public Rigidbody swordRigidbody;
    public float swordSpeed = 10;
    public bool swinging = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {  
        offset = transform.position - player.transform.position;
        startRot = transform.rotation;
        swordRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Swing();
            
        }
    }

    public void Swinging()
    {
        Debug.Log("Pressed");
        for (int i = 0; i < 9; i++)
        {
            transform.Rotate(new Vector3(5, 0, 0), Space.World);
        }
        swordRigidbody.velocity = transform.up * swordSpeed;
    }

    IEnumerator Swing()
    {
        swinging = true;
        Swinging();
        yield return new WaitForSeconds(.3f);
        swinging = false;
    }
}
