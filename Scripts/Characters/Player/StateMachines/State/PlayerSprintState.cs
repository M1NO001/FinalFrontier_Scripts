using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprintState : PlayerGroundedState
{
    public PlayerSprintState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //StartAnimation(stateMachine.Player.AnimationData.SprintParameterHash);
        player._playerModifier.MovementStateModifier = player._playerModifier.RunSpeedModifier;
        player._rangedWeaponModifier.StateReloadingTimeModifier = player._rangedWeaponModifier.ReloadingTimeStandingModifier;

        player._rangedWeaponModifier.HorizontalRecoilStateModifier = player._rangedWeaponModifier.RecoilRunModifier;
        player._rangedWeaponModifier.VerticalRecoilStateModifier = player._rangedWeaponModifier.RecoilRunModifier;

        player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireStateModifier = player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireRunModifier;
        player._rangedWeaponModifier.MinuteOfAngleMinStateModifier = player._rangedWeaponModifier.MinuteOfAngleMinRunModifier;
        player._rangedWeaponModifier.MinuteOfAngleRecoveryStateModifier = player._rangedWeaponModifier.MinuteOfAngleRecoveryPerSecRunModifier;

        player._playerConditions.isStaminaDecay = true;

        
        player._targetTracking.weight = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player._animator.SetLayerWeight(1, 1);
        player._playerConditions.isStaminaDecay = false;
        //StopAnimation(stateMachine.Player.AnimationData.SprintParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (!player.isReloading) player._animator.SetLayerWeight(1, 0);
        else player._animator.SetLayerWeight(1, 1);

        if (!player._playerConditions.UseStamina(player._playerModifier.StaminaPointConsumption * Time.deltaTime))
        {
            stateMachine.ChangeState(stateMachine.WalkState);
        }
    }

    protected override void OnSprintStarted(InputAction.CallbackContext context)
    {
        base.OnSprintStarted(context);
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    protected override void OnAimStarted(InputAction.CallbackContext context)
    {
        base.OnAimStarted(context);
        stateMachine.ChangeState(stateMachine.WalkState);
    }
}