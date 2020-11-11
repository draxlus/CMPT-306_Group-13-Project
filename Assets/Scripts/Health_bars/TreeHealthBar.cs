using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealthBar : MonoBehaviour
{
    private Transform bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("TreeBar");
    }

   public void SetSize(float sizeNormalized){
       bar.localScale = new Vector3(sizeNormalized, 1f);
   }
}
