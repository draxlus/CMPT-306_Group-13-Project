﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int health;

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
