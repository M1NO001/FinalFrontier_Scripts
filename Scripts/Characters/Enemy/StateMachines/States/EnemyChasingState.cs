using UnityEngine;


public class EnemyChasingState : EnemyGroundedState
{
    public EnemyChasingState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.Enemy.Agent.speed = stateMachine.Enemy.Data.RunSpeed;
        stateMachine.Enemy.Agent.isStopped = false;
        stateMachine.Enemy.IsChasing = true;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.RunParameterHash);
        stateMachine.Enemy.StartSetDestinationCoroutine();
    }

    public override void Exit()
    {
        stateMachine.Enemy.IsChasing = false;
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.RunParameterHash);
        stateMachine.Enemy.StopSetDestinationCoroutine();
    }

    public override void Update()
    {
        base.Update(); 
        if (playerDistance > stateMachine.Enemy.Data.ChasingRange * stateMachine.Enemy.Data.ChasingRange && stateMachine.Enemy.Data.Type == EnemyType.LowLevel)
        {
            stateMachine.ChangeState(stateMachine.WanderingState);
            return;
        }
    }
}
