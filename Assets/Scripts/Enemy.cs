using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "player")
        {
            health -= 1;
            print("Colliding");

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
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position += new Vector3(0, 0.1f, 0);

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position += new Vector3(0, -0.1f, 0);

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
