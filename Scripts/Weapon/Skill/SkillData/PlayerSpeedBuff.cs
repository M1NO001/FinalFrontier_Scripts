using System.Collections;
using UnityEngine;

public class PlayerSpeedBuff : Skill
{
    private float skillTime;
    private float playerMovementSpeed;

    public override void SetSkillData(SkillSO skillData)
    {
        skillTime = skillData.skillTime;
        playerMovementSpeed = skillData.playerMovementSpeed;

        ActiveSkill();
    }

    public override void ActiveSkill()
    {
       StartCoroutine(StartSkillActivation(skillTime));
        StartCoroutine(ReturnBaseState(skillTime));
    }


    IEnumerator StartSkillActivation(float skillTime)
    {
        SkillManager.Instance.player._playerModifier.WalkSpeedModifier *= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.RunSpeedModifier *= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.CrouchSpeedModifier *= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.AirSpeedModifier *= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.ForwardSpeedModifier *= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.BackwardSpeedModifier *= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.Non_SideSpeedModifier *= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.SideSpeedModifier *= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.Non_AimingMoveSpeedModifier *= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.AimingMoveSpeedModifier *= playerMovementSpeed;

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
        SkillManager.Instance.player._playerModifier.WalkSpeedModifier /= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.RunSpeedModifier /= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.CrouchSpeedModifier /= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.AirSpeedModifier /= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.ForwardSpeedModifier /= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.BackwardSpeedModifier /= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.Non_SideSpeedModifier /= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.SideSpeedModifier /= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.Non_AimingMoveSpeedModifier /= playerMovementSpeed;
        SkillManager.Instance.player._playerModifier.AimingMoveSpeedModifier /= playerMovementSpeed;
    }
}
