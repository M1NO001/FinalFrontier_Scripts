using System.Diagnostics;

public class PlayerMeleeComboAttackState : PlayerMeleeAttackState
{
    private bool alreadyApplyCombo;

    AttackInfoData attackInfoData;

    public PlayerMeleeComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
       
            base.Enter();
            alreadyApplyCombo = false;
        if (player._playerConditions.UseStamina(player._meleeWeaponModifier.UseStamina))
        {
            player._animator.SetFloat("MeleeAttackSpeed", player._meleeWeaponModifier.Speed);
            player._animator.SetBool("MeleeComboAttack", true);


            int comboIndex = stateMachine.ComboIndex;
            attackInfoData = player.Data.PlayerMeleeWeaponData.GetAttackInfo(comboIndex);
            player._animator.SetInteger("ComboAttack", comboIndex);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        


        //StartAnimation(player.AnimationData.MeleeComboAttackParameterHash);
        
    }

    public override void Exit()
    {
        base.Exit();
        player._animator.SetBool("MeleeComboAttack", false);

        if (!alreadyApplyCombo)
        {
            stateMachine.ComboIndex = 0;
        }

        //StopAnimation(player.AnimationData.MeleeComboAttackParameterHash);
    }


    private void TryComboAttack()
    {
        if (alreadyApplyCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!stateMachine.IsAttacking) return;
        
        alreadyApplyCombo = true;
    }


    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player._animator, "MeleeAttack");
       
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= attackInfoData.ComboTransitionTime)
                TryComboAttack();
        }
        else
        {
            if (alreadyApplyCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.MeleeComboAttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}
