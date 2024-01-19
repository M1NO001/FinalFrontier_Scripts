using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    public int damage;
    public float duration;
    public float speed;

    private bool inEnabled;

    protected virtual void OnEnable()
    {
        inEnabled = true;
        Invoke("ReleaseEffect", duration);
    }

    protected virtual void OnDisable()
    {
        inEnabled = false;
    }

    protected void ReleaseEffect()
    {
        if(inEnabled == true)        
            ObjectPoolManager.Instance.ReleaseObjectToPool(gameObject);
    }
}
