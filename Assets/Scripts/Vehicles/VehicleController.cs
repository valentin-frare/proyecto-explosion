using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public Transform roadBuilderHelper;

    private IVehicleMovement vehicleMovement;

    private bool onSteeringRight => Input.GetKey(KeyCode.D);
    private bool onSteeringLeft => Input.GetKey(KeyCode.A);
    private bool onAccel => Input.GetKey(KeyCode.W);
    private bool onBrake => Input.GetKey(KeyCode.S);

    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private Rigidbody rb; 
    [SerializeField] private VehicleConfig vehicleConfig; 
    [SerializeField] private VehicleWheels vehicleWheels; 
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private TrailRenderer trailRenderer;

    private CameraControl cameraControl;
    private TrailController trailController;
    private bool stopHandleInputs = false;

    public void Init(SwipeControl swipeControl, CinemachineVirtualCamera cinemachineVirtualCamera)
    {
        this.swipeControl = swipeControl;
        this.cinemachineVirtualCamera = cinemachineVirtualCamera;
    }

    void Start()
    {
        vehicleMovement = new PlayerVehicleMovement(rb, vehicleWheels, vehicleConfig);
        swipeControl.Init(vehicleConfig.maxSteeringAngle, vehicleConfig.torque);
        cameraControl = new CameraControl(cinemachineVirtualCamera);
    }

    void Update()
    {
        roadBuilderHelper.position = transform.position.ooZ() + new Vector3(0,0,60);

        vehicleMovement.Update();

        if (stopHandleInputs)
        {
            return;
        }

        HandleInputs();
    }

    private void HandleInputs()
    {
        if (swipeControl.onDragging)
        {
            vehicleMovement.SetSteeringAngle(-swipeControl.Steering);

            vehicleMovement.SetMotorTorque(swipeControl.Acceleration);

            if (swipeControl.deltaY < 0) 
            {
                cameraControl.VehicleOnTop();
            }
            else 
            {
                cameraControl.VehicleOnBotton();
            }
        }
        else
        {
            vehicleMovement.SteerToCenter();

            vehicleMovement.Idle();

            cameraControl.VehicleOnCenter();
        }
    }

    public void StopVehicle()
    {
        stopHandleInputs = true;
        vehicleMovement.SetMotorTorque(0);
    }
}
