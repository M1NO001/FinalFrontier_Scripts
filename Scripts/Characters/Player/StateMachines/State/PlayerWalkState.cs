using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        
        base.Enter();
        player._playerModifier.MovementStateModifier = player._playerModifier.WalkSpeedModifier;
        player._rangedWeaponModifier.StateReloadingTimeModifier = player._rangedWeaponModifier.ReloadingTimeStandingModifier;

        player._rangedWeaponModifier.HorizontalRecoilStateModifier = player._rangedWeaponModifier.RecoilWalkModifier;
        player._rangedWeaponModifier.VerticalRecoilStateModifier = player._rangedWeaponModifier.RecoilWalkModifier;

        player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireStateModifier = player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireWalkModifier;
        player._rangedWeaponModifier.MinuteOfAngleMinStateModifier = player._rangedWeaponModifier.MinuteOfAngleMinWalkModifier;
        player._rangedWeaponModifier.MinuteOfAngleRecoveryStateModifier = player._rangedWeaponModifier.MinuteOfAngleRecoveryPerSecWalkModifier;

    }

    public override void Exit()
    {
        base.Exit();
        
    }


    protected override void OnSprintStarted(InputAction.CallbackContext context)
    {
        base.OnSprintStarted(context);
        stateMachine.ChangeState(stateMachine.SprintState);
    }
}