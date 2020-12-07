using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    public float health;
    [SerializeField] ObjectHealthBar healthBar;
    [SerializeField] BoxCollider2D[] colliders;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        IsDead();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            if (health > 0)
            {
                health -= .05f;
                healthBar.SetSize(health);
            }
        }
    }


    

    private void IsDead()
    {
        if (health <= 0 && (!gameObject.CompareTag("NotMainTower")))
        {
            SceneManager.LoadScene(3);
        }
        else if(health <= 0 && gameObject.CompareTag("NotMainTower"))
        {
            Destroy(gameObject);
        }
    }

}
