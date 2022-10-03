using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

    public static RespawnManager instance {get; private set;}

    private GameObject playerPrefab;
    private Transform spawnPoint;
    private GameObject player;
    private List<GameObject> players = new List<GameObject>();

    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private GameObject roadFollow;

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

        Instantiate(roadFollow, transform.position, transform.rotation).GetComponent<RoadFollow>().Init(player);

        players.Add(player);

        GameEvents.OnPlayerSpawn.Invoke(player);
    }

    public void DeleteAllPlayers(){
        foreach (GameObject obj in players)
        {
            Destroy(obj);
        }
        players.Clear();
    }    
}