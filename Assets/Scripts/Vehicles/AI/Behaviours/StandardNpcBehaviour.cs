using System;
using System.Collections.Generic;
using UnityEngine;

public class StandardNpcBehaviour : IBehaviour
{
    private List<IState> states = new List<IState>();
    private VehicleAiController controller;

    public StandardNpcBehaviour(Transform transform, IVehicleMovement vehicleMovement, Transform player, Sensors sensors, VehicleAiController controller, MonoBehaviour coroutineHelper)
    {
        this.controller = controller;

        FollowPlayerState followPlayerState = new FollowPlayerState(transform, vehicleMovement, player, 25f);
        NormalDriveState normalDriveState = new NormalDriveState(transform, vehicleMovement, sensors);
        StraightDriveState straightDriveState = new StraightDriveState(transform, vehicleMovement, sensors);
        CrashingPlayerState crashingPlayerState = new CrashingPlayerState(transform, vehicleMovement, player);
        HornState hornState = new HornState(coroutineHelper);

        followPlayerState.onPlayerReached += OnPlayerReached;
        straightDriveState.OnVehicleOnFront += OnVehicleOnFront;
        hornState.OnHorn += OnHorn;

        states.Add(straightDriveState);
        states.Add(hornState);
        states.Add(normalDriveState);
        states.Add(followPlayerState);
        states.Add(crashingPlayerState);
    }

    private void OnVehicleOnFront()
    {
        controller.ChangeState( nameof(HornState) );
    }

    private void OnHorn()
    {
        controller.ChangeState( nameof(StraightDriveState) );
    }

    public List<IState> GetStates()
    {
        return states;
    }

    public void OnPlayerReached()
    {

    }
}