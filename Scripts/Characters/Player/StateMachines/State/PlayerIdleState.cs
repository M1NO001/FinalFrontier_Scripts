using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        player._playerModifier.MovementStateModifier = player._playerModifier.IdleSpeedModifier;
        player._rangedWeaponModifier.StateReloadingTimeModifier = player._rangedWeaponModifier.ReloadingTimeStandingModifier;

        player._rangedWeaponModifier.HorizontalRecoilStateModifier = player._rangedWeaponModifier.RecoilStandingModifier;
        player._rangedWeaponModifier.VerticalRecoilStateModifier = player._rangedWeaponModifier.RecoilStandingModifier;

        player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireStateModifier = player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireStandingModifier;
        player._rangedWeaponModifier.MinuteOfAngleMinStateModifier = player._rangedWeaponModifier.MinuteOfAngleMinStandingModifier;
        player._rangedWeaponModifier.MinuteOfAngleRecoveryStateModifier = player._rangedWeaponModifier.MinuteOfAngleRecoveryPerSecStandingModifier;

        base.Enter();
        //StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        player._playerModifier.MovementSpeedModifier = 1f;
        base.Exit();
        //StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
            return;
        }
    }
}