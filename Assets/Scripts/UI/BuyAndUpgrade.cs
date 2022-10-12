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
    public List<VehicleConfig> scriptVeh = new List<VehicleConfig>();

    private VehicleConfig selectedVehicle;
    private GameObject vehicleToBuy;
    private int vehiclePrice;
    private GameObject upgradeToBuy;
    private int upgradePrice;
    
    void Start()
    {
        
    }

    void Update()
    {
        
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
            selectedVehicle = scriptVeh[go.transform.GetSiblingIndex()];
            sliderSpeed.value = selectedVehicle.originalSpeed;
            sliderEndurance.value = selectedVehicle.originalEndurance;
            sliderHandling.value = selectedVehicle.originalHandling;

            upgrades.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedVehicle.priceUpgradeSpeed.ToString();
            upgrades.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedVehicle.priceUpgradeEndurance.ToString();
            upgrades.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedVehicle.priceUpgradeHandLing.ToString();

            if (int.Parse(go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text) == 0)
            {
                foreach (Transform obj in up)
                {
                    obj.gameObject.GetComponent<Toggle>().interactable = true;
                }
            }
            else
            {
                foreach (Transform obj in up)
                {
                    obj.gameObject.GetComponent<Toggle>().interactable = false;
                }
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
        }
    }

    public void BuyThings()
    {
        Transform up = upgrades.transform;

        if (vehiclePrice > 0)
        {
            if (vehiclePrice <= EndLevelCoins.instance.totalCoins)
            {
                EndLevelCoins.instance.totalCoins -= vehiclePrice;
                vehicleToBuy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 0.ToString();
                textSt.GetComponent<TextMeshProUGUI>().text = EndLevelCoins.instance.totalCoins.ToString();
                foreach (Transform obj in up)
                {
                    obj.gameObject.GetComponent<Toggle>().interactable = true;
                }
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
                }
            }
        }
    }
}
