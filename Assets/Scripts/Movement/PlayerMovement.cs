using System.Collections;
using UnityEngine;

public enum PlayerState
{
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
    public bool isAttacking;


    public int hitCount = 0;


    [SerializeField] private PlayerHealthBar healthBar;

    void Start()
    {
        currentState = PlayerState.idle;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.Space) && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            
            StartCoroutine(AttackCo());
            
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            currentState = PlayerState.walk;

        }
        else
        {
            animator.SetLayerWeight(1, 0);
            currentState = PlayerState.idle;
        }

    }
    private IEnumerator AttackCo()
    {

        animator.SetLayerWeight(2, 1);
        isAttacking = true;
        animator.SetBool("attacking", isAttacking);
        currentState = PlayerState.attack;
        yield return null;

        
        isAttacking = false;
        animator.SetBool("attacking", isAttacking);
        yield return new WaitForSeconds(.3f);
        animator.SetLayerWeight(2, 0);
        currentState = PlayerState.idle;
        
    }
    void MoveCharacter()
    {
        animator.SetLayerWeight(1, 1);
        change.Normalize();
        rb.MovePosition(transform.position + change * moveSpeed * Time.deltaTime);
    }

    public void Knock(float knocktime)
    {
        StartCoroutine(KnockCo(knocktime));
    }

    private IEnumerator KnockCo(float knocktime)
    {

        if (rb != null)
        {
            yield return new WaitForSeconds(knocktime);
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rb.velocity = Vector2.zero;
            hitCount = 0;
        }
    }
}
