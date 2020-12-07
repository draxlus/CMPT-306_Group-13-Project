using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{

    [SerializeField] float timer;
    [SerializeField] Text timerText;
 

    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F1");
    }
}
