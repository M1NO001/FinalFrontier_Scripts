using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.IsAttacking && !player.isReloading&&
            !player._playerSkill.skillStartAnimation && !player._playerSkill.skillEndAnimation && !player._playerSkill.skillProgressAnimation)
        {
            
            OnAttack();
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!stateMachine.Player._characterController.isGrounded
        && stateMachine.Player._characterController.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }


    protected virtual void OnAttack()
    {
        stateMachine.ChangeState(stateMachine.MeleeComboAttackState);
    }


    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.IdleState);

        base.OnMovementCanceled(context);
    }

    protected override void OnCrouchStarted(InputAction.CallbackContext context)
    {
        base.OnCrouchStarted(context);

        if(player._animator.GetFloat(player.AnimationData.DirectionX) > 0.8
            || player._animator.GetFloat(player.AnimationData.DirectionY) > 0.8)
        {
            stateMachine.ChangeState(stateMachine.CrouchWalkState);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.CrouchState);
        }
    }

    protected virtual void OnMove()
    {
        
    }


    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (player._playerConditions.UseStamina(8f))
        {
            player._playerConditions.isStaminaDecay = true;
            stateMachine.ChangeState(stateMachine.JumpState);
        }
    }

}
