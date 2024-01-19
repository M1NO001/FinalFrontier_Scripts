using System;
using UnityEngine;


[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "Grounded";


    [SerializeField] private string crouchParameterName = "Crouch";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string sprintParameterName = "Sprint";
    [SerializeField] private string speedParameterName = "Speed";

    [SerializeField] private string directionXName = "DirectionX";
    [SerializeField] private string directionYName = "DirectionY";

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";


    [SerializeField] private string meleeAttackParameterName = "MeleeAttack";
    [SerializeField] private string meleeComboAttackParameterName = "MeleeComboAttack";
    [SerializeField] private string aimParameterName = "Aim";
    [SerializeField] private string shootParameterName = "Shoot";
    [SerializeField] private string reloadParameterName = "Reload";
    [SerializeField] private string reloadSpeedParameterName = "ReloadSpeed";

    [SerializeField] private string dieParameterName = "Die";


    public int GroundParameterHash { get; private set; }
    public int CrouchParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int SprintParameterHash { get; private set; }
    public int SpeedParameterHash { get; private set; }

    public int DirectionX { get; private set; }
    public int DirectionY { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int fallParameterHash { get; private set; }

    public int MeleeAttackParameterHash { get; private set; }
    public int MeleeComboAttackParameterHash { get; private set; }
    public int AimParameterHash { get; private set; }
    public int ShootParameterHash { get; private set; }
    public int ReloadParameterHash { get; private set; }
    public int ReloadSpeedParameterHash { get; private set; }

    public int DieParameterHash { get; private set; }
    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        CrouchParameterHash = Animator.StringToHash(crouchParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        SprintParameterHash = Animator.StringToHash(sprintParameterName);
        SpeedParameterHash = Animator.StringToHash(speedParameterName);

        DirectionX = Animator.StringToHash(directionXName);
        DirectionY = Animator.StringToHash(directionYName);


        AirParameterHash = Animator.StringToHash(airParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        fallParameterHash = Animator.StringToHash(fallParameterName);


        MeleeAttackParameterHash = Animator.StringToHash(meleeAttackParameterName);
        MeleeComboAttackParameterHash = Animator.StringToHash(meleeComboAttackParameterName);
        AimParameterHash = Animator.StringToHash(aimParameterName);
        ShootParameterHash = Animator.StringToHash(shootParameterName);
        ReloadParameterHash = Animator.StringToHash(reloadParameterName);
        ReloadSpeedParameterHash = Animator.StringToHash(reloadSpeedParameterName);

        DieParameterHash = Animator.StringToHash(dieParameterName);
    }
}
