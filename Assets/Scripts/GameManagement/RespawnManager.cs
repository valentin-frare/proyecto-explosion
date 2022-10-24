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
            Destroy(player.GetComponent<VehicleController>().sphere.gameObject);
        }
        
        spawnPoint.position = new Vector3(
            GameManager.instance.GetActualLevel().lanes.Find(lane => lane < 0), 
            spawnPoint.position.y, 
            spawnPoint.position.z
        );

        player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        player.GetComponent<VehicleController>().Init(inputControl, cinemachineVirtualCamera);

        players.Add(player);

        if (players.Count > 4)
        {
            players[0].GetComponent<VehicleController>().DestroyVehicle();
            players.RemoveAt(0);
        }

        GameEvents.OnPlayerSpawn.Invoke(player);
    }

    public void DeleteAllPlayers(){
        foreach (GameObject obj in players)
        {
            if (obj != null)
            {
                obj.GetComponent<VehicleController>().DestroyVehicle();
            }
        }
        players.Clear();
    }

    public GameObject GetPlayer()
    {
        return player;
    }    
}