using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Stack<UI_Popup> _popupStack;
    private int _order = 10;

    public GameObject Root { get; private set; }

    private void Awake()
    {
        _popupStack = new Stack<UI_Popup>();
        
        Root = GameObject.Find("@UI_Root");
        if (Root == null)
            Root = new GameObject { name = "@UI_Root" };
    }
    
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Resources.Load<GameObject>($"Prefabs/UI/{name}");
        GameObject instantiatedGo = Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity, Root.transform);
        instantiatedGo.name = instantiatedGo.name.Replace("(Clone)", "").Trim();

        T sceneUI = instantiatedGo.GetOrAddComponent<T>();

        return sceneUI;
    }
}
