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
    [SerializeField]
    private GameObject allMoney;
    
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
                GameManager.instance.level = GameManager.instance.NextLevel();
                VehiclePrices();
                break;
            case GameState.Victory:
                ModifyPlayer();
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
        int index = RespawnManager.instance.GetPlayer().GetComponent<VehicleController>().vehicleConfig.id;

        grid.transform.GetChild(index).GetComponent<Toggle>().isOn = true;
        switchOff.allowSwitchOff = false;

        allMoney.GetComponent<TextMeshProUGUI>().text = "$ " + EndLevelCoins.instance.totalCoins;
        for (int i = 0; i < bau.trueScriptVeh.Count; i++)
        {
            grid.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = bau.trueScriptVeh[i].price.ToString();
        }
    }

    private void ModifyPlayer()
    {
        int index = 0;

        for (int i = 0; i < 9; i++)
        {
            if (grid.transform.GetChild(i).GetComponent<Toggle>().isOn == true)
            {
                index = i;
                break;
            }
        }

        RespawnManager.instance.ChangePlayerPrefab(index);
        
        if (bau.trueScriptVeh[index].originalSpeed == bau.trueScriptVeh[index].upgradeSpeed)
        {
            float highestTorque = Mathf.Lerp(0, 2000, bau.trueScriptVeh[index].upgradeSpeed);
            float percentageTorque = bau.trueScriptVeh[index].originalTorque / highestTorque;
            GameManager.instance.multiplyTorque = percentageTorque;
        }
        else
        {
            GameManager.instance.multiplyTorque = 1f;
        }

        if (bau.trueScriptVeh[index].originalEndurance == bau.trueScriptVeh[index].upgradeEndurance)
        {
            float highestEndurance = Mathf.Lerp(0, 50, bau.trueScriptVeh[index].upgradeEndurance);
            GameManager.instance.addEndurance = Mathf.CeilToInt(highestEndurance) - bau.trueScriptVeh[index].theRealOriginalEndurance;
        }
        else
        {
            GameManager.instance.multiplyTorque = 1f;
        }
    }
}