using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //player._playerModifier.MovementStateModifier = player._playerModifier.AirSpeedModifier;
        player._rangedWeaponModifier.StateReloadingTimeModifier = player._rangedWeaponModifier.ReloadingTimeStandingModifier;

        player._rangedWeaponModifier.HorizontalRecoilStateModifier = player._rangedWeaponModifier.RecoilAirModifier;
        player._rangedWeaponModifier.VerticalRecoilStateModifier = player._rangedWeaponModifier.RecoilAirModifier;

        player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireStateModifier = player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireAirModifier;
        player._rangedWeaponModifier.MinuteOfAngleMinStateModifier = player._rangedWeaponModifier.MinuteOfAngleMinAirModifier;
        player._rangedWeaponModifier.MinuteOfAngleRecoveryStateModifier = player._rangedWeaponModifier.MinuteOfAngleRecoveryPerSecAirModifier;
    }

    public override void Exit()
    {
        base.Exit();

        
    }

    public override void Update()
    {
        base.Update();
        

        if (stateMachine.Player._characterController.isGrounded || stateMachine.Player._characterController.velocity.y == 0)
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

        MoveCharacterController();
    }


    private void MoveCharacterController()
    {
        Vector3 Dir = new Vector3(stateMachine.MovementInput.x, 0.0f, stateMachine.MovementInput.y);

        player._characterController.Move(
            ((_mainCamera.transform.rotation * Dir).normalized * player._playerModifier.MovementSpeed)
            * Time.deltaTime
            );
    }

}