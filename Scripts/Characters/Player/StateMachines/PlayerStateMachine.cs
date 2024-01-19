using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    // States
    public PlayerIdleState IdleState { get; }
    public PlayerCrouchState CrouchState { get; }
    public PlayerCrouchWalkState CrouchWalkState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerSprintState SprintState { get; }
    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }
    public PlayerMeleeAttackState MeleeComboAttackState { get; }

    
    public Vector2 MovementInput { get; set; }
    public Vector2 LookInput { get; set; }
    public float _cinemachineTargetYaw { get; set; }
    public float _cinemachineTargetPitch { get; set; }
    public float MovementSpeed { get; set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float RotationDamping { get; private set; }

    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }



    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        CrouchState = new PlayerCrouchState(this);
        CrouchWalkState = new PlayerCrouchWalkState(this);
        WalkState = new PlayerWalkState(this);
        SprintState = new PlayerSprintState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        MeleeComboAttackState = new PlayerMeleeComboAttackState(this);

        //MovementSpeed = player.Data.PlayerConditionData.MovementSpeed;
    }

}