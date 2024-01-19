using UnityEngine;

public class ElectricBall : Skill
{
    private float projectileDamage;
    private float skillTime;
    private float skillTimer = 0f;

    private void Start()
    {
        SetSkillData(GameManager.Instance.playerSkillData);
    }

    public override void SetSkillData(SkillSO skillData)
    {
        projectileDamage = skillData.projectileDamage;
        skillTime = skillData.skillTime;
    }

    private void Update()
    {
        ActiveSkill();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.GetComponentInChildren<EnemyHealth>().TakeDamage((int)projectileDamage);
        }
        if (other.gameObject.layer == 7 || other.gameObject.layer == 0)
        {
            ObjectPoolManager.Instance.ReleaseObjectToPool(gameObject);
        }

    }

    public override void ActiveSkill()
    {
        if (skillTimer <= skillTime)
        {
            skillTimer += Time.deltaTime;
        }

        else
        {
            ObjectPoolManager.Instance.ReleaseObjectToPool(gameObject);
            skillTimer = 0f;
        }

    }
}
