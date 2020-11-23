using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 资源加载模块
/// 1.同步异步
/// 2.协程
/// 3.泛型
/// 4.Lamda表达式
/// </summary>
public class ResMgr : BaseMgr<ResMgr>
{   
    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T Load<T>(string name) where T:Object
    {
        T res = Resources.Load<T>(name);
        if(res is GameObject)
        {
            return GameObject.Instantiate(res);
        }
        else
        {
            return res;
        }
        
    }
    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callbake"></param>
    public void LoadAsyn<T>(string name, UnityAction<T> callback) where T: Object
    {
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsyn(name, callback));
    }
    
    private IEnumerator ReallyLoadAsyn<T>(string name, UnityAction<T> callback) where T:Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;
        if(r.asset is GameObject)
        {
            callback(GameObject.Instantiate(r.asset) as T);
        }
        else
        {
            callback(r.asset as T);
        }
    }

}
