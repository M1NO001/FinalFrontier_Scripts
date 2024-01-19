using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderingState : EnemyGroundedState
{
    public EnemyWanderingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Enemy.Agent.speed = stateMachine.Enemy.Data.WalkSpeed;
        stateMachine.Enemy.Agent.isStopped = false;
        base.Enter();
        if (stateMachine.Enemy.Data.Type == EnemyType.MiddleLevel)
        {
            InMiddleLevelMonster();
            return;
        }
        stateMachine.Enemy.Agent.SetDestination(GetWanderLocation());
        StartAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);

    }

    public override void Exit()
    {
        base.Exit();
        if (stateMachine.Enemy.Data.Type == EnemyType.MiddleLevel)
        {
            return;
        }
        StopAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if(stateMachine.Enemy.Agent.remainingDistance < stateMachine.Enemy.Agent.stoppingDistance + 0.1f)
        {
            stateMachine.Enemy.Agent.SetDestination(GetWanderLocation());
        }
        if (playerDistance <= stateMachine.Enemy.Data.ChasingRange * stateMachine.Enemy.Data.ChasingRange)
        {
            stateMachine.ChangeState(stateMachine.ChasingState); 
            return;
        }
    }

    private void InMiddleLevelMonster()
    {
        stateMachine.ChangeState(stateMachine.ChasingState);
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(stateMachine.Enemy.transform.position + (Random.onUnitSphere * Random.Range(stateMachine.Enemy.Data.minWanderDistance, stateMachine.Enemy.Data.maxWanderDistance)), out hit, stateMachine.Enemy.Data.maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(stateMachine.Enemy.transform.position, hit.position) < stateMachine.Enemy.Data.ChasingRange)
        {
            NavMesh.SamplePosition(stateMachine.Enemy.transform.position + (Random.onUnitSphere * Random.Range(stateMachine.Enemy.Data.minWanderDistance, stateMachine.Enemy.Data.maxWanderDistance)), out hit, stateMachine.Enemy.Data.maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }
}
