using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public UIManager UIManager { get; private set; }
    public SceneLoader SceneLoader { get; private set; } 
    
    // 게임 시작시 실행을 위한 코드
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Execute()
    {
        // GameManager의 Instance초기화를 시켜주며 Initialize로 다른 요소 초기화
        Instance.Initialize();
    }

    private void Initialize()
    {
        // UIManager 할당 or 생성
        UIManager = TryGetComponent<UIManager>(out var uiManager) ?
            uiManager : gameObject.AddComponent<UIManager>();
        // UIManager의 Instance와 요소 초기화
        UIManager.Instance.Initialize();
        
        // SceneLoader 할당 or 생성
        SceneLoader = TryGetComponent<SceneLoader>(out var sceneLoader) ?
            sceneLoader : gameObject.AddComponent<SceneLoader>();
    }
}
