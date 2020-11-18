using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerationManager : MonoBehaviour
{
    public int worldWidth;
    public int worldHeight;
    public GameObject enemy;
    public int enemiesPerRound;
    public int maxEnemies;
    private float timeRemaining; //Time before another group of enemies is spawned
    public static int currentEnemies; //Number of enemies on screen

    private void Start()
    {
        timeRemaining = 1;
        currentEnemies = 0;
        createEnemies(enemiesPerRound);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            if(currentEnemies < maxEnemies)
            {
                createEnemies(enemiesPerRound);
            }
            //reset the timer
            timeRemaining = 1;
        }
        
    }

    private void createEnemies(int num)
    {
        while(num > 0)
        {
            if(currentEnemies < maxEnemies)
            {
                Instantiate(enemy, new Vector3(Random.Range(-worldWidth + 1, worldWidth - 1), Random.Range(-worldHeight, worldHeight), -0.1f), Quaternion.identity);
                currentEnemies++;
            }
            num--;
        }
        
    }

}
