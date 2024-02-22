using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers Instance { get { Initialize(); return _instance; } }

    private GameManager _gameManager;
    private GameSceneManager _gameSceneManager;
    private UIManager _uiManager;
    private SceneLoader _sceneLoader;

    public static GameManager GameManager => Instance._gameManager;
    public static GameSceneManager GameSceneManager => Instance._gameSceneManager;
    public static UIManager UIManager => Instance._uiManager;
    public static SceneLoader SceneLoader => Instance._sceneLoader;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        Initialize();
    }
    
    private static void Initialize()
    {
        if(_instance == null)
        {
            var go = GameObject.Find("@Managers");

            if(go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }
            
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();

            if (!go.TryGetComponent(out _instance._gameManager))
                _instance._gameManager = go.AddComponent<GameManager>();

            if (!go.TryGetComponent(out _instance._gameSceneManager))
                _instance._gameSceneManager = go.AddComponent<GameSceneManager>();
            
            if (!go.TryGetComponent(out _instance._uiManager))
                _instance._uiManager = go.AddComponent<UIManager>();

            if (!go.TryGetComponent(out _instance._sceneLoader))
                _instance._sceneLoader = go.AddComponent<SceneLoader>();
        }
    }
}
