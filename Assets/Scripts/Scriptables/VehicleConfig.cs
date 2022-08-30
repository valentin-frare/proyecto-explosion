using UnityEngine;

[CreateAssetMenu(fileName = "VehicleConfig", menuName = "Trinity/VehicleConfig", order = 0)]
public class VehicleConfig : ScriptableObject {
    public float torque;
    public float torqueReverse;
    public float brakeForce;
    public float steeringSpeed;
}