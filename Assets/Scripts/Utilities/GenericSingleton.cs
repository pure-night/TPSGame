using System;
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
                // GenericSingleton을 사용하는 스크립트는 동적할당을 하기 전인 오브젝트가 없는 씬의
                // 처음에 생성되어 FindObjectOfType을 사용해도 괜찮음.
                // 처음에 Instance 초기화 해줄 것.
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
