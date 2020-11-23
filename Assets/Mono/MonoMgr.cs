using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 1.提供外部类update函数
/// 2.提供外部类协程函数
/// </summary>
public class MonoMgr : BaseMgr<MonoMgr>
{
    private MonoControl control;

    public MonoMgr()
    {
        GameObject obj = new GameObject("MonoControl");
        control = obj.AddComponent<MonoControl>();
    }
    public void AddUpdateListener(UnityAction fun)
    {
        control.AddUpdateListener(fun);
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        control.RemoveUpdateListener(fun);
    }
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return control.StartCoroutine(routine);
    }
}
