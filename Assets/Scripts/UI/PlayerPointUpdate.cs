using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPointUpdate : MonoBehaviour
{
    
    public Transform playerPoint;
    public Transform playerPointInvisible;
    private Transform playerPosition;
    private float maxWidth;
    private float final;
    private float start;
    private float playerPointStart;
    private float porcentaje;
    private VehicleController playerVehicle;
    private float timer = 0;

    private void Awake() 
    {
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    private void Start()
    {
        maxWidth = playerPointInvisible.position.x - playerPoint.position.x;
        playerPointStart = playerPoint.position.x;
    }
    
    private void OnPlayerSpawn(GameObject player)
    {
        playerPosition = player.transform;
        playerVehicle = player.GetComponent<VehicleController>();
        start = playerPosition.position.z;
        porcentaje = 0;
        final = GameManager.instance.GetActualLevel().finishLine;
    }

    private void LateUpdate()
    {
        if (playerPosition == null) return;

        if (porcentaje >= 100)
        {
            return;
        }

        timer += Time.deltaTime;
        porcentaje = (start-playerPosition.position.z)*100/final;
        playerPoint.position = new Vector2(playerPointStart + (porcentaje*maxWidth/100),playerPoint.position.y);

        if (porcentaje >= 100)
        {
            playerVehicle.StopVehicle();
            StartCoroutine(GameManager.instance.Victory(2f, final, timer));
            timer = 0;
        }
    }
}
