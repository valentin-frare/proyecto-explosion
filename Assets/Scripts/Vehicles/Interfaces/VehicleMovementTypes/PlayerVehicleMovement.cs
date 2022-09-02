using UnityEngine;

public class PlayerVehicleMovement : BaseVehicleMovement
{
    public PlayerVehicleMovement(Rigidbody rigidbody, VehicleWheels vehicleWheels, VehicleConfig vehicleConfig) : base(rigidbody, vehicleWheels, vehicleConfig)
    {
    }

    public override void Idle()
    {
        SetMotorTorque(-vehicleConfig.torque / 4);
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

    public override void SetMotorTorque(float torque)
    {
        if (vehicleWheels.backWheels[0].collider.rpm < -vehicleConfig.maxRpm)
        {
            base.SetMotorTorque(0f);
            return;
        }

        if (torque < 0)
            base.SetMotorTorque(torque);
    }
}