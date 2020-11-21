using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemyAI : Enemy
{
    //Transform of the target
    public Transform target;
    //Range at which Logboi will start chasing the player
    public float chaseRadius;
    //Range at which Logboi will attack
    public float attackRadius;

    private Rigidbody2D rb;

    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        //Finds the target
        target = GameObject.FindWithTag("Player").transform;
        currentState = EnemyState.idle;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    //Checks how far away the enemy is from the Player if the player is within chase range the enemy will chase him, if not it will remain still
    void CheckDistance(){
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius){
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger){
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                rb.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
        else{
            ChangeState(EnemyState.idle);
        }
    }

    //Change the state of the enemy
    private void ChangeState(EnemyState newState){
        if(currentState != newState){
            currentState = newState;
        }
    }
}
