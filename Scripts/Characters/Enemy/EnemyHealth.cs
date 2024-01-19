using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth;
    protected int health;
    public event Action OnDie;
    public event Action<float, float> OnTakeDamage;
    public event Action OnReusedAndInitHealth;

    private bool inPhase2;
    private bool inPhase3;
    public event Action OnPhase2;
    public event Action OnPhase3;

    public bool IsDead;

    private void Start()
    {
        health = maxHealth;
    }

    public void HealthInitialize(int maxHp)
    {
        IsDead = false;
        maxHealth = maxHp;
        health = maxHealth;
        OnReusedAndInitHealth?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;
        health = Mathf.Max(health - damage, 0);

        OnTakeDamage?.Invoke(damage, health);

        if (health < maxHealth * 0.7f && !inPhase2)
        {
            inPhase2 = true;
            OnPhase2?.Invoke();
        }

        if (health < maxHealth * 0.4f && !inPhase3)
        {
            inPhase3 = true;
            OnPhase3?.Invoke();
        }

        if (health == 0 && !IsDead)
        {
            IsDead = true;
            OnDie?.Invoke();
        }
    }
}