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
    [SerializeField]
    private Sprite[] spritesButtons;
    
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
                GameManager.instance.wonLevel = false;
                break;
            default:
                break;
        }
    }

    private void VehiclePrices()
    {
        int index = RespawnManager.instance.GetPlayer().GetComponent<VehicleController>().vehicleConfig.id;

        allMoney.GetComponent<TextMeshProUGUI>().text = "$ " + EndLevelCoins.instance.totalCoins;
        for (int i = 0; i < bau.trueScriptVeh.Count; i++)
        {
            grid.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = bau.trueScriptVeh[i].price.ToString();

            if (i == index)
            {
                grid.transform.GetChild(i).GetComponent<Toggle>().isOn = true;
                grid.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = spritesButtons[1];
                switchOff.allowSwitchOff = false;
            }

            if (bau.trueScriptVeh[i].price == 0)
            {
                grid.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = spritesButtons[0];
            }
            else
            {
                grid.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = spritesButtons[2];
            }
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
            float highestTorque = Mathf.Lerp(0, 600, bau.trueScriptVeh[index].upgradeSpeed);
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
            GameManager.instance.addEndurance = 0;
        }

        if (bau.trueScriptVeh[index].originalHandling == bau.trueScriptVeh[index].upgradeHandling)
        {
            float highestHandling = Mathf.Lerp(40, 150, bau.trueScriptVeh[index].upgradeHandling);
            float percentageHandling = bau.trueScriptVeh[index].originalSteeringSpeed / highestHandling;
            GameManager.instance.multiplyHandling = percentageHandling;
        }
        else
        {
            GameManager.instance.multiplyHandling = 1f;
        }
    }
}