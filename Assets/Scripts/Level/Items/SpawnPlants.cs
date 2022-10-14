using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    [SerializeField] private GameObject plants;
    [SerializeField] private int amount;
    [SerializeField] private GameObject obstacles;
    [SerializeField] private int amountObstacles;

    private PoolingManager poolingManager;
    private PoolingManager poolingManagerObstacles;
    private Transform player;
    private Vector3 finishLine;
    private List<Vector3> positionCoins = new List<Vector3>();
    private List<bool> activeCoins = new List<bool>();
    private List<Vector3> positionBrokenVehicles = new List<Vector3>();
    private List<bool> activeBrokenVehicles = new List<bool>();
    private List<Transform> lanes = new List<Transform>();
    
    private void Awake()
    {        
        poolingManager = new PoolingManager(plants, amount);
        poolingManager.Init();

        poolingManagerObstacles = new PoolingManager(obstacles, amountObstacles);
        poolingManagerObstacles.Init();
    
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    private void Start()
    {
        Transform generalLane = GameObject.FindGameObjectWithTag("Lanes").transform;
        for (int i = 0; i < generalLane.childCount; i++)
        {
            lanes.Add(generalLane.GetChild(i).transform);
        }
    }

    private void OnPlayerSpawn(GameObject player)
    {
        plants = GameManager.instance?.GetActualLevel().plants;
        obstacles = GameManager.instance?.GetActualLevel().obstacles;

        positionCoins.Clear();
        activeCoins.Clear();
        poolingManager.DeactivateObjects();
        positionBrokenVehicles.Clear();
        activeBrokenVehicles.Clear();
        poolingManagerObstacles.DeactivateObjects();

        this.player = player.transform;
        finishLine = new Vector3(0, 0, this.player.position.z - GameManager.instance.finishLine);
        AlignObjects();
    }

    private void AlignObjects()
    {
        List<float> lanesBrokenVehicles = new List<float>();
        lanesBrokenVehicles.Add(lanes[0].position.x - 2);
        lanesBrokenVehicles.Add(lanes[lanes.Count-1].position.x + 2);
        if (lanes.Count > 2)
        {
            lanesBrokenVehicles.Add(0);
        }


        float x = this.player.position.z - 80;
        while (x > finishLine.z)
        {
            positionCoins.Add(new Vector3(lanes[Random.Range(0,lanes.Count)].position.x, 1, x));
            positionBrokenVehicles.Add(new Vector3(lanesBrokenVehicles[Random.Range(0,lanesBrokenVehicles.Count)], 1, x));
            x -= 80;
        }


        for (int i = 0; i < positionCoins.Count; i++)
        {
            activeCoins.Add(false);
            activeBrokenVehicles.Add(false);
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < positionCoins.Count; i++)
        {
            if (!activeCoins[i])
            {
                if (positionCoins[i].z >= (player.position.z - 150))
                {
                    activeCoins[i] = true;
                    poolingManager.GetPooledObject(positionCoins[i]);
                }
            }
        }

        for (int i = 0; i < poolingManager.pooledObjects.Count; i++)
        {
            GameObject go = poolingManager.pooledObjects[i];
            if (go.activeSelf)
            {
                if (go.transform.position.z >= (player.position.z + 50))
                {
                    for (int x = 0; x < go.transform.childCount; x++)
                    {
                        go.transform.GetChild(x).gameObject.SetActive(false);
                        go.transform.GetChild(x).GetChild(0).gameObject.SetActive(true);
                    }
                    go.SetActive(false);
                }
            }
        }

        for (int i = 0; i < positionBrokenVehicles.Count; i++)
        {
            if (!activeBrokenVehicles[i])
            {
                if (positionBrokenVehicles[i].z >= (player.position.z - 150))
                {
                    activeBrokenVehicles[i] = true;
                    poolingManagerObstacles.GetPooledObject(positionBrokenVehicles[i]);
                }
            }
        }

        for (int i = 0; i < poolingManagerObstacles.pooledObjects.Count; i++)
        {
            GameObject go = poolingManagerObstacles.pooledObjects[i];
            if (go.activeSelf)
            {
                if (go.transform.position.z >= (player.position.z + 50))
                {
                    go.SetActive(false);
                }
            }
        }

    }
}