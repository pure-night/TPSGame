using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public Stage StageInfo { get; private set; }

    public void InitializeGame()
    {
        var map = Resources.Load<GameObject>("Prefabs/Stage/Map");
        Instantiate(map);
        StageInfo = map.GetComponent<Stage>();

        var player = Resources.Load<GameObject>("Prefabs/Character/Player");
        Instantiate(player, StageInfo.PlayerSpawnPoint);
    }
}
