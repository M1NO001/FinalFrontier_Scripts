using System.Collections;
using UnityEngine;

public class Tornado : Skill
{
    private float skillTime;
    private float projectileDamage;
    private float skillTimer = 0f;

    public override void SetSkillData(SkillSO skillData)
    {
        skillTime = skillData.skillTime;
        projectileDamage = skillData.projectileDamage;
        
    }

    private void OnEnable()
    {
        SetSkillData(SkillManager.Instance.GetUltSkillData());
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
        };
    }
    
    private void OnTriggerEnter(Collider other)
    {
        projectileDamage = SkillManager.Instance.GetUltSkillData().projectileDamage;
        if (other.gameObject.layer == 7)
        {
            gameObject.transform.position = other.transform.position;
            other.GetComponentInChildren<EnemyHealth>().TakeDamage((int)projectileDamage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        projectileDamage = SkillManager.Instance.GetUltSkillData().projectileDamage;
        if (other.gameObject.layer == 7)
        {
            gameObject.transform.position = other.transform.position;
            other.GetComponentInChildren<EnemyHealth>().TakeDamage((int)projectileDamage);
        }
    }

}
