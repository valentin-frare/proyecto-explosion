using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField]
    private Toggle vehicleOne;
    [SerializeField]
    private ToggleGroup switchOff;
    [SerializeField]
    private GameObject grid;
    [SerializeField]
    private BuyAndUpgrade bau;
    
    public void SpawnAgain()
    {
        switch (GameManager.instance.gameState)
        {
            case GameState.Crashed:
                EndLevelCoins.instance.RestartCoins();
                RespawnManager.instance.SpawnPlayer();
                GameManager.instance.SetGameState(GameState.Playing);
                break;
            case GameState.Menu:
                Transform go = GameObject.FindGameObjectWithTag("ServiceStationMenu").transform;
                go.GetChild(0).gameObject.SetActive(true);
                GameManager.instance.SetGameState(GameState.Victory);
                vehicleOne.isOn = true;
                switchOff.allowSwitchOff = false;
                VehiclePrices();
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

    private void VehiclePrices()
    {
        for (int i = 0; i < bau.scriptVeh.Count; i++)
        {
            //grid.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = (bau.scriptVeh[i].price > 0 ? (""+bau.scriptVeh[i].price) : "COGIDO");
            grid.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = bau.scriptVeh[i].price.ToString();
        }
    }
}