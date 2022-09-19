using System;
using UnityEngine;

public class NormalDriveState : IState
{
    private Transform enemy;
    private IVehicleMovement enemyMovement;
    private Sensors sensors;

    public NormalDriveState(Transform enemy, IVehicleMovement enemyMovement, Sensors sensors)
    {
        this.enemy = enemy;
        this.enemyMovement = enemyMovement;
        this.sensors = sensors;
    }

    public void Update()
    {   
        if (sensors.vehicleInFront && sensors.vehicleFarInFront) 
        {
            enemyMovement.Idle();

            if (sensors.vehicleOnLeft)
            {
                enemyMovement.SteerRight();
            }
            else if (sensors.vehicleOnRight)
            {
                enemyMovement.SteerLeft();
            }
            else if (sensors.vehicleOnLeft && sensors.vehicleOnRight)
            {
                // uwu
            }
            else
            {
                enemyMovement.SteerRight();
            }
        }
        else if (sensors.vehicleFarInFront && !sensors.vehicleInFront)
        {
            enemyMovement.Accel();

            if (Mathf.Abs(LaneHelper.instance.GetCloserRightLane(enemy.position).x - enemy.position.x) <= 0.3) 
            {
                enemyMovement.SteerToCenter();
            }
            else
            {
                if (sensors.vehicleOnLeft)
                {
                    enemyMovement.SteerRight();
                }
                else if (sensors.vehicleOnRight)
                {
                    enemyMovement.SteerLeft();
                }
                else if (sensors.vehicleOnLeft && sensors.vehicleOnRight)
                {
                    // uwu
                }
                else
                {
                    enemyMovement.SteerRight();
                }
            }
        }
        else
        {
            enemyMovement.Accel();

            if (Mathf.Abs(LaneHelper.instance.GetCloserRightLane(enemy.position).x - enemy.position.x) <= 0.3) 
            {
                enemyMovement.SteerToCenter();
            }
            else
            {
                SteerToClosestLane();
            }
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