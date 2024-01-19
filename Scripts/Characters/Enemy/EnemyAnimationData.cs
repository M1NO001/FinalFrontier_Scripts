using System;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";

    [SerializeField] private string airParameterName = "@Air";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string baseAttackParameterName = "BaseAttack";


    public int GroundParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int BaseAttackParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);

        AirParameterHash = Animator.StringToHash(airParameterName);

        AttackParameterHash = Animator.StringToHash(attackParameterName);
        BaseAttackParameterHash = Animator.StringToHash(baseAttackParameterName);
    }
}