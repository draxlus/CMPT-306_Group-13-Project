using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleAngeryEnemyAI : Enemy
{
    //Transform of the target
    public Transform target;

    private Rigidbody2D rb;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Core").transform;
        currentState = EnemyState.idle;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChaseCore();
    }
    
    void ChaseCore(){
        if(currentState != EnemyState.stagger){
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            changeAnim(temp - transform.position);
            rb.MovePosition(temp);
            ChangeState(EnemyState.walk);
        }
        else if(health <= 0){
            this.gameObject.SetActive(false);
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

private void ChangeState(EnemyState newState){
    if(currentState != newState){
        currentState = newState;
    }
}
}

