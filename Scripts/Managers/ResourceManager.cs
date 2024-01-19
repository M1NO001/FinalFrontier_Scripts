using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager _instance;

    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ResourceManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("ResourceManager");
                    _instance = obj.AddComponent<ResourceManager>();
                }
            }
            return _instance;
        }
    }

    // 리소스를 로드하는 메서드
    public T LoadResource<T>(string resourcePath) where T : Object
    {
        T resource = Resources.Load<T>(resourcePath);

        if (resource == null)
        {
            Debug.LogError($"Resource not found at path: {resourcePath}");
        }

        return resource;
    }

    // 리소스를 언로드하는 메서드
    public void UnloadResource(Object resource)
    {
        Resources.UnloadAsset(resource);
    }
}
