public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyAppliedDealing;

    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Enemy.IsAttacking = true;
        alreadyAppliedDealing = false;
        base.Enter();
        if (stateMachine.Enemy.Data.Type != EnemyType.Boss)
        {
            stateMachine.Enemy.Agent.isStopped = true;
            StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
            StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (stateMachine.Enemy.Data.Type != EnemyType.Boss)
        {
            stateMachine.Enemy.Agent.isStopped = false;
            StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
            StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
        }
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Enemy.Data.Type != EnemyType.Boss)
        {
            if (playerDistance > stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange)
            {
                stateMachine.Enemy.IsAttacking = false;
                stateMachine.ChangeState(stateMachine.ChasingState);
            }

            float normalizedTime = stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (normalizedTime < 1f)
            {
                if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
                {
                    stateMachine.Target.TakePhysicalDamage(stateMachine.Enemy.Data.Damage);
                    alreadyAppliedDealing = true;
                }

            }
            else
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
        }
    }
}