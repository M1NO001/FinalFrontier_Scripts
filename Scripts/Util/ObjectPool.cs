using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 1000;
    private List<GameObject> objects;

    private void Awake()
    {
        
    }

    public void Initialize(GameObject go)
    {
        prefab = go;
        objects = new List<GameObject>();

        GameObject obj = Instantiate(go, transform);
        obj.SetActive(false);
        objects.Add(obj);
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        foreach (var obj in objects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        if (objects.Count < poolSize)
        {
            var ob = Instantiate(prefab, position, rotation, transform);
            objects.Add(ob);
            return ob;
        }

        Debug.Log("ObjectPool Out of Range");
        return null;
    }


    public void ReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ReleaseAllObjects()
    {
        foreach(GameObject obj in objects)
        {
            ReleaseObject(obj);
        }
    }
}