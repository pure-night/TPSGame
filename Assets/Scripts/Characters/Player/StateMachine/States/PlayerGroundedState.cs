using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void ExitState()
    {
        base.ExitState();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!IsGround)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }
    
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if(stateMachine.MovementInput == Vector2.zero)
            return;
        
        stateMachine.ChangeState(stateMachine.IdleState);
        
        base.OnMovementCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        if(IsGround)
            stateMachine.ChangeState(stateMachine.JumpState);
    }

    protected virtual void OnMove()
    {
        if (stateMachine.MovementInput == Vector2.zero) return;
        
        if(stateMachine.IsRunning)
            stateMachine.ChangeState(stateMachine.RunState);
        else
            stateMachine.ChangeState(stateMachine.WalkState);
    }
}
