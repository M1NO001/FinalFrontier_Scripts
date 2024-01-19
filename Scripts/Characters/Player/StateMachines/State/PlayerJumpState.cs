using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    float jumpForce;

    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        jumpForce = stateMachine.Player._playerModifier.JumpForce;
        player._forceReceiver.Jump(jumpForce);

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.JumpParameterHash);

        player._rangedWeaponModifier.StateReloadingTimeModifier = player._rangedWeaponModifier.ReloadingTimeStandingModifier;

        player._rangedWeaponModifier.HorizontalRecoilStateModifier = player._rangedWeaponModifier.RecoilAirModifier;
        player._rangedWeaponModifier.VerticalRecoilStateModifier = player._rangedWeaponModifier.RecoilAirModifier;

        player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireStateModifier = player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireAirModifier;
        player._rangedWeaponModifier.MinuteOfAngleMinStateModifier = player._rangedWeaponModifier.MinuteOfAngleMinAirModifier;
        player._rangedWeaponModifier.MinuteOfAngleRecoveryStateModifier = player._rangedWeaponModifier.MinuteOfAngleRecoveryPerSecAirModifier;

    }


    protected override void Move()
    {
        Vector3 Dir = new Vector3(stateMachine.MovementInput.x, 0.0f, stateMachine.MovementInput.y);


        player._characterController.Move(
            ((_mainCamera.transform.rotation * Dir).normalized * player._playerModifier.MovementSpeed * 2f)
            * Time.deltaTime
            );

        player.transform.forward = new Vector3(_mainCamera.transform.forward.x, 0, _mainCamera.transform.forward.z);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);

        player._playerConditions.isStaminaDecay = false;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (stateMachine.Player._characterController.velocity.y < 0f)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }

        if (player._characterController.isGrounded)
        {
            player._animator.SetBool("Grounded", true);
            if (stateMachine.previousState.GetType().ToString() != "PlayerJumpState")
            {
                stateMachine.ChangeState(stateMachine.previousState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
            return;
        }
    }
}