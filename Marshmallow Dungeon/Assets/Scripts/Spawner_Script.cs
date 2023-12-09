using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//12/08/2023
//This script breaksdown the connection within the spawner to the 


public class Spawner_Script : MonoBehaviour
{
    public bool goingLeft;

    public GameObject projectilePrefab;
    public float timeBetweenShots;
    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnProjectile", startDelay, timeBetweenShots);
    }


    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        if (projectile.GetComponent<Lazer_Script>())
        {
            projectile.GetComponent<Lazer_Script>().goingLeft = goingLeft;
        }
    }
    
}
