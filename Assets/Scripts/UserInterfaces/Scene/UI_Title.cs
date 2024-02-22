using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Title : UI_Scene
{
    private enum Buttons
    {
        StartBtn,
    }
    
    protected override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartBtn).onClick.AddListener(() => 
        { 
            Managers.SceneLoader.LoadScene(Scenes.Game);
        });
    }
}
