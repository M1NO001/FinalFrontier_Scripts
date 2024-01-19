using System;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class EggEnemy : MonoBehaviour
{
    public event Action OnDestroy;
    [field: SerializeField] private Image healthBar;
    private EnemyHealth CharacterHealth;
    private int maxHealth = 50;
    private int exp = 2000;


    private void Start()
    {
        CharacterHealth = GetComponent<EnemyHealth>();
        CharacterHealth.HealthInitialize(maxHealth);
        CharacterHealth.OnDie += OnDie;
        CharacterHealth.OnTakeDamage += OnTakeDamage;
    }

    private void OnTakeDamage(float damage, float health)
    {
        healthBar.fillAmount = health / maxHealth;
        gameObject.transform.DOShakePosition(0.5f, 0.1f, 2);

    }
    private void OnDie()
    {
        GamePlaySceneManager.Instance.PlayerClass.AddExp(exp);
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }
}
