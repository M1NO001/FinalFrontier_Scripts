
public class PlayerMeleeAttackState : PlayerBaseState
{
    float previousMovementSpeedModifier;
    public PlayerMeleeAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player._twoBoneIKConstraintMelee) player._twoBoneIKConstraintMelee.weight = 1f;
        if (player._targetTracking) player._targetTracking.weight = 0f;
        if (player._idle) player._idle.weight = 0f;

        StartAnimation(stateMachine.Player.AnimationData.MeleeAttackParameterHash);
        player._playerModifier.MovementSpeedModifier = 0;
        player._targetTracking.weight = 0f;
        player._rangedWeaponModifier.RangedWeapon.SetActive(false);
        player._meleeWeaponModifier.MeleeWeapon.SetActive(true);
        player._animator.SetLayerWeight(1, 0);
        previousMovementSpeedModifier = player._playerModifier.MovementSpeedModifier;
        player.canAim = false;
        player.canShoot = false;
        player.canReload = false;
        player.isAttack = true;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.MeleeAttackParameterHash);
        player._playerModifier.MovementSpeedModifier = previousMovementSpeedModifier;
        player.canAim = true;
        player.canShoot = true;
        player.canReload = true;
        player._targetTracking.weight = 1f;
        player._rangedWeaponModifier.RangedWeapon.SetActive(true);
        player._meleeWeaponModifier.MeleeWeapon.SetActive(false);
        player._animator.SetLayerWeight(1, 1);
        player.isAttack = false;

        if (player._twoBoneIKConstraintMelee) player._twoBoneIKConstraintMelee.weight = 0f;
        if (player._targetTracking) player._targetTracking.weight = 1f;
        if (player._idle) player._idle.weight = 1f;
    }

}
