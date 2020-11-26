using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTower2 : MonoBehaviour
{
    public GameObject missile;
    private float reloadTime;
    private GameObject enemy;
    private bool hasFired;

    private void Start()
    {
        reloadTime = 2f;
        hasFired = false;
    }

    private void shoot()
    {
        float angleStep = 360f / 10;
        float angle = 0f;

        for (int i = 0; i <= 10 - 1; i++)
        {

            float projectileDirXposition = enemy.transform.position.x;
            float projectileDirYposition = enemy.transform.position.y;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - new Vector2(transform.position.x, transform.position.y)).normalized * 5;

            var proj = Instantiate(missile, transform.position, transform.rotation);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("COLLISION NAME" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject;
            if (!hasFired)
            {
                print("Shot");
                reloadTime = 5;
                shoot();
                hasFired = true;
            }
        }
    }

    private void Update()
    {
        if (reloadTime >= 0 && hasFired)
        {
            reloadTime -= Time.deltaTime;
            print("Reloading");
        }
        else if (reloadTime < 0)
        {
            hasFired = false;
            reloadTime = 5f;
            print("Can fire again");
        }


    }
}
