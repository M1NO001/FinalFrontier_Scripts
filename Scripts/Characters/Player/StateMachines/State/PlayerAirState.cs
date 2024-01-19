using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //StartAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        //StopAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (player._playerModifier.MovementStateModifier > player._playerModifier.AirSpeedModifier)
        {
            player._playerModifier.MovementStateModifier -= 1f * Time.deltaTime;
        }
        else if (player._playerModifier.MovementStateModifier < player._playerModifier.AirSpeedModifier)
        {
            player._playerModifier.MovementStateModifier += 1f * Time.deltaTime;
        }
    }
}
