using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
    public GameObject fatherObj;
    public List<GameObject> poolList;

    public PoolData(GameObject obj, GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>() {obj};
    }

    public void PushObj(GameObject obj)
    {
        poolList.Add(obj);
        obj.transform.parent = fatherObj.transform;
        obj.SetActive(false);
    }

    public GameObject GetObj()
    {
        GameObject obj = null;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }
}
/// <summary>
/// BufferPool Management
/// 1.Dictionary/List
/// 2.Gameobject and Resouce API
/// </summary>
public class PoolMgr: BaseMgr<PoolMgr>
{

    private GameObject poolObj;
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    /// <summary>
    /// get Gameobject from bufferpool
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObj(string name)
    {
        GameObject obj = null;
        if(poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            obj = poolDic[name].GetObj();
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;
        }
        return obj;
    }
    /// <summary>
    /// push GameObject to Bufferpool
    /// </summary>
    public void PushObj(string name,GameObject obj)
    {
        if (poolObj == null)
        {
            poolObj = new GameObject("pool");
        }
        obj.transform.parent = poolObj.transform;
        obj.SetActive(false);
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        else
        {
            poolDic.Add(name, new PoolData(obj,poolObj));
            poolDic[name].PushObj(obj);
        }
    }
    /// <summary>
    /// Clean buffer pool
    /// mainly using in scene switch
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
