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
        float nonConstantMaxRpm = Mathf.Abs(Mathf.Clamp(torque / vehicleConfig.torque, -1f, -0.25f));

        //Debug.Log(vehicleWheels.backWheels[0].collider.rpm.ToString() + " / " + (-vehicleConfig.maxRpm * nonConstantMaxRpm).ToString());

        if (vehicleWheels.backWheels[0].collider.rpm < (-vehicleConfig.maxRpm * nonConstantMaxRpm))
        {
            base.SetBrakeForce(200f);
            base.SetMotorTorque(0f);
            return;
        }

        if (torque < 0)
            base.SetMotorTorque(torque);
    }
}