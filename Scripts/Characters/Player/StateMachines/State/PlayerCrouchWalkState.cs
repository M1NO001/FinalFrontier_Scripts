using UnityEngine.InputSystem;

public class PlayerCrouchWalkState : PlayerGroundedState
{
    public PlayerCrouchWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        player._playerModifier.MovementStateModifier = player._playerModifier.CrouchSpeedModifier;
        player._rangedWeaponModifier.StateReloadingTimeModifier = player._rangedWeaponModifier.ReloadingTimeCrouchingModifier;

        player._rangedWeaponModifier.HorizontalRecoilStateModifier = player._rangedWeaponModifier.RecoilCrouchModifier;
        player._rangedWeaponModifier.VerticalRecoilStateModifier = player._rangedWeaponModifier.RecoilCrouchModifier;

        player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireStateModifier = player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireCrouchModifier;
        player._rangedWeaponModifier.MinuteOfAngleMinStateModifier = player._rangedWeaponModifier.MinuteOfAngleMinCrouchModifier;
        player._rangedWeaponModifier.MinuteOfAngleRecoveryStateModifier = player._rangedWeaponModifier.MinuteOfAngleRecoveryPerSecCrouchModifier;

        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.CrouchParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.CrouchState);
    }

    protected override void OnMovementStarted(InputAction.CallbackContext context)
    {
        
    }

    protected override void OnCrouchStarted(InputAction.CallbackContext context)
    {
        StopAnimation(stateMachine.Player.AnimationData.CrouchParameterHash);
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    protected override void OnSprintStarted(InputAction.CallbackContext context)
    {
        StopAnimation(stateMachine.Player.AnimationData.CrouchParameterHash);
        stateMachine.ChangeState(stateMachine.SprintState);
    }

}
