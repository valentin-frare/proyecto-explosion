using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenu : MonoBehaviour
{
    
    public void SpawnAgain()
    {
        switch (GameManager.instance.gameState)
        {
            case GameState.Crashed:
                RespawnManager.instance.SpawnPlayer();
                GameManager.instance.SetGameState(GameState.Playing);
                break;
            case GameState.Playing:
                //
                break;
            default:
            break;
        }
    }

}
