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
            if (sensors.vehicleOnLeft)
            {
                enemyMovement.Brake();
                enemyMovement.SteerRight();
            }
            else if (sensors.vehicleOnRight)
            {
                enemyMovement.Brake();
                enemyMovement.SteerLeft();
            }
            else if (sensors.vehicleOnLeft && sensors.vehicleOnRight)
            {
                enemyMovement.Brake();
            }
            else
            {
                enemyMovement.Brake();
                enemyMovement.SteerLeft();
            }
        }
        else if (sensors.vehicleFarInFront && !sensors.vehicleInFront)
        {
            enemyMovement.Accel();

            if (Mathf.Abs(LaneHelper.instance.GetLeftLanePosition().x - enemy.position.x) <= 0.7) 
            {
                enemyMovement.SteerToCenter();
            }
            else
            {
                var steerAngle = -(LaneHelper.instance.GetLeftLanePosition().x - enemy.position.x);
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
        else
        {
            enemyMovement.Accel();

            if (Mathf.Abs(LaneHelper.instance.GetLeftLanePosition().x - enemy.position.x) <= 0.7) 
            {
                enemyMovement.SteerToCenter();
            }
            else
            {
                var steerAngle = -(LaneHelper.instance.GetLeftLanePosition().x - enemy.position.x);
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
        
    }
}