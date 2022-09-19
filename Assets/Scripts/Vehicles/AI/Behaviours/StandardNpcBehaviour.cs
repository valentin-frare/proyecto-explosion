using System;
using System.Collections.Generic;
using UnityEngine;

public class StandardNpcBehaviour : IBehaviour
{
    private List<IState> states = new List<IState>();

    public StandardNpcBehaviour(Transform transform, IVehicleMovement vehicleMovement, Transform player, Sensors sensors)
    {
        FollowPlayerState followPlayerState = new FollowPlayerState(transform, vehicleMovement, player, 25f);
        NormalDriveState normalDriveState = new NormalDriveState(transform, vehicleMovement, sensors);
        StraightDriveState straightDriveState = new StraightDriveState(transform, vehicleMovement, sensors);
        CrashingPlayerState crashingPlayerState = new CrashingPlayerState(transform, vehicleMovement, player);

        followPlayerState.onPlayerReached += OnPlayerReached;
        straightDriveState.OnVehicleOnFront += OnVehicleOnFront;

        states.Add(straightDriveState);
        states.Add(normalDriveState);
        states.Add(followPlayerState);
        states.Add(crashingPlayerState);
    }

    private void OnVehicleOnFront()
    {
        Debug.Log("VehicleOnFront");
    }

    public List<IState> GetStates()
    {
        return states;
    }

    public void OnPlayerReached()
    {

    }
}