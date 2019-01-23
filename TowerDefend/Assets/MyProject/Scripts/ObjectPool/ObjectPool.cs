using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public GameObject[] objectsPrefab;

    public List<GameObject> pooled = new List<GameObject>();

    /// <summary>
    /// 根据名字生成一个Object
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetGameObject(string type)
    {
        //首先检查对象池是否存在该对象
        foreach (GameObject item in pooled)
        {
            if(item.name == type && !item.activeInHierarchy)
            {
                //存在直接返回该对象
                item.SetActive(true);
                return item;
            }
        }

        //不存在则新建对象
        for (int i = 0; i < objectsPrefab.Length; i++)
        {
            if(objectsPrefab[i].name == type)
            {
                GameObject newObject = Instantiate(objectsPrefab[i]);
                pooled.Add(newObject);
                newObject.name = type;
                return newObject;
            }
        }

        return null;
    }

    public void ReleaseObject(GameObject go)
    {
        go.SetActive(false);
    }
}
