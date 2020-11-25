using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNight : MonoBehaviour
{
    public Light2D sun;
    public int day;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sunset());
        day = 0;
    }

    IEnumerator Sunset()
    {
        while(sun.intensity > 0.3f)
        {
            sun.intensity -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(Sunrise());
        StopCoroutine(Sunset());
        day += 1;
    }
    IEnumerator Sunrise()
    {
        while (sun.intensity < 1f)
        {
            sun.intensity += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(Sunset());
        StopCoroutine(Sunrise());
        
    }
    private void Update()
    {
       
    }
}
