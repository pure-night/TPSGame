using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData GroundData;

    protected bool IsGround { get; set; }

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        GroundData = stateMachine.Player.Data.GroundedData;
    }

    public virtual void EnterState()
    {
        AddInputActionsCallbacks();
    }

    public virtual void ExitState()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    public void AnimationTriggerEvent() { }

    public virtual void Update()
    {
        CheckIsGround();
    }
    
    protected void ForceMove() { }
    
    private void Move()
    {
        var movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        var forward = stateMachine.MainCameraTransform.forward;
        var right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x; 
    }

    private void Rotate(Vector3 movementDirection)
    {
        if(movementDirection != Vector3.zero)
        {
            var playerTransform = stateMachine.Player.Character;
            var targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation,
                stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private void Move(Vector3 movementDirection)
    {
        var movementSpeed = GetMovementSpeed();
        var direction = movementDirection * (movementSpeed * Time.fixedDeltaTime * 10f);
        direction.y = stateMachine.Player.Rigidbody.velocity.y;

        stateMachine.Player.Rigidbody.velocity = direction;
    }
    
    private float GetMovementSpeed()
    {
        var movementSpeed = stateMachine.MovementBaseSpeed + stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }
    
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected void SetAnimationFloat(int animationHash, float value)
    {
        stateMachine.Player.StartBlendAnimation(animationHash, value);
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;
        input.PlayerActions.Run.canceled += OnRunCanceled;

        stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;
        input.PlayerActions.Run.canceled -= OnRunCanceled;

        stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context) { }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        stateMachine.IsRunning = true;
    }

    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsRunning = false;
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context) { }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }
    
    private void CheckIsGround()
    {
        var playerTransform = stateMachine.Player.transform;
        
        IsGround = Physics.CheckSphere(playerTransform.position + new Vector3(0, 0.18f, 0), 0.2f, GroundData.GroundLayerMask);
    }
}
