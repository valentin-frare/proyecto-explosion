using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VehicleAiController : MonoBehaviour 
{
    public IVehicleMovement vehicleMovement;

    private Transform player;
    private StateMachine stateMachine;
    private IBehaviour currentBehaviour;

    [SerializeField] private Sensors sensors;
    [SerializeField] private Transform vehicle; 
    [SerializeField] private Rigidbody sphere; 
    [SerializeField] private VehicleConfig vehicleConfig; 
    [SerializeField] private VehicleWheels vehicleWheels; 
    [SerializeField] private SensorsConfig sensorsConfig;

    private void Awake() 
    {
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    private void OnPlayerSpawn(GameObject player)
    {
        this.player = player.transform;

        Init();
    }

    public void Init()
    {
        sensors = new Sensors(gameObject, sensorsConfig);

        sensors.Init();

        vehicleMovement = new EnemyVehicleMovement(vehicle, sphere, vehicleWheels, vehicleConfig);

        currentBehaviour = new StandardNpcBehaviour(transform, vehicleMovement, player, sensors, this as VehicleAiController, this as MonoBehaviour);

        stateMachine = new StateMachine(currentBehaviour.GetStates());

        Debug.Log(stateMachine.GetCurrentState());
    }

    private void Update()
    {
        if (stateMachine == null) return;

        vehicleMovement.Update();

        stateMachine.Update();

        sensors.Update();

        #if UNITY_EDITOR
        sensors.DebugMode();
        #endif
    }

    private void FixedUpdate() 
    {
        if (stateMachine == null) return;

        vehicleMovement.FixedUpdate();
    }

    private void OnDisable() 
    {
        try
        {
            sphere.gameObject.SetActive(false);
        }
        catch (MissingReferenceException)
        {

        }
    }

    private void OnEnable() 
    {
        sphere.gameObject.SetActive(true);
    }

    public void ChangeState(string state)
    {
        Debug.Log(state);
        stateMachine.SetCurrentState( stateMachine.States.FindIndex( st => st.ToString() == state ) );
    }
}
