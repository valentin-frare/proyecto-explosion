using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyAndUpgrade : MonoBehaviour
{
    [SerializeField]
    private GameObject textSt;
    [SerializeField]
    private GameObject vehicles;
    [SerializeField]
    private GameObject upgrades;
    [SerializeField]
    private Slider sliderSpeed;
    [SerializeField]
    private Slider sliderEndurance;
    [SerializeField]
    private Slider sliderHandling;
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private Button exitButton;
    public List<VehicleConfig> scriptVeh = new List<VehicleConfig>();
    public List<VehicleNonScriptable> trueScriptVeh = new List<VehicleNonScriptable>();
    public Sprite[] spritesButtons;
    private VehicleNonScriptable selectedVehicle;
    private GameObject vehicleToBuy;
    private int vehiclePrice;
    private GameObject upgradeToBuy;
    private int upgradePrice;
    
    void Start()
    {
        for (int i = 0; i < scriptVeh.Count; i++)
        {
            VehicleConfig vc = scriptVeh[i];
            trueScriptVeh.Add(new VehicleNonScriptable(vc.price, vc.originalSpeed, vc.originalEndurance, vc.originalHandling, vc.upgradeSpeed, vc.upgradeEndurance, vc.upgradeHandling, vc.priceUpgradeSpeed, vc.priceUpgradeEndurance, vc.priceUpgradeHandLing, vc.id, vc.torque, vc.endurance, vc.steeringSpeed));
        }
    }

    public void SelectVehicle(GameObject go)
    {
        if (GameManager.instance.gameState == GameState.Victory && go.GetComponent<Toggle>().isOn == true)
        {
            vehicleToBuy = go;
            vehiclePrice = int.Parse(go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
            Transform up = upgrades.transform;
            foreach (Transform obj in up)
            {
                obj.gameObject.GetComponent<Toggle>().isOn = false;
            }
            selectedVehicle = trueScriptVeh[go.transform.GetSiblingIndex()];
            sliderSpeed.value = selectedVehicle.originalSpeed;
            sliderEndurance.value = selectedVehicle.originalEndurance;
            sliderHandling.value = selectedVehicle.originalHandling;

            upgrades.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedVehicle.priceUpgradeSpeed.ToString();
            upgrades.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedVehicle.priceUpgradeEndurance.ToString();
            upgrades.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedVehicle.priceUpgradeHandLing.ToString();

            if (int.Parse(go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text) == 0)
            {
                exitButton.interactable = true;
                go.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = spritesButtons[0];
                go.transform.GetChild(0).GetComponent<Image>().sprite = spritesButtons[1];
                foreach (Transform obj in up)
                {
                    if (int.Parse(obj.GetChild(1).GetComponent<TextMeshProUGUI>().text) > 0)
                    {
                        obj.gameObject.GetComponent<Toggle>().interactable = true;
                    }
                    else
                    {
                        obj.gameObject.GetComponent<Toggle>().interactable = false;
                    }
                }
            }
            else
            {
                go.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = spritesButtons[2];
                go.transform.GetChild(0).GetComponent<Image>().sprite = spritesButtons[3];
                exitButton.interactable = false;
                foreach (Transform obj in up)
                {
                    obj.gameObject.GetComponent<Toggle>().interactable = false;
                }
            }

            if (int.Parse(go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text) <= EndLevelCoins.instance.totalCoins && int.Parse(go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text) > 0)
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }

    public void SwitchOnOff(GameObject go)
    {
        if (GameManager.instance.gameState == GameState.Victory && go.GetComponent<Toggle>().isOn == true)
        {
            upgradeToBuy = go;
            upgradePrice = int.Parse(go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
            switch (go.name)
            {
                case "U0":
                    sliderSpeed.value = selectedVehicle.upgradeSpeed;
                    break;
                case "U1":
                    sliderEndurance.value = selectedVehicle.upgradeEndurance;
                    break;
                case "U2":
                    sliderHandling.value = selectedVehicle.upgradeHandling;
                    break;
                default:
                    break;
            }
            if (int.Parse(go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text) <= EndLevelCoins.instance.totalCoins)
            {
                buyButton.interactable = true;
            }
        }

        if (GameManager.instance.gameState == GameState.Victory && go.GetComponent<Toggle>().isOn == false)
        {
            switch (go.name)
            {
                case "U0":
                    sliderSpeed.value = selectedVehicle.originalSpeed;
                    break;
                case "U1":
                    sliderEndurance.value = selectedVehicle.originalEndurance;
                    break;
                case "U2":
                    sliderHandling.value = selectedVehicle.originalHandling;
                    break;
                default:
                    break;
            }
            if (go == upgradeToBuy)
            {
                buyButton.interactable = false;
            }
        }
    }

    public void BuyThings()
    {
        Transform up = upgrades.transform;
        int index = vehicleToBuy.transform.GetSiblingIndex();

        if (vehiclePrice > 0)
        {
            if (vehiclePrice <= EndLevelCoins.instance.totalCoins)
            {
                EndLevelCoins.instance.totalCoins -= vehiclePrice;
                vehicleToBuy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 0.ToString();
                vehicleToBuy.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = spritesButtons[0];
                vehicleToBuy.transform.GetChild(0).GetComponent<Image>().sprite = spritesButtons[1];
                textSt.GetComponent<TextMeshProUGUI>().text = EndLevelCoins.instance.totalCoins.ToString();
                trueScriptVeh[index].price = 0;
                vehiclePrice = 0;
                exitButton.interactable = true;
                foreach (Transform obj in up)
                {
                    obj.gameObject.GetComponent<Toggle>().interactable = true;
                }
                buyButton.interactable = false;
            }
        }
        else
        {
            int x = 0;
            foreach (Transform obj in up)
            {
                if (obj.gameObject.GetComponent<Toggle>().isOn == true)
                {
                    x++;
                }
            }
            if (x > 0)
            {
                if (upgradePrice <= EndLevelCoins.instance.totalCoins)
                {
                    EndLevelCoins.instance.totalCoins -= upgradePrice;
                    upgradeToBuy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 0.ToString();
                    textSt.GetComponent<TextMeshProUGUI>().text = EndLevelCoins.instance.totalCoins.ToString();
                    upgradeToBuy.GetComponent<Toggle>().interactable = false;

                    switch (upgradeToBuy.name)
                    {
                        case "U0":
                            trueScriptVeh[index].priceUpgradeSpeed = 0;
                            trueScriptVeh[index].originalSpeed = trueScriptVeh[index].upgradeSpeed;
                            break;
                        case "U1":
                            trueScriptVeh[index].priceUpgradeEndurance = 0;
                            trueScriptVeh[index].originalEndurance = trueScriptVeh[index].upgradeEndurance;
                            break;
                        case "U2":
                            trueScriptVeh[index].priceUpgradeHandLing = 0;
                            trueScriptVeh[index].originalHandling = trueScriptVeh[index].upgradeHandling;
                            break;
                        default:
                            break;
                    }
                    buyButton.interactable = false;
                }
            }
        }
    }
}
