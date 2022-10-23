using UnityEngine;

[CreateAssetMenu(fileName = "VehicleConfig", menuName = "Trinity/VehicleConfig", order = 0)]
public class VehicleConfig : ScriptableObject {
    public float drag;

    [Header("Basic Settings")]
    public int id;
    public float torque;
    public int endurance;
    public float torqueReverse;
    public float brakeForce;
    public float steeringSpeed;
    public float maxSteeringAngle;
    public float maxRpm;

    [Header("Advanced Settings")]
    public LayerMask groundLayer;
    public AnimationCurve steeringCurve;

    [Header("Debug Settings")]
    public Vector3 defaultDirection;

    [Header("Shop Settings")]
    public float price;
    public float originalSpeed;
    public float originalEndurance;
    public float originalHandling;

    [Header("Shop Upgrades Measurements")]
    public float upgradeSpeed;
    public float upgradeEndurance;
    public float upgradeHandling;

    [Header("Shop Upgrades Prices")]
    
    public float priceUpgradeSpeed;
    public float priceUpgradeEndurance;
    public float priceUpgradeHandLing;
}