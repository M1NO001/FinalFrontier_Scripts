public class EnemyGroundedState : EnemyBaseState
{
    public EnemyGroundedState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(stateMachine.Enemy.Data.Type != EnemyType.Boss) StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        if (stateMachine.Enemy.Data.Type != EnemyType.Boss) StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (playerDistance <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange)
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            
            return;
        }
    }
}
