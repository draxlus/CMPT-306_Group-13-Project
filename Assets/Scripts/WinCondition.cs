using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public static int swampPointsDestroyed;
    private void Start()
    {
        swampPointsDestroyed = 0;
    }

    private void Update()
    {
        if(swampPointsDestroyed == 4)
        {
            print("Won the game");
            Time.timeScale = 0;
        }
    }
}
