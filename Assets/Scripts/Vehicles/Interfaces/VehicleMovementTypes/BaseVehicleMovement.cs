

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

    }

    public virtual void Accel()
    {
        float singleStep = vehicleConfig.torque * Time.deltaTime;

        rigidbody.transform.position = Vector3.MoveTowards(rigidbody.transform.position, rigidbody.transform.position + new Vector3(0,0,1), singleStep);
    }

    public virtual void Brake()
    {
        
    }

    public virtual void Reverse()
    {
        
    }

    public virtual void SteerLeft()
    {
        
    }

    public virtual void SteerRight()
    {
        
    }

    public virtual void Idle()
    {
        
    }

    public virtual void SteerToCenter()
    {
        float singleStep = vehicleConfig.steeringSpeed * Time.deltaTime;

        rigidbody.transform.rotation = Quaternion.RotateTowards(rigidbody.transform.rotation, Quaternion.Euler(0,0,0), singleStep);

        vehicleWheels.frontLeftWheel.rotation = Quaternion.Euler(0,0,-90f);
        vehicleWheels.frontRightWheel.rotation = Quaternion.Euler(0,0,-90f);
    }

    public virtual void SetSteeringAngle(float angle)
    {
        float singleStep = vehicleConfig.steeringSpeed * Time.deltaTime;

        rigidbody.transform.rotation = Quaternion.RotateTowards(rigidbody.transform.rotation, Quaternion.Euler(0,angle,0), singleStep);

        vehicleWheels.frontLeftWheel.rotation = Quaternion.Euler(0,angle,-90f);
        vehicleWheels.frontRightWheel.rotation = Quaternion.Euler(0,angle,-90f);

        //rigidbody.transform.position = Vector3.MoveTowards(rigidbody.transform.position, rigidbody.transform.position + new Vector3(angle > 0 ? -1 : 1,0,0), singleStep / 4);
    }

    public virtual float GetSteeringAngle()
    {
        return 0f;
    }

    public virtual void SetMotorTorque(float torque)
    {
        

        float singleStep = -torque * Time.deltaTime;

        Debug.Log(singleStep);

        rigidbody.transform.position = Vector3.MoveTowards(rigidbody.transform.position, new Vector3(rigidbody.transform.position.x,rigidbody.transform.position.y,singleStep > 0 ? -10 : 10), singleStep);
    }

    public virtual void SetBrakeForce(float brake)
    {
        
    }
}