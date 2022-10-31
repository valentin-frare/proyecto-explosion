using UnityEngine;

public class PlayerVehicleMovement : BaseVehicleMovement
{
    public PlayerVehicleMovement(Transform transform, Rigidbody sphereMotor, VehicleWheels vehicleWheels, VehicleConfig vehicleConfig, float alignToGroundTime = 20) : base(transform, sphereMotor, vehicleWheels, vehicleConfig, alignToGroundTime)
    {
    }

    public override void Update()
    {
        transform.position = sphereMotor.transform.position;

        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, vehicleConfig.groundLayer);
        
        torque *= torque > 0 ? (vehicleConfig.torque / GameManager.instance.multiplyTorque) : vehicleConfig.torqueReverse;
        
        sphereMotor.drag = isCarGrounded ? normalDrag : vehicleConfig.drag;
    }

    public override void Accel()
    {
        SetMotorTorque(vehicleConfig.torque / GameManager.instance.multiplyTorque);
    }

    public override void Idle()
    {
        this.torque = ((vehicleConfig.torque / GameManager.instance.multiplyTorque)/4);
    }

    public override void SetMotorTorque(float torque)
    {
        if (isBraked)
            this.torque = 0f;
        else
            this.torque = Mathf.Clamp(torque * (vehicleConfig.torque / GameManager.instance.multiplyTorque), (vehicleConfig.torque / GameManager.instance.multiplyTorque)/4, (vehicleConfig.torque / GameManager.instance.multiplyTorque));
    }

    public override void SteerToCenter()
    {
        float singleStep = (vehicleConfig.steeringSpeed / GameManager.instance.multiplyHandling) * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, Vector3.back, singleStep, 0.0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDirection), Time.smoothDeltaTime * 4f);
    }

    public override void SetSteeringAngle(float input)
    {
        
        if (isCarGrounded)
        {
            float singleStep = (vehicleConfig.steeringSpeed / GameManager.instance.multiplyHandling) * Time.smoothDeltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, input < 0 ? Vector3.right : Vector3.left, singleStep, 0.0f);

            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, input < 0 ? Vector3.right : Vector3.left, vehicleConfig.steeringCurve.Evaluate(Mathf.Abs(input)), 0.0f);

            transform.rotation = Quaternion.Slerp(startRot, Quaternion.LookRotation(newDirection), vehicleConfig.steeringCurve.Evaluate(Mathf.Abs(input)));
        }
    }
}