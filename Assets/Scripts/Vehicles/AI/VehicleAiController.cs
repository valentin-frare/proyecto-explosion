using System.Collections.Generic;
using UnityEngine;

public class VehicleAiController : MonoBehaviour 
{
    private enum EnemyStates
    {
        FollowPlayer,
        NormalDrive,
        CrashingPlayer
    }

    private IVehicleMovement vehicleMovement;
    private Transform player;
    private StateMachine stateMachine;
    [SerializeField] private Sensors sensors;

    [SerializeField] private Transform vehicle; 
    [SerializeField] private Rigidbody sphere; 
    [SerializeField] private VehicleConfig vehicleConfig; 
    [SerializeField] private VehicleWheels vehicleWheels; 
    [SerializeField] private SensorsConfig sensorsConfig;

    private List<IState> states = new List<IState>();

    private void Awake() 
    {
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    private void OnPlayerSpawn(GameObject player)
    {
        this.player = player.transform;

        Init();
    }

    private void Init()
    {
        sensors = new Sensors(gameObject, sensorsConfig);

        sensors.Init();

        vehicleMovement = new EnemyVehicleMovement(vehicle, sphere, vehicleWheels, vehicleConfig);

        FollowPlayerState followPlayerState = new FollowPlayerState(transform, vehicleMovement, player, 25f);
        NormalDriveState normalDriveState = new NormalDriveState(transform, vehicleMovement, sensors);
        CrashingPlayerState crashingPlayerState = new CrashingPlayerState(transform, vehicleMovement, player);

        followPlayerState.onPlayerReached += OnPlayerReached;

        states.Add(normalDriveState);
        states.Add(followPlayerState);
        
        states.Add(crashingPlayerState);

        stateMachine = new StateMachine(states);
    }

    private void OnPlayerReached()
    {
        stateMachine.SetCurrentState((int)EnemyStates.NormalDrive);
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
}
