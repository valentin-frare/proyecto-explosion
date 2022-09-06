using UnityEngine;

public class EnemyVehicleMovement : BaseVehicleMovement
{
    //TODO: agregar el modo loco y el modo cuidadoso

    public EnemyVehicleMovement(Rigidbody rigidbody, VehicleWheels vehicleWheels, VehicleConfig vehicleConfig) : base(rigidbody, vehicleWheels, vehicleConfig)
    {
    }

    public override void Idle()
    {
        SetMotorTorque(-vehicleConfig.torque / 4);
    }

    public override void Accel()
    {
        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.brakeTorque = 0f;
        }

        base.Accel();
    }

    public override void Brake()
    {
        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.motorTorque = 0f;
        }

        base.Brake();
    }

    public override void SteerToCenter()
    {
        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.brakeTorque = 0f;
        }

        if (base.GetSteeringAngle() == 0f) return;

        if ((rigidbody.gameObject.transform.rotation.y * Mathf.Rad2Deg) < 0.005f && (rigidbody.gameObject.transform.rotation.y * Mathf.Rad2Deg) > -0.005f)
        {
            base.SetSteeringAngle(0f);
        }

        if ((rigidbody.gameObject.transform.rotation.y * Mathf.Rad2Deg) > 0.001f ) 
        {
            base.SetSteeringAngle(-(rigidbody.gameObject.transform.rotation.y * Mathf.Rad2Deg));
        }

        if ((rigidbody.gameObject.transform.rotation.y * Mathf.Rad2Deg) < -0.001f) 
        {
            base.SetSteeringAngle(-(rigidbody.gameObject.transform.rotation.y * Mathf.Rad2Deg));
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

    public override void SetSteeringAngle(float angle)
    {
        base.SetSteeringAngle(Mathf.Clamp(angle, -30f, 30f));
    }
}