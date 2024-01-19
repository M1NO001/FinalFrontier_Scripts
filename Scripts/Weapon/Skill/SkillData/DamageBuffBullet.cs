using System.Collections;
using UnityEngine;

public class DamageBuffBullet : Skill
{
    private float skillTime;
    private float projectileDamage;

    private float bulletIncreaseSize;
    private float damageIncreasedNum;
    public GameObject damageIncreasedBullet;
    public GameObject defaultBullet;


    public override void SetSkillData(SkillSO skillData)
    {
        bulletIncreaseSize = 100f;
        skillTime = skillData.skillTime;
        projectileDamage = skillData.projectileDamage;
        damageIncreasedNum = (100 + projectileDamage) / 100;
        ActiveSkill();
    }

    public override void ActiveSkill()
    {
        StartCoroutine(StartSkillActivation(skillTime));
        StartCoroutine(ReturnBaseState(skillTime));
    }


   IEnumerator StartSkillActivation(float skillTime)
    {
        SkillManager.Instance.player._rangedWeaponModifier.pfBulletProjectile = (GameObject)damageIncreasedBullet;
        SkillManager.Instance.player._rangedWeaponModifier.ProjectileDamageModifier += damageIncreasedNum;
        SkillManager.Instance.player._rangedWeaponModifier.ProjectileSizeModifier *= bulletIncreaseSize;

        while (skillTime > 1.0f)
        {
            skillTime -= 1f;
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator ReturnBaseState(float SkillTime)
    {
        yield return new WaitForSeconds(skillTime);
        ReturnBaseState();
    }

    private void ReturnBaseState()
    {
        SkillManager.Instance.player._rangedWeaponModifier.pfBulletProjectile = defaultBullet;
        SkillManager.Instance.player._rangedWeaponModifier.ProjectileDamageModifier -= damageIncreasedNum;
        SkillManager.Instance.player._rangedWeaponModifier.ProjectileSizeModifier /= bulletIncreaseSize;
    }

}
