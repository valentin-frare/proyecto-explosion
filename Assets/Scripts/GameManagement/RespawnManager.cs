using System;
using Cinemachine;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

    public static RespawnManager instance {get; private set;}

    private GameObject playerPrefab;
    private Transform spawnPoint;

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
        var player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        player.GetComponent<VehicleController>().Init(swipeControl, cinemachineVirtualCamera);

        Instantiate(roadFollow, transform.position, transform.rotation).GetComponent<RoadFollow>().Init(player);

        GameEvents.OnPlayerSpawn.Invoke(player);
    }
}