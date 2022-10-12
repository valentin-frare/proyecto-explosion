using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleNonScriptable
{
    public float price;
    public float originalSpeed;
    public float originalEndurance;
    public float originalHandling;
    public float upgradeSpeed;
    public float upgradeEndurance;
    public float upgradeHandling;
    public float priceUpgradeSpeed;
    public float priceUpgradeEndurance;
    public float priceUpgradeHandLing;

    public VehicleNonScriptable(float price, float originalSpeed, float originalEndurance, float originalHandling, float upgradeSpeed, float upgradeEndurance, float upgradeHandling, float priceUpgradeSpeed, float priceUpgradeEndurance, float priceUpgradeHandLing)
    {
        this.price = price;
        this.originalSpeed = originalSpeed;
        this.originalEndurance = originalEndurance;
        this.originalHandling = originalHandling;
        this.upgradeSpeed = upgradeSpeed;
        this.upgradeEndurance = upgradeEndurance;
        this.upgradeHandling = upgradeHandling;
        this.priceUpgradeSpeed = priceUpgradeSpeed;
        this.priceUpgradeEndurance = priceUpgradeEndurance;
        this.priceUpgradeHandLing = priceUpgradeHandLing;
    }
    
}
