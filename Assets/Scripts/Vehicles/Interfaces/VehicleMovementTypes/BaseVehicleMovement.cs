using UnityEngine;

public class BaseVehicleMovement : IVehicleMovement
{   
    protected VehicleConfig vehicleConfig;
    protected Transform transform;
    protected Rigidbody sphereMotor;
    protected VehicleWheels vehicleWheels;
    protected Rigidbody rigidbody;
    protected bool isCarGrounded;
    protected bool isBraked;
    protected float steering;
    protected float torque;
    protected float normalDrag;
    protected float alignToGroundTime;
    protected Quaternion startRot;

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
        transform.position = sphereMotor.transform.position;

        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, vehicleConfig.groundLayer);
        
        torque *= torque > 0 ? (vehicleConfig.torque / GameManager.instance.multiplyTorque) : vehicleConfig.torqueReverse;
        
        sphereMotor.drag = isCarGrounded ? normalDrag : vehicleConfig.drag;
    }

    public virtual void FixedUpdate()
    {
        if (isCarGrounded)
            sphereMotor.AddForce(transform.forward * torque, ForceMode.Acceleration);
        else
            sphereMotor.AddForce(transform.up * -200f);
    }

    public virtual void Accel()
    {
        SetMotorTorque(vehicleConfig.torque / GameManager.instance.multiplyTorque);
    }

    public virtual void Brake()
    {
        isBraked = true;

        SetMotorTorque(0f); //Update the motor torque
    }

    public virtual void Reverse()
    {
        
    }

    public virtual void SteerLeft()
    {
        SetSteeringAngle(.8f);
    }

    public virtual void SteerRight()
    {
        SetSteeringAngle(-.8f);
    }

    public virtual void Idle()
    {
        this.torque = ((vehicleConfig.torque / GameManager.instance.multiplyTorque)/4);
    }

    public virtual void SteerToCenter()
    {
        float singleStep = vehicleConfig.steeringSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, Vector3.back, singleStep, 0.0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDirection), Time.smoothDeltaTime * 4f);
    }

    public virtual void SetSteeringAngle(float input)
    {
        float newRot = input * vehicleConfig.steeringSpeed * Time.deltaTime;
        
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
        if (isBraked)
            this.torque = 0f;
        else
            this.torque = Mathf.Clamp(torque * (vehicleConfig.torque / GameManager.instance.multiplyTorque), (vehicleConfig.torque / GameManager.instance.multiplyTorque)/4, (vehicleConfig.torque / GameManager.instance.multiplyTorque));
    }

    public virtual void SetBrakeForce(float brake)
    {
        
    }

    public virtual void Teleport(Vector3 newPosition, Quaternion newRotation)
    {
        this.sphereMotor.transform.position = newPosition;
        this.sphereMotor.transform.rotation = newRotation;
    }
}