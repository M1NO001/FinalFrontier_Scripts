using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    Player player;

    public AudioSource Air;
    public AudioSource Enemy;
    public AudioSource Wall;

    public CapsuleCollider col;

    public ParticleSystem collideSFX;

    public LayerMask MonsterlayerMask;

    private List<Collider> alreadyColliderWith = new List<Collider>();

    public void Awake()
    {
        player = GetComponentInParent<Player>();
        col = GetComponentInParent<CapsuleCollider>();
    }

    public void OnEnable()
    {
        col.enabled = false;
        alreadyColliderWith.Clear();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (alreadyColliderWith.Contains(other)) return;
        alreadyColliderWith.Add(other);

        if (other.gameObject.layer == 7)
        {
            other.GetComponentInChildren<EnemyHealth>().TakeDamage(player._meleeWeaponModifier.Damage);
            if (Enemy) Enemy.Play();

        }
        else if(other.gameObject.layer != 7 && other.gameObject.layer != 6) 
        {
            if (Wall) Wall.Play();
        }
    }

}
