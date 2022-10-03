using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

    public static RespawnManager instance {get; private set;}

    private GameObject playerPrefab;
    private Transform spawnPoint;
    private GameObject player;
    private List<VehicleController> players = new List<VehicleController>();

    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake() {
        instance = this;
    }

    private void Start() 
    {
        playerPrefab = PlayerPrefabsManager.instance.GetActualPlayerModel();

        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
    }

    public void SpawnPlayer()
    {
        if(player != null)
        {
            Destroy(player.GetComponent<VehicleController>());
        }
        player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        player.GetComponent<VehicleController>().Init(swipeControl, cinemachineVirtualCamera);

        players.Add(player.GetComponent<VehicleController>());

        GameEvents.OnPlayerSpawn.Invoke(player);
    }

    public void DeleteAllPlayers(){
        foreach (VehicleController obj in players)
        {
            obj.DestroyVehicle();
        }
        players.Clear();
    }    
}