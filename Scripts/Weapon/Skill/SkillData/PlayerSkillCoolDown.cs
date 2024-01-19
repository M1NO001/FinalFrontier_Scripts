using System.Collections;
using UnityEngine;

public class PlayerSkillCoolDown : Skill
{
    private float skillTime;
    private float coolTimeEffect;

    public override void SetSkillData(SkillSO skillData)
    {
        skillTime = skillData.skillTime;
        coolTimeEffect = skillData.effectNum;

        ActiveSkill();
    }

    public override void ActiveSkill()
    {
        StartCoroutine(StartSkillActivation(skillTime));
        StartCoroutine(ReturnBaseState(skillTime));
    }


    IEnumerator StartSkillActivation(float skillTime)
    {
        SkillManager.Instance.player._playerSkill.skillCoolTime = GameManager.Instance.playerSkillData.coolTime;
        SkillManager.Instance.player._playerSkill.skillTimer = GameManager.Instance.playerSkillData.coolTime;
        SkillManager.Instance.player._playerSkill.skillCoolTime *= coolTimeEffect;
        SkillManager.Instance.player._playerSkill.skillTimer *= coolTimeEffect;

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
        SkillManager.Instance.player._playerSkill.skillCoolTime = GameManager.Instance.playerSkillData.coolTime;
        SkillManager.Instance.player._playerSkill.skillTimer = GameManager.Instance.playerSkillData.coolTime;
    }
}
