using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public enum GameState
{
    Playing,
    Crashed,
    MainMenu,
    Paused,
    Victory,
    Menu
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

    public IEnumerator Victory(float x, float final, float timer, float torque = 200)
    {
        yield return new WaitForSeconds(x);
        GameManager.instance.SetGameState(GameState.Menu);
        Transform go = GameObject.FindGameObjectWithTag("GeneralMenu").transform;
        go.GetChild(0).gameObject.SetActive(true);
        go.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "GANASTE";
        go.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "CONTINUAR";
        go.GetChild(0).GetChild(2).gameObject.SetActive(true);
        EndLevelCoins.instance.GenerateCoinsEndLevel(final, torque, timer);
        go.GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "OBJETOS: $" + EndLevelCoins.instance.objectCoins;
        go.GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "TIEMPO: $" + EndLevelCoins.instance.timerCoins;
        go.GetChild(0).GetChild(2).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = "TOTAL: $" + EndLevelCoins.instance.levelCoins;
    }

    public IEnumerator Defeat(float time){
        yield return new WaitForSeconds(time);
        GameManager.instance.SetGameState(GameState.Menu);
        Transform go = GameObject.FindGameObjectWithTag("GeneralMenu").transform;
        go.GetChild(0).gameObject.SetActive(true);
        go.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "PERDISTE";
        go.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "REINICIAR";
        go.GetChild(0).GetChild(2).gameObject.SetActive(false);
    }
}
