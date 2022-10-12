using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class VehicleController : MonoBehaviour, IDamageable
{
    public Transform roadBuilderHelper;

    private IVehicleMovement vehicleMovement;

    [SerializeField] private InputControl inputControl;
    [SerializeField] private Transform vehicle; 
    [SerializeField] private Rigidbody sphere; 
    [SerializeField] private VehicleConfig vehicleConfig; 
    [SerializeField] private VehicleWheels vehicleWheels; 
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private GameObject trailRenderer;
    [SerializeField] private CrashDetectors crashDetectors;
    [SerializeField] private GameObject fireParticles;

    private CameraControl cameraControl;
    private TrailController trailController;
    private bool stopHandleInputs = false;

    private GameObject trail1;
    private GameObject trail2;
    private List<GameObject> allTrails = new List<GameObject>();

    public void Init(InputControl inputControl, CinemachineVirtualCamera cinemachineVirtualCamera)
    {
        this.inputControl = inputControl;
        this.cinemachineVirtualCamera = cinemachineVirtualCamera;
    }

    void Start()
    {
        vehicleMovement = new PlayerVehicleMovement(vehicle, sphere, vehicleWheels, vehicleConfig);
        inputControl.Init();
        cameraControl = new CameraControl(cinemachineVirtualCamera);
        crashDetectors.OnVehicleCrashed += OnPlayerCrash;

        cameraControl.ResetView();
    }

    void Update()
    {
        roadBuilderHelper.position = transform.position.ooZ() + new Vector3(0,0,120);

        vehicleMovement.Update();

        if (stopHandleInputs)
        {
            return;
        }

        //crashDetectors.Update();

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
        if (inputControl.onDragging)
        {
            vehicleMovement.SetSteeringAngle(-inputControl.Steering);

            vehicleMovement.SetMotorTorque(-inputControl.Acceleration);

            if (inputControl.deltaY < 0) 
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

        if (Mathf.Abs(inputControl.Steering) > .5f && inputControl.onDragging)
        {
            if (trail1 == null)
            {
                trail1 = Instantiate(trailRenderer, vehicleWheels.backWheels[0].obj.transform.position, vehicleWheels.backWheels[0].obj.transform.rotation, vehicleWheels.backWheels[0].obj.transform);
                allTrails.Add(trail1);
            }

            if (trail2 == null)
            {
                trail2 = Instantiate(trailRenderer, vehicleWheels.backWheels[1].obj.transform.position, vehicleWheels.backWheels[1].obj.transform.rotation, vehicleWheels.backWheels[1].obj.transform);
                allTrails.Add(trail2);
            }
        }
        else 
        {
            if (trail1 != null)
            {
                trail1.transform.parent = null;
                trail1 = null;
            }

            if (trail2 != null)
            {
                trail2.transform.parent = null;
                trail2 = null;
            }
        }
    }
    
    public void Damage(int damage = 1)
    {
        Debug.Log("Auch");
    }

    private void OnPlayerCrash(Transform crashDetector)
    {
        GameManager.instance.gameState = GameState.Crashed;
        Instantiate(fireParticles, crashDetector.position, fireParticles.transform.rotation, crashDetector);
        stopHandleInputs = true;
        vehicleMovement.Brake();
        StartCoroutine(GameManager.instance.Defeat(2f));
    }

    public void StopVehicle()
    {
        stopHandleInputs = true;
        vehicleMovement.Brake();
    }

    public void DestroyVehicle()
    {
        foreach (GameObject item in allTrails)
        {
            Destroy(item);
        }
        Destroy(sphere.gameObject);
        Destroy(this.gameObject);
    }
}
