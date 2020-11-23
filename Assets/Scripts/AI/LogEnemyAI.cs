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
                changeAnim(temp - transform.position);
                rb.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if(Vector3.Distance(target.position, transform.position) <= chaseRadius){
            anim.SetBool("wakeUp", false);
        }
        else{
            anim.SetBool("wakeUp", false);
            ChangeState(EnemyState.idle);
        }
    }

    private void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void changeAnim(Vector2 direction){
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
            if(direction.x > 0){
                SetAnimFloat(Vector2.right);
            }
            else if(direction.x < 0){
                SetAnimFloat(Vector2.left);
            }
        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y)){
            if(direction.y > 0){
                SetAnimFloat(Vector2.up);
            }
            else if(direction.y < 0){
                SetAnimFloat(Vector2.down);
            }
        }
    }

    //Change the state of the enemy
    private void ChangeState(EnemyState newState){
        if(currentState != newState){
            currentState = newState;
        }
    }
}
