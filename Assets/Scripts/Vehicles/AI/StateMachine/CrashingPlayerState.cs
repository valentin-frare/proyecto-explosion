using System;
using UnityEngine;

public class CrashingPlayerState : IState
{
    private Transform enemy;
    private IVehicleMovement enemyMovement;
    private Transform player;
    private bool eventCalled;
    private bool playerReached;
    public Action onPlayerReached;

    public CrashingPlayerState(Transform enemy, IVehicleMovement enemyMovement, Transform player)
    {
        this.enemy = enemy;
        this.enemyMovement = enemyMovement;
        this.player = player;
    }
    
    public void Update()
    {
        if (enemy.position.z > player.position.z)
        {
            playerReached = false;
            eventCalled = false;
            if (player.position.x < enemy.position.x && Mathf.Abs(player.position.x - enemy.position.x) > 2f) 
            {
                enemyMovement.SteerRight();
            }
            else if (player.position.x > enemy.position.x && Mathf.Abs(player.position.x - enemy.position.x) > 2f) 
            {
                enemyMovement.SteerLeft();
            }
            else 
            {
                enemyMovement.SteerToCenter();
            }
            enemyMovement.Accel();
        }
        else if (enemy.position.z < player.position.z)
        {
            enemyMovement.Brake();
        }
        else
        {
            playerReached = true;
            enemyMovement.SteerToCenter();
        }

        if (playerReached && eventCalled == false)
        {
            eventCalled = true;
            onPlayerReached.Invoke();
        }
    }
}