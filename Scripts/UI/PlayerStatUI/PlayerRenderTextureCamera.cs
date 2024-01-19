using UnityEngine;

public class PlayerRenderTextureCamera : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset;
    void Start()
    {
        offset = GetComponent<Transform>().position - targetTransform.position;
    }

    void LateUpdate()
    {
        transform.position = targetTransform.position + offset;
    }
}