using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;


    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Hits"))
        {
            health -= 1;
            Debug.Log("Colliding");

        }
    }



    // Update is called once per frame
    void Update()
    {
        if(gameObject!= null)
        {
            if (health <= 0)
            {
                kill();
            }
            
        }
    }

    private void kill()
    {
        if(gameObject!= null)
        {
            print("killed enemy");
            Destroy(gameObject);
            //Reduce the number of enemies
            MobGenerationManager.currentEnemies -= 1;
        }
    }
}
