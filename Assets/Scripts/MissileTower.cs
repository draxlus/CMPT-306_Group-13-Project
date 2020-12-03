using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTower : MonoBehaviour {

    public GameObject missile;
    private float reloadTime;
    private GameObject[] enemies;
    private bool hasFired;
    public float radius = 6;

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

            float projectileDirXposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 2;
            float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 2;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - new Vector2(transform.position.x, transform.position.y)).normalized * 5;

            var proj = Instantiate(missile, transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }

    private void checkDistance()
    {
        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance <= radius)

                {

                    if (!hasFired)
                    {
                        print("Shot");
                        reloadTime = 5;
                        shoot();
                        hasFired = true;
                    }
                }
            }
        }
    }


    private void findEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    private void Update()
    {
        Invoke("findEnemies", 2);
        if(reloadTime >= 0 && hasFired)
        {
            reloadTime -= Time.deltaTime;
            print("Reloading");
        }
        else if(reloadTime < 0)
        {
            hasFired = false;
            reloadTime = 5f;
            print("Can fire again");
        }
        checkDistance();
    }
}
