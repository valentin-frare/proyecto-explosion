using UnityEngine;

public class PlayerVehicleMovement : BaseVehicleMovement
{
    public PlayerVehicleMovement(Rigidbody rigidbody, VehicleWheels vehicleWheels, VehicleConfig vehicleConfig) : base(rigidbody, vehicleWheels, vehicleConfig)
    {
    }

    public override void Idle()
    {
        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.motorTorque = -vehicleConfig.torque / 4;
        }
    }

    public override void SteerToCenter()
    {
        if (rigidbody.gameObject.transform.rotation.y > 0.001f) 
        {
            base.SetSteeringAngle(rigidbody.gameObject.transform.rotation.y * -40f);
        }

        if (rigidbody.gameObject.transform.rotation.y < -0.001f) 
        {
            base.SetSteeringAngle(rigidbody.gameObject.transform.rotation.y * -40f);
        }
    }
}