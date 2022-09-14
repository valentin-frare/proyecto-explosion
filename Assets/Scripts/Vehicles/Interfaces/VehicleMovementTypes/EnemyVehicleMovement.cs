using UnityEngine;

public class EnemyVehicleMovement : BaseVehicleMovement
{
    public EnemyVehicleMovement(Transform transform, Rigidbody sphereMotor, VehicleWheels vehicleWheels, VehicleConfig vehicleConfig, float alignToGroundTime = 20) : base(transform, sphereMotor, vehicleWheels, vehicleConfig, alignToGroundTime)
    {
    }
}