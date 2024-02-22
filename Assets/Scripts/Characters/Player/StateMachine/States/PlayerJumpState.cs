using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        stateMachine.JumpForce = stateMachine.Player.Data.AirData.JumpForce;
        
        stateMachine.Player.Rigidbody.AddForce(Vector3.up * stateMachine.JumpForce, ForceMode.Impulse);

        base.EnterState();
        StartAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void ExitState()
    {
        base.ExitState();

        StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (stateMachine.Player.Rigidbody.velocity.y <= 0)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }
}
