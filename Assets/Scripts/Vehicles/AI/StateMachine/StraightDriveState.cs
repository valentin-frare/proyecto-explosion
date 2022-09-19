using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightDriveState : IState
{
    private IVehicleMovement enemyMovement;
    private Sensors sensors;
    private Transform enemy;

    public Action OnVehicleOnFront;

    public StraightDriveState(Transform enemy, IVehicleMovement enemyMovement, Sensors sensors)
    {
        this.enemy = enemy;
        this.enemyMovement = enemyMovement;
        this.sensors = sensors;
    }

    public void Update()
    {
        enemyMovement.Idle();

        SteerToClosestLane();

        if (sensors.vehicleInFront)
        {
            OnVehicleOnFront.Invoke();
        }
    }

    private void SteerToClosestLane()
    {
        var steerAngle = -(LaneHelper.instance.GetCloserRightLane(enemy.position).x - enemy.position.x);
        if (steerAngle < 0 && !sensors.vehicleOnLeft)
        {
            enemyMovement.SetSteeringAngle(steerAngle);
        }
        else if (steerAngle > 0 && !sensors.vehicleOnRight)
        {
            enemyMovement.SetSteeringAngle(steerAngle);
        }
        else
        {
            enemyMovement.SteerToCenter();
        }
    }
}