using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Logboi" || collision.gameObject.name == "Enemy")
        {
            //Kill the enemy
            Destroy(collision.gameObject);
        }
    }

}
