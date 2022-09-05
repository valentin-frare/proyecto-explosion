using System;
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

    [SerializeField] private Rigidbody rb; 
    [SerializeField] private VehicleConfig vehicleConfig; 
    [SerializeField] private VehicleWheels vehicleWheels; 

    private List<IState> states = new List<IState>();

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        vehicleMovement = new EnemyVehicleMovement(rb, vehicleWheels, vehicleConfig);

        FollowPlayerState followPlayerState = new FollowPlayerState(transform, vehicleMovement, player, 25f);
        NormalDriveState normalDriveState = new NormalDriveState(transform, vehicleMovement);
        CrashingPlayerState crashingPlayerState = new CrashingPlayerState(transform, vehicleMovement, player);

        followPlayerState.onPlayerReached += OnPlayerReached;

        states.Add(followPlayerState);
        states.Add(normalDriveState);
        states.Add(crashingPlayerState);

        stateMachine = new StateMachine(states);
    }

    private void OnPlayerReached()
    {
        stateMachine.SetCurrentState((int)EnemyStates.NormalDrive);
    }

    private void Update()
    {
        vehicleMovement.Update();

        stateMachine.Update();
    }

    private void HandleInputs()
    {
        
    }
}
