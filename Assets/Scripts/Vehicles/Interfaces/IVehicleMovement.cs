public interface IVehicleMovement
{
    public abstract void Update();
    public abstract void SteerLeft();
    public abstract void SteerRight();
    public abstract void SteerToCenter();
    public abstract float GetSteeringAngle();
    public abstract void SetSteeringAngle(float angle);
    public abstract void SetMotorTorque(float torque);
    public abstract void Accel();
    public abstract void Brake();
    public abstract void Reverse();
    public abstract void Idle();
}