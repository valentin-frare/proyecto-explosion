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

    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private Rigidbody rb; 
    [SerializeField] private VehicleConfig vehicleConfig; 
    [SerializeField] private VehicleWheels vehicleWheels; 

    void Start()
    {
        vehicleMovement = new PlayerVehicleMovement(rb, vehicleWheels, vehicleConfig);

        swipeControl.Init(vehicleConfig.maxSteeringAngle, vehicleConfig.torque);
    }

    void Update()
    {
        vehicleMovement.Update();

        HandleInputs();
    }

    private void HandleInputs()
    {
        if (swipeControl.onDragging)
        {
            vehicleMovement.SetSteeringAngle(-swipeControl.Steering);

            vehicleMovement.SetMotorTorque(swipeControl.Acceleration);
        }
        else
        {
            vehicleMovement.SteerToCenter();

            vehicleMovement.Idle();
        }
    }
}
