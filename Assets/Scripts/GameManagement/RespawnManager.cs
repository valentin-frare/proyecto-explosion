using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

    public static RespawnManager instance {get; private set;}

    private GameObject playerPrefab;
    private Transform spawnPoint;
    private GameObject player;
    [SerializeField] private List<GameObject> players = new List<GameObject>();

    [SerializeField] private InputControl inputControl;
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
            player.GetComponent<VehicleController>().enabled = false;
        }

        player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        player.GetComponent<VehicleController>().Init(inputControl, cinemachineVirtualCamera);

        players.Add(player);

        GameEvents.OnPlayerSpawn.Invoke(player);
    }

    public GameObject GetActualPlayer() => player;

    public void DeleteAllPlayers(){
        foreach (GameObject obj in players)
        {
            Debug.Log(obj.GetComponent<VehicleController>());
            if (obj != null)
            {
                obj.GetComponent<VehicleController>().DestroyVehicle();
            }
        }
        players.Clear();
    }    
}