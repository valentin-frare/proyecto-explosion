

using UnityEngine;

public class BaseVehicleMovement : IVehicleMovement
{   
    protected VehicleConfig vehicleConfig;
    protected VehicleWheels vehicleWheels;

    public BaseVehicleMovement(VehicleWheels vehicleWheels, VehicleConfig vehicleConfig)
    {
        this.vehicleWheels = vehicleWheels;
        this.vehicleConfig = vehicleConfig;
    }

    public virtual void Update()
    {
        vehicleWheels.frontLeftWheel.localEulerAngles = new Vector3(vehicleWheels.frontLeftWheel.localEulerAngles.x, (vehicleWheels.frontLeftWheelCol.steerAngle - vehicleWheels.frontLeftWheel.localEulerAngles.z) - 90f, vehicleWheels.frontLeftWheel.localEulerAngles.z);
        vehicleWheels.frontRightWheel.localEulerAngles = new Vector3(vehicleWheels.frontRightWheel.localEulerAngles.x, (vehicleWheels.frontRightWheelCol.steerAngle - vehicleWheels.frontRightWheel.localEulerAngles.z) - 90f, vehicleWheels.frontRightWheel.localEulerAngles.z);

        vehicleWheels.frontLeftWheel.transform.Rotate((Vector3.up * vehicleWheels.frontLeftWheelCol.rpm) * Time.fixedDeltaTime); 
        vehicleWheels.frontRightWheel.transform.Rotate((Vector3.up * vehicleWheels.frontRightWheelCol.rpm) * Time.fixedDeltaTime); 

        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.obj.transform.Rotate((Vector3.up * wheel.collider.rpm) * Time.fixedDeltaTime);
        }
    }

    public virtual void Accel()
    {
        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.motorTorque = -vehicleConfig.torque;
        }
    }

    public virtual void Brake()
    {
        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.brakeTorque = vehicleConfig.brakeForce;
        }
    }

    public virtual void Reverse()
    {
        throw new System.NotImplementedException();
    }

    public virtual void SteerLeft()
    {
        if (vehicleWheels.frontLeftWheelCol.steerAngle > -30f)
        {
            vehicleWheels.frontLeftWheelCol.steerAngle -= vehicleConfig.steeringSpeed;
        }

        if (vehicleWheels.frontLeftWheelCol.steerAngle > -30f)
        {
            vehicleWheels.frontRightWheelCol.steerAngle -= vehicleConfig.steeringSpeed;
        }
    }

    public virtual void SteerRight()
    {
        if (vehicleWheels.frontLeftWheelCol.steerAngle < 30f)
        {
            vehicleWheels.frontLeftWheelCol.steerAngle += vehicleConfig.steeringSpeed;
        }

        if (vehicleWheels.frontLeftWheelCol.steerAngle < 30f)
        {
            vehicleWheels.frontRightWheelCol.steerAngle += vehicleConfig.steeringSpeed;
        }
    }

    public virtual void Idle()
    {
        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.motorTorque = 0f;
        }

        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.brakeTorque = 0f;
        }
    }

    public virtual void SteerToCenter()
    {
        vehicleWheels.frontLeftWheelCol.steerAngle += (vehicleWheels.frontLeftWheelCol.steerAngle > 0) ? -vehicleConfig.steeringSpeed : vehicleConfig.steeringSpeed;

        vehicleWheels.frontRightWheelCol.steerAngle += (vehicleWheels.frontRightWheelCol.steerAngle > 0) ? -vehicleConfig.steeringSpeed : vehicleConfig.steeringSpeed;
    }
}