using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public Transform roadBuilderHelper;

    private IVehicleMovement vehicleMovement;

    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private Transform vehicle; 
    [SerializeField] private Rigidbody sphere; 
    [SerializeField] private VehicleConfig vehicleConfig; 
    [SerializeField] private VehicleWheels vehicleWheels; 
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private CrashDetectors crashDetectors;
    [SerializeField] private GameObject fireParticles;

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
        vehicleMovement = new PlayerVehicleMovement(vehicle, sphere, vehicleWheels, vehicleConfig);
        swipeControl.Init();
        cameraControl = new CameraControl(cinemachineVirtualCamera);
        crashDetectors.OnVehicleCrashed += OnPlayerCrash;
    }

    void Update()
    {
        roadBuilderHelper.position = transform.position.ooZ() + new Vector3(0,0,60);

        vehicleMovement.Update();

        if (stopHandleInputs)
        {
            return;
        }

        crashDetectors.Update();

        HandleInputs();
    }

    private void FixedUpdate() {
        if (stopHandleInputs)
        {
            return;
        }

        vehicleMovement.FixedUpdate();
    }

    private void HandleInputs()
    {
        if (swipeControl.onDragging)
        {
            vehicleMovement.SetSteeringAngle(-swipeControl.Steering);

            vehicleMovement.SetMotorTorque(-swipeControl.Acceleration);

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

    private void OnPlayerCrash(Transform crashDetector)
    {
        Instantiate(fireParticles, crashDetector.position, fireParticles.transform.rotation, crashDetector);
        stopHandleInputs = true;
        vehicleMovement.Brake();
    }

    public void StopVehicle()
    {
        stopHandleInputs = true;
        vehicleMovement.Brake();
    }
}
