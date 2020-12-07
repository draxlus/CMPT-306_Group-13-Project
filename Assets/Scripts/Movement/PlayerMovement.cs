using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState{
    idle,
    walk,
    attack,
    interact,
    stagger
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector3 change;
    private Animator animator;
    private float maxHealth;
    public float health;

    [SerializeField] Inventory inventory;
    [SerializeField] List<ItemAmount> materials;
    public int hitCount = 0;

    
    [SerializeField] private PlayerHealthBar healthBar;

    void Start(){
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        health = 10f;
        maxHealth = health;
    }


    private void Update()
    {
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            HealthIncrease();
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
       
        if(currentState == PlayerState.walk || currentState == PlayerState.idle){
            UpdateAnimationAndMove();
        }
    }

    void UpdateAnimationAndMove()
    {
        if(change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
            currentState = PlayerState.walk;
        }
        else
        {
            animator.SetBool("moving", false);
            currentState = PlayerState.idle;
        }

    }
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }
    void MoveCharacter()
    {
        change.Normalize();
        rb.MovePosition(transform.position + change * moveSpeed * Time.deltaTime);
    }  

    private void TakeDamage(float damage){
        health -= damage;
        healthBar.SetSize((health - (damage/maxHealth))/10);
        if (health <= 0){
            SceneManager.LoadScene(3);
        }
    }

    public void Knock(float knocktime, float damage){
        StartCoroutine(KnockCo(knocktime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(float knocktime){

        if (rb != null){
            yield return new WaitForSeconds(knocktime);
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rb.velocity = Vector2.zero;
            hitCount = 0;
        }
    }

    private void HealthIncrease()
    {
        foreach (ItemAmount itemAmount in materials)
        {
            if (inventory.ItemCount(itemAmount.Item.ID) < itemAmount.Amount)
            {
                Debug.LogWarning("You don't have the required materials.");
                NotificationManager.Instance.SetNewNotification("No Green Gem in Inventory");
            }
            else
            {
                for (int j = 0; j < itemAmount.Amount; j++)
                {
                    Item oldItem = inventory.RemoveItem(itemAmount.Item.ID);
                    oldItem.Destroy();
                }
                health += 2f;
                healthBar.SetSize(health/10);
                NotificationManager.Instance.SetNewNotification("Consumed " + itemAmount.Item.name);
            }
        }
    }
}
