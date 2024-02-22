public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
        StartAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }

    public override void ExitState()
    {
        base.ExitState();
        StopAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }
}
