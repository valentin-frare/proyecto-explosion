using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    private IVehicleMovement vehicleMovement;

    private bool onSteeringRight => Input.GetKey(KeyCode.D);
    private bool onSteeringLeft => Input.GetKey(KeyCode.A);
    private bool onAccel => Input.GetKey(KeyCode.W);
    private bool onBrake => Input.GetKey(KeyCode.S);

    [SerializeField] private VehicleConfig vehicleConfig; 
    [SerializeField] private VehicleWheels vehicleWheels; 

    void Start()
    {
        vehicleMovement = new PlayerVehicleMovement(vehicleWheels, vehicleConfig);
    }

    void Update()
    {
        vehicleMovement.Update();

        HandleInputs();
    }

    private void HandleInputs()
    {
        if (onSteeringRight)
        {
            vehicleMovement.SteerRight();
        }
        else if (onSteeringLeft)
        {
            vehicleMovement.SteerLeft();
        }
        else
        {
            vehicleMovement.SteerToCenter();
        }

        if (onAccel)
        {
            vehicleMovement.Accel();
        }
        else if (onBrake)
        {
            vehicleMovement.Brake();
        }
        else
        {
            vehicleMovement.Idle();
        }
    }
}
