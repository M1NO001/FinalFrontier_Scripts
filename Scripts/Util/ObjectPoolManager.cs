using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    public List<ObjectPool> objectPools = new List<ObjectPool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void CreateObjectPool(GameObject go)
    {
        GameObject Obj = new GameObject(go.name+"(Clone)");
        Obj.transform.parent = transform;
        ObjectPool objPool = Obj.AddComponent<ObjectPool>();
        objPool.Initialize(go);
        objectPools.Add(objPool);
    }

    public GameObject GetObjectFromPool(GameObject go, Vector3 position, Quaternion rotation)
    {
        ObjectPool pool = objectPools.Find(p => p.name == go.name + "(Clone)");
        if (pool != null)
        {
            return pool.GetObject(position, rotation);
        }
        else
        {
            CreateObjectPool(go);
            pool = objectPools.Find(p => p.name == go.name + "(Clone)");
            return pool.GetObject(position, rotation);
        }
    }


    public void ReleaseObjectToPool(GameObject go)
    {
        ObjectPool pool = objectPools.Find(p => p.name == go.name);
        if (pool != null)
        {
            pool.ReleaseObject(go);
        }
        else
        {
            Debug.LogError("Object pool with name '" + go.name + "' not found.");
        }
    }

    public void ReleaseAllObjectsInPool(GameObject go)
    {
        ObjectPool pool = objectPools.Find(p => p.name == go.name + "(Clone)");

        if (pool != null)
        {
            pool.ReleaseAllObjects();
        }
    }
}