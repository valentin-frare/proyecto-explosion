using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
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
    [SerializeField] private GameObject trailRenderer;
    [SerializeField] private CrashDetectors crashDetectors;
    [SerializeField] private GameObject fireParticles;

    private CameraControl cameraControl;
    private TrailController trailController;
    private bool stopHandleInputs = false;

    private GameObject trail1;
    private GameObject trail2;
    private List<GameObject> allTrails = new List<GameObject>();

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
        roadBuilderHelper.position = transform.position.ooZ() + new Vector3(0,0,120);

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

        if (Mathf.Abs(swipeControl.Steering) > .5f && swipeControl.onDragging)
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

    private void OnPlayerCrash(Transform crashDetector)
    {
        GameManager.instance.gameState = GameState.Crashed;
        Instantiate(fireParticles, crashDetector.position, fireParticles.transform.rotation, crashDetector);
        stopHandleInputs = true;
        vehicleMovement.Brake();
        StartCoroutine(AbrirMenu(2f));
    }

    private IEnumerator AbrirMenu(float time){
        yield return new WaitForSeconds(time);
        Transform go = GameObject.FindGameObjectWithTag("GeneralMenu").transform;
        go.GetChild(0).gameObject.SetActive(true);
        go.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "PERDISTE";
        go.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "REINICIAR";
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
