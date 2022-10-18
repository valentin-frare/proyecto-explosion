using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    [SerializeField] private GameObject plants;
    [SerializeField] private int amount;
    [SerializeField] private GameObject obstacles;
    [SerializeField] private int amountObstacles;
    [SerializeField] private GameObject civilCars;
    [SerializeField] private int amountCivilCars;

    private PoolingManager poolingManager;
    private PoolingManager poolingManagerObstacles;
    private PoolingManager poolingManagerCivilCars;
    private Transform player;
    private Vector3 finishLine;
    private List<Vector3> positionCoins = new List<Vector3>();
    private List<bool> activeCoins = new List<bool>();
    private List<Vector3> positionBrokenVehicles = new List<Vector3>();
    private List<bool> activeBrokenVehicles = new List<bool>();
    private List<Vector3> positionCivilVeh = new List<Vector3>();
    private List<bool> activeCivilVeh = new List<bool>();
    private List<Transform> lanes = new List<Transform>();
    private Transform generalLane;
    
    private void Awake()
    {
        var container = GameObject.FindGameObjectWithTag("LvlContainer").transform;        
        poolingManager = new PoolingManager(plants, amount, container);
        poolingManager.Init();

        poolingManagerObstacles = new PoolingManager(obstacles, amountObstacles, container);
        poolingManagerObstacles.Init();

        poolingManagerCivilCars = new VehiclePoolingManager(civilCars, amountCivilCars);
        poolingManagerCivilCars.Init();
    
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    private void Start()
    {
        generalLane = GameObject.FindGameObjectWithTag("Lanes").transform;
    }

    private void OnPlayerSpawn(GameObject player)
    {
        plants = GameManager.instance?.GetActualLevel().plants;
        obstacles = GameManager.instance?.GetActualLevel().obstacles;
        civilCars = GameManager.instance?.GetActualLevel().civilCars;

        positionCoins.Clear();
        activeCoins.Clear();
        poolingManager.DeactivateObjects();
        positionBrokenVehicles.Clear();
        activeBrokenVehicles.Clear();
        poolingManagerObstacles.DeactivateObjects();
        positionCivilVeh.Clear();
        activeCivilVeh.Clear();
        poolingManagerCivilCars.DeactivateObjects();
        TransformLanes();

        this.player = player.transform;
        finishLine = new Vector3(0, 0, this.player.position.z - GameManager.instance.GetActualLevel().finishLine);
        AlignObjects();
    }

    private void TransformLanes()
    {
        lanes.Clear();

        if (GameManager.instance.GetActualLevel().lanes.Count == 4)
        {
            for (int i = 0; i < generalLane.childCount; i++)
            {
                generalLane.GetChild(i).position = new Vector3 (GameManager.instance.GetActualLevel().lanes[i], generalLane.GetChild(i).position.y, generalLane.GetChild(i).position.z);
            }
        }
        else if (GameManager.instance.GetActualLevel().lanes.Count == 2)
        {
            int x = 0;
            for (int i = 0; i < generalLane.childCount; i = i + 2)
            {
                generalLane.GetChild(i).position = new Vector3 (GameManager.instance.GetActualLevel().lanes[x], generalLane.GetChild(i).position.y, generalLane.GetChild(i).position.z);
                generalLane.GetChild(i+1).position = new Vector3 (GameManager.instance.GetActualLevel().lanes[x], generalLane.GetChild(i).position.y, generalLane.GetChild(i).position.z);
                x++;
            }
        }

        for (int i = 0; i < generalLane.childCount; i++)
        {
            lanes.Add(generalLane.GetChild(i).transform);
        }
    }

    private void AlignObjects()
    {
        List<float> lanesBrokenVehicles = new List<float>();
        lanesBrokenVehicles.Add(lanes[0].position.x - 2);
        lanesBrokenVehicles.Add(lanes[lanes.Count-1].position.x + 2);
        if (GameManager.instance.GetActualLevel().lanes.Count > 2)
        {
            lanesBrokenVehicles.Add(0);
        }


        float x = this.player.position.z - 80;
        while (x > finishLine.z)
        {
            positionCoins.Add(new Vector3(lanes[Random.Range(0, lanes.Count)].position.x, 1, x));
            positionBrokenVehicles.Add(new Vector3(lanesBrokenVehicles[Random.Range(0,lanesBrokenVehicles.Count)], 1, x));
            positionCivilVeh.Add(new Vector3(lanes[Random.Range(0, lanes.Count/2)].position.x, 1, x));
            x -= 80;
        }

        for (int i = 0; i < positionCoins.Count; i++)
        {
            activeCoins.Add(false);
            activeBrokenVehicles.Add(false);
            activeCivilVeh.Add(false);
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

        for (int i = 0; i < poolingManager.PooledObjects.Count; i++)
        {
            GameObject go = poolingManager.PooledObjects[i];
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

        for (int i = 0; i < poolingManagerObstacles.PooledObjects.Count; i++)
        {
            GameObject go = poolingManagerObstacles.PooledObjects[i];
            if (go.activeSelf)
            {
                if (go.transform.position.z >= (player.position.z + 50))
                {
                    go.SetActive(false);
                }
            }
        }

        for (int i = 0; i < positionCivilVeh.Count; i++)
        {
            if (!activeCivilVeh[i])
            {
                if (positionCivilVeh[i].z >= (player.position.z - 150))
                {
                    activeCivilVeh[i] = true;
                    poolingManagerCivilCars.GetPooledObject(positionCivilVeh[i]);
                }
            }
        }

        for (int i = 0; i < poolingManagerCivilCars.PooledObjects.Count; i++)
        {
            GameObject go = poolingManagerCivilCars.PooledObjects[i];
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