using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public Action<GameState> OnGameStateChanged;

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

    public void SetGameState(GameState state)
    {
        gameState = state;

        OnGameStateChanged?.Invoke(state);
    }
}
