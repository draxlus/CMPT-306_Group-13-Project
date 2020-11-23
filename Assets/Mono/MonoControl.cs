using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoControl : MonoBehaviour
{
    private event UnityAction updateEvent;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (updateEvent != null)
        {
            updateEvent();
        }
    }
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }
   
}
