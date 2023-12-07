using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Liebert, Jasper
//12/06/2023
//Manages collisions with sword

public class SwordControl : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {  

    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// Manages collisions with the sword item
    /// </summary>
    /// <param name="other"> GameObject that was collided with </param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
        }
    }
}
