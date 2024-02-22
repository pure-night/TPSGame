public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        base.EnterState();

        StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void ExitState()
    {
        base.ExitState();

        StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsGround)
        {
            stateMachine.ChangeState(stateMachine.LandState);
            return;
        }
    }
}
