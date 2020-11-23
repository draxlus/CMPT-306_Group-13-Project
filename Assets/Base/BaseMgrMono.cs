using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例模式基类，继承monoBehaviour
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseMgrMono<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        return instance;
    }

    protected virtual void Awake()
    {
        instance = this as T;
    }
}
//protected override void Awake(){
//    base.Awake();
//}

