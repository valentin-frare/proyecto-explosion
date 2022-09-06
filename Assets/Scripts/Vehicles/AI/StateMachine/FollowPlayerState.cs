using System;
using UnityEngine;

public class FollowPlayerState : IState
{
    private Transform player;
    private float minDistance;
    private Transform enemy;
    private IVehicleMovement enemyMovement;
    private bool eventCalled;
    private bool playerReached;
    public Action onPlayerReached;

    public FollowPlayerState(Transform enemy, IVehicleMovement enemyMovement, Transform player, float minDistance)
    {
        this.enemy = enemy;
        this.enemyMovement = enemyMovement;
        this.player = player;
        this.minDistance = minDistance;
    }

    public void Update()
    {
        if (Vector3.Distance(enemy.position, player.position) > minDistance && enemy.position.z > player.position.z)
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