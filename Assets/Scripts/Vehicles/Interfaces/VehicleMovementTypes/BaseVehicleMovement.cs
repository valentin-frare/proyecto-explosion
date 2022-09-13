

using UnityEngine;

public class BaseVehicleMovement : IVehicleMovement
{   
    protected VehicleConfig vehicleConfig;
    protected Transform transform;
    protected Rigidbody sphereMotor;
    protected VehicleWheels vehicleWheels;
    protected Rigidbody rigidbody;
    protected bool isCarGrounded;
    protected float steering;
    protected float torque;
    protected float normalDrag;
    protected float alignToGroundTime;

    Quaternion startRot;

    public BaseVehicleMovement(Transform transform, Rigidbody sphereMotor, VehicleWheels vehicleWheels, VehicleConfig vehicleConfig, float alignToGroundTime = 20f)
    {
        this.transform = transform;
        this.sphereMotor = sphereMotor;
        this.vehicleWheels = vehicleWheels;
        this.vehicleConfig = vehicleConfig;
        this.alignToGroundTime = alignToGroundTime;
        
        sphereMotor.transform.parent = null;
        normalDrag = sphereMotor.drag;

        startRot = transform.rotation;
    }

    public virtual void Update()
    {
        // Set Cars Position to Our Sphere
        transform.position = sphereMotor.transform.position;

        // Raycast to the ground and get normal to align car with it.
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, vehicleConfig.groundLayer);
        
        // Rotate Car to align with ground
        //Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        //transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);
        
        // Calculate Movement Direction
        torque *= torque > 0 ? vehicleConfig.torque : vehicleConfig.torqueReverse;
        
        // Calculate Drag
        sphereMotor.drag = isCarGrounded ? normalDrag : vehicleConfig.drag;
    }

    public virtual void FixedUpdate()
    {
        if (isCarGrounded)
            sphereMotor.AddForce(transform.forward * torque, ForceMode.Acceleration); // Add Movement
        else
            sphereMotor.AddForce(transform.up * -200f); // Add Gravity
    }

    public virtual void Accel()
    {
        
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
        this.torque = vehicleConfig.torque/4;
    }

    public virtual void SteerToCenter()
    {
        float singleStep = vehicleConfig.steeringSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, Vector3.back, singleStep, 0.0f);

        //transform.rotation = Quaternion.LookRotation(newDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDirection), Time.smoothDeltaTime * 4f);
    }

    public virtual void SetSteeringAngle(float input)
    {

        float newRot = input * vehicleConfig.steeringSpeed * Time.deltaTime /** torque*/;
        
        if (isCarGrounded)
        {
            float singleStep = vehicleConfig.steeringSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, input < 0 ? Vector3.right : Vector3.left, vehicleConfig.steeringCurve.Evaluate(Mathf.Abs(input)), 0.0f);

            transform.rotation = Quaternion.Slerp(startRot, Quaternion.LookRotation(newDirection), vehicleConfig.steeringCurve.Evaluate(Mathf.Abs(input)));
        }
    }

    public virtual float GetSteeringAngle()
    {
        return 0f;
    }

    public virtual void SetMotorTorque(float torque)
    {
        this.torque = Mathf.Clamp(torque * vehicleConfig.torque, vehicleConfig.torque/4, vehicleConfig.torque);
    }

    public virtual void SetBrakeForce(float brake)
    {
        
    }
}