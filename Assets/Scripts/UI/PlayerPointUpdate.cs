using System.Collections;
using System.Collections.Generic;
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
    
    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        maxWidth = playerPointInvisible.position.x - playerPoint.position.x;
        playerPointStart = playerPoint.position.x;
        start = playerPosition.position.z;
        final = 1000;
    }

    private void LateUpdate()
    {
        float porcentaje = (start-playerPosition.position.z)*100/final;
        if(porcentaje >= 100){
            return;
        }
        playerPoint.position = new Vector2(playerPointStart + (porcentaje*maxWidth/100),playerPoint.position.y);
    }
}
