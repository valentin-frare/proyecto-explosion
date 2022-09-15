using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Crashed,
    MainMenu,
    Paused
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    public GameState gameState;

    private void Awake() {
        instance = this;

        GameEvents.OnGameStart += OnGameStart;
        GameEvents.OnGameEnd += OnGameEnd;
    }

    private void OnGameStart()
    {
        RespawnManager.instance.SpawnPlayer();

        gameState = GameState.Playing;
    }

    private void OnGameEnd()
    {

    }
}
