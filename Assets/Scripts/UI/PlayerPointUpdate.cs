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
    public float final = 500f;
    private float start;
    private float playerPointStart;
    private float porcentaje = 0;
    private VehicleController playerVehicle;

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
    }

    private void LateUpdate()
    {
        if (playerPosition == null) return;

        if (porcentaje >= 100)
        {
            return;
        }

        porcentaje = (start-playerPosition.position.z)*100/final;
        playerPoint.position = new Vector2(playerPointStart + (porcentaje*maxWidth/100),playerPoint.position.y);

        if (porcentaje >= 100)
        {
            playerVehicle.StopVehicle();
            StartCoroutine(VictoryMenu(2f));
        }
    }

    private IEnumerator VictoryMenu(float x)
    {
        yield return new WaitForSeconds(x);
        Transform go = GameObject.FindGameObjectWithTag("GeneralMenu").transform;
        go.GetChild(0).gameObject.SetActive(true);
        go.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "GANASTE";
    }
}
