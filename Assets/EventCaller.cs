using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCaller : MonoBehaviour
{
    public void OnGameStart()
    {
        GameManager.instance.SetGameState(GameState.Playing);
        GameEvents.OnGameStart.Invoke();
    }
    public void OnGameEnd() => GameEvents.OnGameEnd.Invoke();
}
