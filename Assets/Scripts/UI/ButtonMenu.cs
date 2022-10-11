using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField]
    private Toggle vehicleOne;
    [SerializeField]
    private ToggleGroup switchOff;
    
    public void SpawnAgain()
    {
        switch (GameManager.instance.gameState)
        {
            case GameState.Crashed:
                EndLevelCoins.instance.RestartCoins();
                RespawnManager.instance.SpawnPlayer();
                GameManager.instance.SetGameState(GameState.Playing);
                break;
            case GameState.Playing:
                Transform go = GameObject.FindGameObjectWithTag("ServiceStationMenu").transform;
                go.GetChild(0).gameObject.SetActive(true);
                GameManager.instance.SetGameState(GameState.Victory);
                vehicleOne.isOn = true;
                switchOff.allowSwitchOff = false;
                break;
            case GameState.Victory:
                EndLevelCoins.instance.RestartCoins();
                RespawnManager.instance.DeleteAllPlayers();
                RespawnManager.instance.SpawnPlayer();
                GameManager.instance.SetGameState(GameState.Playing);
                break;
            default:
            break;
        }
    }
}
