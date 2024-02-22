using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("Animations")] [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    [field: Header("References")] [field: SerializeField] public PlayerSO Data { get; private set; }
    [field: SerializeField] public Transform Character { get; private set; }
    
    public Rigidbody Rigidbody { get; private set; }
    public PlayerInput Input { get; private set; }
    public Animator Animator { get; private set; }
    public DirectionEnum Direction { get; set; }

    public CharacterHealth CharacterHealth { get; private set; }
    
    private PlayerStateMachine _stateMachine;

    private Coroutine _currentCoroutine;
    private WaitForFixedUpdate _waitForFixedUpdate;
    
    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        CharacterHealth = GetComponent<CharacterHealth>();
        Rigidbody = GetComponent<Rigidbody>();
        Direction = DirectionEnum.Forward;
        
        _stateMachine = new PlayerStateMachine(this);
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _stateMachine.ChangeState(_stateMachine.IdleState);

        CharacterHealth.OnDie += OnDie;
    }
    
    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

    private void OnDie()
    {
        Animator.SetTrigger(AnimationData.DieParameterHash);
        enabled = false;
    }

    // 코루틴이 MonoBehaviour이 필요해서 여기에서 구현.
    public void StartBlendAnimation(int animationHash, float value)
    {
        if(_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        
        _currentCoroutine = StartCoroutine(BlendAnimationCoroutine(animationHash, value));
    }

    // 부드러운 애니메이션 전환을 위한 코드
    private IEnumerator BlendAnimationCoroutine(int animationHash, float targetValue)
    {
        while (Mathf.Abs(Animator.GetFloat(animationHash) - targetValue) >= 0.005f)
        {
            Animator.SetFloat(animationHash, targetValue, 0.25f, Time.fixedDeltaTime);
            yield return _waitForFixedUpdate;
        }
        
        Animator.SetFloat(animationHash, targetValue);
    }
}
