using System;
using UnityEngine;

public class NormalDriveState : IState
{
    private Transform enemy;
    private IVehicleMovement enemyMovement;

    public NormalDriveState(Transform enemy, IVehicleMovement enemyMovement)
    {
        this.enemy = enemy;
        this.enemyMovement = enemyMovement;
    }

    public void Update()
    {
        enemyMovement.Accel();
        enemyMovement.SteerToCenter();
    }
}