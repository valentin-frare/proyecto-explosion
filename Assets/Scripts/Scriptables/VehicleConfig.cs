using UnityEngine;

[CreateAssetMenu(fileName = "VehicleConfig", menuName = "Trinity/VehicleConfig", order = 0)]
public class VehicleConfig : ScriptableObject {
    public float drag;

    [Header("Basic Settings")]
    public float torque;
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
}