using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyAndUpgrade : MonoBehaviour
{
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
    private List<VehicleConfig> scriptVeh = new List<VehicleConfig>();

    private VehicleConfig selectedVehicle;
    
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
            Transform up = upgrades.transform;
            foreach (Transform obj in up)
            {
                obj.gameObject.GetComponent<Toggle>().isOn = false;
            }
            selectedVehicle = scriptVeh[go.transform.GetSiblingIndex()];
            sliderSpeed.value = selectedVehicle.originalSpeed;
            sliderEndurance.value = selectedVehicle.originalEndurance;
            sliderHandling.value = selectedVehicle.originalHandling;
        }
    }

    public void SwitchOnOff(GameObject go)
    {
        if (GameManager.instance.gameState == GameState.Victory && go.GetComponent<Toggle>().isOn == true)
        {
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
}
