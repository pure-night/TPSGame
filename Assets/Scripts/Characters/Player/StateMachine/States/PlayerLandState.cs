using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        base.EnterState();

        StartAnimation(stateMachine.Player.AnimationData.LandParameterHash);

        if (stateMachine.IsRunning)
        {
            stateMachine.ChangeState(stateMachine.RunState);
        }
        else
            stateMachine.ChangeState(stateMachine.IdleState);
    }
}
