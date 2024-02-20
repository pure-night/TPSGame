using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // 모든 오브젝트를 동적할당 하고, GenericSingleton을 사용하는 스크립트는
                // 오브젝트가 별로 없는 씬 처음에 생성되어 FindObjectOfType을 사용해도 괜찮음.
                _instance = FindObjectOfType<T>();
                
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }

            return _instance;
        }
    }
}
