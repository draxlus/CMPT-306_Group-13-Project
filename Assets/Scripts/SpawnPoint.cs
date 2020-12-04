using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float health;
    public int swampnumber;
    [SerializeField] private ObjectHealthBar healthBar;
    private float maxHealth = 5;
    private float healthRatio = 1;
    private Rigidbody2D rb;
    public float hitCount;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        swampnumber = 2;
        rb = GetComponent<Rigidbody2D>();
    }

    

    private void TakeDamage(float damage)
    {

        health -= damage;
        healthRatio = healthRatio - (damage / maxHealth);
        healthBar.SetSize(healthRatio);
        if (health <= 0)
        {
            WinCondition.swampPointsDestroyed++;
            Destroy(gameObject);
        }
    }


    public void Knock(float knocktime, float damage)
    {
        StartCoroutine(KnockCo(knocktime));
        TakeDamage(damage);
    }
    private IEnumerator KnockCo(float knocktime)
    {

        if (rb != null)
        {
            yield return new WaitForSeconds(knocktime);
            hitCount = 0;
        }
    }
}
