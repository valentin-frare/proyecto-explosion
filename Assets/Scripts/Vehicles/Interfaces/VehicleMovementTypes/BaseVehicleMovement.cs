

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
    }

    public virtual void Update()
    {
        vehicleWheels.frontLeftWheelAxis.localEulerAngles = new Vector3(vehicleWheels.frontLeftWheelAxis.localEulerAngles.x, ((vehicleWheels.frontLeftWheelCol.steerAngle - 90f) - vehicleWheels.frontLeftWheel.localEulerAngles.z), vehicleWheels.frontLeftWheelAxis.localEulerAngles.z);
        vehicleWheels.frontRightWheelAxis.localEulerAngles = new Vector3(vehicleWheels.frontRightWheelAxis.localEulerAngles.x, ((vehicleWheels.frontRightWheelCol.steerAngle - 90f) - vehicleWheels.frontRightWheel.localEulerAngles.z), vehicleWheels.frontRightWheelAxis.localEulerAngles.z);

        vehicleWheels.frontLeftWheel.transform.Rotate((Vector3.up * vehicleWheels.frontLeftWheelCol.rpm) * Time.fixedDeltaTime); 
        vehicleWheels.frontRightWheel.transform.Rotate((Vector3.up * vehicleWheels.frontRightWheelCol.rpm) * Time.fixedDeltaTime); 

        foreach (var wheel in vehicleWheels.backWheels)
        {
            wheel.obj.transform.Rotate((Vector3.up * wheel.collider.rpm) * Time.fixedDeltaTime);
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