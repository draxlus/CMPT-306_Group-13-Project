using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;


    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            //stop the game
            Time.timeScale = 0;
        }

    }

}
