using UnityEngine.InputSystem;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        stateMachine.MovementSpeedModifier = GroundData.RunSpeedModifier;
        base.EnterState();
        SetAnimationFloat(stateMachine.Player.AnimationData.SpeedParameterHash, 2);
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        base.OnRunCanceled(context);
        stateMachine.ChangeState(stateMachine.WalkState);
    }
}