using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Rigidbody rb;

    public float knockTime;

    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        knockTime = 0.2f;
        damage = 1;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D other = collision.GetComponent<Rigidbody2D>();
            other.GetComponent<Enemy>().currentState = EnemyState.stagger;
            collision.GetComponent<Enemy>().Knock(other, knockTime, damage);
            
        }
    }


    private void OnBecameInvisible()
    {
        print("destroying bullet");
        Destroy(gameObject);
    }

}
