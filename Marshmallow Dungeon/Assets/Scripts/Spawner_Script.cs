using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//12/08/2023
//This script breaksdown the connection within the spawner to the 


public class Spawner_Script : MonoBehaviour
{
    //Variables
    public bool goingLeft;

    public GameObject projectilePrefab;
    public float timeBetweenShots;
    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        new WaitForSeconds(startDelay);
        InvokeRepeating("SpawnProjectile", startDelay, timeBetweenShots);
    }

    /// <summary>
    /// Spawns Lazer projectile
    /// </summary>
    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        projectile.GetComponent<Lazer_Script>().goingLeft = goingLeft;

    }

}
