using UnityEngine;

public class EnemyVehicleMovement : BaseVehicleMovement
{
    public EnemyVehicleMovement(Transform transform, Rigidbody sphereMotor, VehicleWheels vehicleWheels, VehicleConfig vehicleConfig, float alignToGroundTime = 20) : base(transform, sphereMotor, vehicleWheels, vehicleConfig, alignToGroundTime)
    {
    }

    public override void SteerLeft()
    {
        steering -= .5f * Time.deltaTime;

        SetSteeringAngleInternal(steering);

        Debug.Log(steering);
    }

    public override void SteerRight()
    {
        steering += .5f * Time.deltaTime;

        SetSteeringAngleInternal(steering);

        Debug.Log(steering);
    }

    public void SetSteeringAngleInternal(float input)
    {
        float newRot = input * vehicleConfig.steeringSpeed * Time.deltaTime;
        
        if (isCarGrounded)
        {
            float singleStep = vehicleConfig.steeringSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, input < 0 ? Vector3.right : Vector3.left, vehicleConfig.steeringCurve.Evaluate(Mathf.Abs(input)), 0.0f);

            transform.rotation = Quaternion.Slerp(startRot, Quaternion.LookRotation(newDirection), vehicleConfig.steeringCurve.Evaluate(Mathf.Abs(input)));
        }
    }

    public override void SetSteeringAngle(float input)
    {
        steering = 0f;

        float singleStep = vehicleConfig.steeringSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, input < 0 ? Vector3.right : Vector3.left, singleStep, 0.0f);

        transform.rotation = Quaternion.Slerp(startRot, Quaternion.LookRotation(newDirection), vehicleConfig.steeringCurve.Evaluate(Mathf.Abs(input)));
    }
}