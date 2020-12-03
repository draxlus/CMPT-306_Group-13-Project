using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float strength;
    public float knockTime;
    public float damage;

    //On attack test if the object being hit is an hit if it is a call is made to 
    //the enemies Knockable script to apply knockback to the hit
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Swamp")){
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null){
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * strength;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("Enemy")){
                    other.GetComponent<Enemy>().hitCount++;
                    if(other.GetComponent<Enemy>().hitCount == 1){
                        hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                        other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                    }

                }
                if (other.gameObject.CompareTag("Player")){
                    other.GetComponent<PlayerMovement>().hitCount++;
                    if(other.GetComponent<PlayerMovement>().hitCount == 1){
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                        
                }
                if (other.gameObject.CompareTag("Swamp"))
                {
                    other.GetComponent<swamppoint>().hitCount++;
                    if (other.GetComponent<swamppoint>().hitCount == 1)
                    {
                        hit.GetComponent<swamppoint>().currentState = PlayerState.stagger;
                        other.GetComponent<swamppoint>().Knock(knockTime, damage);
                    }

                }

            }
        }
          
    }

    //Sets the Enemies hitcount to 0
    private void OnTriggerExit2D(Collider2D other){
        other.GetComponent<Enemy>().hitCount--;
        if(other.GetComponent<Enemy>().hitCount < 0){
            other.GetComponent<Enemy>().hitCount = 0;
        }
    }

   
}



