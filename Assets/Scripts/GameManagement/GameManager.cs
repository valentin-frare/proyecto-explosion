using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake() {
        GameEvents.OnGameStart += OnGameStart;
        GameEvents.OnGameEnd += OnGameEnd;
    }

    private void OnGameStart()
    {
        RespawnManager.instance.SpawnPlayer();
    }

    private void OnGameEnd()
    {

    }
}
