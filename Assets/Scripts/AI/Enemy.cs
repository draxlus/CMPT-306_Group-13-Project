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
    public int health;
    public string enemy_name;
    public int baseAttack;
    public float moveSpeed;

    public int hitCount = 0;

    public void Knock(Rigidbody2D myRigidBody, float knocktime){
        StartCoroutine(KnockCo(myRigidBody,knocktime));
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