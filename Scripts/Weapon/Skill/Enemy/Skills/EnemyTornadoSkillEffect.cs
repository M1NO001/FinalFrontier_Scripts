using UnityEngine;

public class EnemyTornadoSkillEffect : EnemyEffect
{
    private Transform playerTransform;
    private Rigidbody _skillRigidbody;

    private void Start()
    {
        _skillRigidbody = GetComponent<Rigidbody>();
        playerTransform = GamePlaySceneManager.Instance.PlayerClass.transform;
        _skillRigidbody.velocity = (playerTransform.position - transform.position).normalized * speed;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if (playerTransform != null)
        {
            _skillRigidbody.velocity = (playerTransform.position - transform.position).normalized * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            GamePlaySceneManager.Instance.PlayerConditions.TakePhysicalDamage(damage);
            ReleaseEffect();
        }
    }
}
