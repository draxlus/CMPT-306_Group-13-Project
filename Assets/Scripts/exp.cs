using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exp : MonoBehaviour
{
    int exps = 0;
    int levelup = 0;
    int level = 0;

    void Start()
    {
        exps = 0;
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        while (exps > levelup) {
            exps -= levelup;
            level++;
        }
    }
}
