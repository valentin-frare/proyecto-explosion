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
}