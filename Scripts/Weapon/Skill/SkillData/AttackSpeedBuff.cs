using System.Collections;
using UnityEngine;

public class AttackSpeedBuff : Skill
{
    private float skillTime;
    private float projectileSpeed;
    private float reloadingTime;
    private float bulletIncreaseSize;

    public GameObject attackSpeedIncreasedBullet;
    public GameObject defaultBullet;
    

    public override void SetSkillData(SkillSO skillData)
    {
        bulletIncreaseSize = 100f;
        skillTime = skillData.skillTime;
        projectileSpeed = skillData.projectileSpeed;
        reloadingTime = skillData.reloadingTime;
        ActiveSkill();
    }

    public override void ActiveSkill()
    {
        StartCoroutine(StartSkillActivation(skillTime));
        StartCoroutine(ReturnBaseState(skillTime));
    }

    IEnumerator StartSkillActivation(float skillTime)
    {
        SkillManager.Instance.player._rangedWeaponModifier.pfBulletProjectile = (GameObject)attackSpeedIncreasedBullet;
        SkillManager.Instance.player._rangedWeaponModifier.ProjectileSpeedModifier *= projectileSpeed;
        SkillManager.Instance.player._rangedWeaponModifier.ReloadingTimeModifier *= reloadingTime;
        SkillManager.Instance.player._rangedWeaponModifier.ProjectileSizeModifier *= bulletIncreaseSize;

        while (skillTime >= 1.0f)
        {
            skillTime -= Time.deltaTime;
            yield return new WaitForSeconds(1.0f);
        }

        
        yield return null;
    }

    IEnumerator ReturnBaseState(float SkillTime)
    {
        yield return new WaitForSeconds(skillTime);
        ReturnBaseState();
    }

    private void ReturnBaseState()
    {
        SkillManager.Instance.player._rangedWeaponModifier.pfBulletProjectile = defaultBullet;
        SkillManager.Instance.player._rangedWeaponModifier.ProjectileSpeedModifier /= projectileSpeed;
        SkillManager.Instance.player._rangedWeaponModifier.ReloadingTimeModifier /= reloadingTime;
        SkillManager.Instance.player._rangedWeaponModifier.ProjectileSizeModifier /= bulletIncreaseSize;
    }
}
