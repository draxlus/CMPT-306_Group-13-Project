using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    public static float timer;
    public static int worldWidth, worldHeight;
    public static float maxTime;
    public static int currentLoot;
    public int maxLoot;
    public GameObject loot;


    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if(currentLoot < maxLoot)
            {
                dropLoot();
            }
            //reset the timer
            timer = maxTime;
        }
    }

    private void dropLoot()
    {
        Instantiate(loot, new Vector3(Random.Range(-worldWidth + 1, worldWidth - 1), Random.Range(-worldHeight, worldHeight), -0.1f), Quaternion.identity);
        currentLoot++;
    }
}
