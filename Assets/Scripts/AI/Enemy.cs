using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemy_name;
    public float moveSpeed;

    public int hitCount = 0;

    [SerializeField] private ObjectHealthBar healthBar;

    private void Awake(){
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage){

        health -= damage;
        healthBar.SetSize(health - (damage/maxHealth.initialValue));
        if (health <= 0){
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D myRigidBody, float knocktime, float damage){
        if (damage > 0){
            StartCoroutine(KnockCo(myRigidBody,knocktime));
            TakeDamage(damage);
        }
        else{
            StartCoroutine(KnockCo(myRigidBody, knocktime));
        }
    }
    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knocktime){

        if (myRigidBody != null){
            yield return new WaitForSeconds(knocktime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}