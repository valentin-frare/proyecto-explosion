using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCaller : MonoBehaviour
{
    public void OnGameStart() => GameEvents.OnGameStart.Invoke();
    public void OnGameEnd() => GameEvents.OnGameEnd.Invoke();
}
