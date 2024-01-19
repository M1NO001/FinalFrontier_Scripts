using System.Collections;
using UnityEngine;

public class HealOverTime : Skill
{
    private float skillTime;
    private float playerHealthPointRecovery;

    public override void SetSkillData(SkillSO skillData)
    {
        skillTime = skillData.skillTime;
        playerHealthPointRecovery = skillData.playerHealthPointRecovery;

        ActiveSkill();
    }

    public override void ActiveSkill()
    {
        StartCoroutine(StartSkillActivation(skillTime));
    }

    IEnumerator StartSkillActivation(float skillTime)
    {
        while (skillTime >= 1.0f)
        {
            skillTime -= 1f;
            SkillManager.Instance.player._playerConditions.Heal(playerHealthPointRecovery);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
