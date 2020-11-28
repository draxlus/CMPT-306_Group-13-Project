using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swamppoint : MonoBehaviour
{
    public int health;
    public int swampnumber;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        swampnumber = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("In the swamppoint script");
        print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Hits") || collision.gameObject.CompareTag("PlayerAttack") || collision.gameObject.CompareTag("Player"))
        {
            health--;
            print("killinng the swamp");
        }
    }
    private void FixedUpdate()
    {
        if (health == 0)
        {
            WinCondition.swampPointsDestroyed++;
            Destroy(gameObject);
        }
    }
}
