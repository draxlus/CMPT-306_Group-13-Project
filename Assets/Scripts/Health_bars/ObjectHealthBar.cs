using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealthBar : MonoBehaviour
{
    private Transform bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("ObjectBar");
    }

   public void SetSize(float sizeNormalized){
       bar.localScale = new Vector3(sizeNormalized, 1f);
   }
}
