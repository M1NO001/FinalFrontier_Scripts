using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngineInternal;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHit;
    [SerializeField] private LayerMask Player;
    [SerializeField] private LayerMask Bullet;

    [SerializeField] private ParticleSystem impact;

    public int damage;
    
    public Rigidbody _bulletRigidbody;

    RaycastHit hit;


    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    public void Initialize(Vector3 position, Quaternion quaternion)
    {
        transform.position = position;
        transform.rotation = quaternion;
    }

    public void Shoot(float speed, float _damage, float criticalChance, float criticalMultiplier)
    {
        
        if(UnityEngine.Random.value <= criticalChance)
        {
            damage = (int)(_damage * criticalMultiplier);
        }
        else damage = (int)_damage;

        _bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        _bulletRigidbody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        _bulletRigidbody.velocity = Vector3.zero;

        if (Physics.Raycast(transform.position - transform.forward*2f, transform.forward, out hit, 10f))
        {
            if (impact != null)
            {
                ParticleSystem particle = ObjectPoolManager.Instance.GetObjectFromPool(impact.gameObject, hit.point, Quaternion.identity).GetComponent<ParticleSystem>();
                particle.transform.forward = hit.normal;
                particle.Emit(1);
            }
        }

        if (other.gameObject.layer == 7)
        {
            other.GetComponentInChildren<EnemyHealth>().TakeDamage(damage);
            //if(impact != null)
            //    ObjectPoolManager.Instance.GetObjectFromPool(impact.gameObject, transform.position, transform.rotation);
        }
        else if(other.gameObject.layer == 0)
        {
            //if (impact != null)
            //    ObjectPoolManager.Instance.GetObjectFromPool(impact.gameObject, transform.position, transform.rotation);
        }
        
        
        //impact.Emit(1);



        ObjectPoolManager.Instance.ReleaseObjectToPool(gameObject);
    }

}
