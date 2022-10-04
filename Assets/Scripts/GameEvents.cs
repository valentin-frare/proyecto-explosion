using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    // Game Loop Events
    public static Action OnGameStart;
    public static Action OnGameEnd;
    public static Action<GameObject> OnPlayerSpawn;

    // Collision Events
    public static Action<ContactPoint[], ColliderType> OnCarCollision;
}
