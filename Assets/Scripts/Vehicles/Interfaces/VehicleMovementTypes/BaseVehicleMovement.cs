

using UnityEngine;

public class BaseVehicleMovement : IVehicleMovement
{   
    protected VehicleConfig vehicleConfig;
    protected VehicleWheels vehicleWheels;
    protected Rigidbody rigidbody;

    public BaseVehicleMovement(Rigidbody rigidbody, VehicleWheels vehicleWheels, VehicleConfig vehicleConfig)
    {
        this.rigidbody = rigidbody;
        this.vehicleWheels = vehicleWheels;
        this.vehicleConfig = vehicleConfig;

        Debug.Log(vehicleWheels.frontLeftWheel.rotation.z);
    }

    public virtual void Update()
    {
        vehicleWheels.frontLeftWheelAxis.localEulerAngles = new Vector3(vehicleWheels.frontLeftWheelAxis.localEulerAngles.x, vehicleWheels.frontLeftWheelCol.steerAngle, vehicleWheels.frontLeftWheelAxis.localEulerAngles.z);
        vehicleWheels.frontRightWheelAxis.localEulerAngles = new Vector3(vehicleWheels.frontRightWheelAxis.localEulerAngles.x, vehicleWheels.frontRightWheelCol.steerAngle, vehicleWheels.frontRightWheelAxis.localEulerAngles.z);
        
        vehicleWheels.frontLeftWheelCol.GetWorldPose(out var posLeft, out var quatLeft);
        vehicleWheels.frontLeftWheel.position = posLeft;
        //vehicleWheels.frontLeftWheel.Rotate(quatLeft.eulerAngles);

        vehicleWheels.frontRightWheelCol.GetWorldPose(out var posRight, out var quatRight);
        vehicleWheels.frontRightWheel.position = posRight;
        //vehicleWheels.frontRightWheel.Rotate(quatRight.eulerAngles);

        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.GetWorldPose(out var pos, out var quat);
            wheel.obj.transform.position = pos;
            //wheel.obj.transform.Rotate(quat.eulerAngles);
        }

        AntiRoll();
    }

    public void AntiRoll()
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;
        float AntiRoll = 5000.0f;
    
        bool groundedL = vehicleWheels.frontLeftWheelCol.GetGroundHit(out hit);
        if (groundedL)
            travelL = (-vehicleWheels.frontLeftWheelCol.transform.InverseTransformPoint(hit.point).y - vehicleWheels.frontLeftWheelCol.radius) / vehicleWheels.frontLeftWheelCol.suspensionDistance;
    
        bool groundedR = vehicleWheels.frontRightWheelCol.GetGroundHit(out hit);
        if (groundedR)
            travelR = (-vehicleWheels.frontRightWheelCol.transform.InverseTransformPoint(hit.point).y - vehicleWheels.frontRightWheelCol.radius) / vehicleWheels.frontRightWheelCol.suspensionDistance;
    
        float antiRollForce = (travelL - travelR) * AntiRoll;
    
        if (groundedL)
            rigidbody.AddForceAtPosition(vehicleWheels.frontLeftWheelCol.transform.up * -antiRollForce,
                vehicleWheels.frontLeftWheelCol.transform.position);  
        if (groundedR)
            rigidbody.AddForceAtPosition(vehicleWheels.frontRightWheelCol.transform.up * antiRollForce,
                vehicleWheels.frontRightWheelCol.transform.position);  
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
        if (vehicleWheels.frontLeftWheelCol.steerAngle > -vehicleConfig.maxSteeringAngle)
        {
            vehicleWheels.frontLeftWheelCol.steerAngle -= vehicleConfig.steeringSpeed;
        }

        if (vehicleWheels.frontLeftWheelCol.steerAngle > -vehicleConfig.maxSteeringAngle)
        {
            vehicleWheels.frontRightWheelCol.steerAngle -= vehicleConfig.steeringSpeed;
        }
    }

    public virtual void SteerRight()
    {
        if (vehicleWheels.frontLeftWheelCol.steerAngle < vehicleConfig.maxSteeringAngle)
        {
            vehicleWheels.frontLeftWheelCol.steerAngle += vehicleConfig.steeringSpeed;
        }

        if (vehicleWheels.frontLeftWheelCol.steerAngle < vehicleConfig.maxSteeringAngle)
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

    public virtual void SetSteeringAngle(float angle)
    {
        vehicleWheels.frontLeftWheelCol.steerAngle = angle;
        vehicleWheels.frontRightWheelCol.steerAngle = angle;
    }

    public virtual float GetSteeringAngle()
    {
        return (vehicleWheels.frontLeftWheelCol.steerAngle + vehicleWheels.frontRightWheelCol.steerAngle) / 2;
    }

    public virtual void SetMotorTorque(float torque)
    {
        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.motorTorque = torque;
        }
    }

    public virtual void SetBrakeForce(float brake)
    {
        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.collider.brakeTorque = brake;
        }
    }
}