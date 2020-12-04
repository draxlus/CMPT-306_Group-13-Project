using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Sheroze Ajmal (Lambda)

//Purpose: Mob generation at the swamp-points

/*Overview of the implementation:
 * Randomly choose a swampoint for the swamppointArray, and instantiate an enemy at that location
 */

public class SwampPointMobGeneration : MonoBehaviour
{
    public GameObject enemy;
    public float generationInterval; //time before another mob generation takes place

    private GameObject[] swampointArray; //stores all swamp-points
    private float timer;


    private void Start()
    {
        timer = generationInterval;
        Invoke("swampInit", 1);
    }
    private void swampInit()
    {
        swampointArray = GameObject.FindGameObjectsWithTag("Swamp");
    }
    

    //Purpose: returns a random swamp-point gameobject
    private GameObject chooseRandomSwamppoint()
    {
        int randInt = Random.Range(0, swampointArray.Length - 1);
        return swampointArray[randInt];
    }

    //Purpose: creates a mob at a random spawn-point after every generationInterval has passed
    private void instantiateMob()
    {
        if(timer <= 0)
        {
            //Create a new mob of enemies
            Instantiate(enemy, chooseRandomSwamppoint().gameObject.transform.position, Quaternion.identity);
            timer = generationInterval;
            print("Creating a new mob");
        }
        else
        {
            //generationInterval has not yet passed
            timer -= Time.deltaTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
        

        instantiateMob();
    }
}
