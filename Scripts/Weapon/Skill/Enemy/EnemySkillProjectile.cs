using UnityEngine;

public class EnemySkillProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHit;
    [SerializeField] private LayerMask Enemy;

    public int damage;

    public Rigidbody _skillRigidbody;


    private void Awake()
    {
        _skillRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    public void Initialize(Vector3 position, Quaternion quaternion)
    {
        transform.position = position;
        transform.rotation = quaternion;
    }

    public void Shoot(float speed, int _damage)
    {
        damage = _damage;
        _skillRigidbody.velocity = transform.forward * speed;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        _skillRigidbody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Instantiate(vfxHit, transform.position, Quaternion.identity);
        //Destroy(gameObject);
        if (other.gameObject.layer == 6)
        {
            other.GetComponentInChildren<EnemyHealth>().TakeDamage(damage);
        }

        ObjectPoolManager.Instance.ReleaseObjectToPool(gameObject);
    }
}
