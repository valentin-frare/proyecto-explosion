using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    [SerializeField] private GameObject plants;
    [SerializeField] private int amount;
    [SerializeField] private GameObject obstacles;
    [SerializeField] private int amountObstacles;
    [SerializeField] private SwipeControl swipeCtrl;

    private PoolingManager poolingManager;
    private PoolingManager poolingManagerObstacles;
    private Camera cam;
    private Transform anyRoute;
    private Transform player;
    private Vector3 finishLine;
    private List<Vector3> positionCoins = new List<Vector3>();
    private List<bool> activeCoins = new List<bool>();
    private List<Vector3> positionBrokenVehicles = new List<Vector3>();
    private List<bool> activeBrokenVehicles = new List<bool>();
    private List<Transform> lanes = new List<Transform>();
    private bool dontDoAnything = false;
    
    private void Awake()
    {
        cam = Camera.main;
        
        poolingManager = new PoolingManager(plants, amount);
        poolingManager.Init();

        poolingManagerObstacles = new PoolingManager(obstacles, amountObstacles);
        poolingManagerObstacles.Init();
    
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
        GameManager.instance.OnGameStateChanged += ClearObjects;
    }

    private void Start()
    {
        var originalRoute = GameObject.FindGameObjectWithTag("Road").transform;
        anyRoute = new GameObject("TerribleRoad").transform;
        anyRoute.position = originalRoute.position;
        anyRoute.localScale = originalRoute.localScale;
        Transform generalLane = GameObject.FindGameObjectWithTag("Lanes").transform;
        for (int i = 0; i < generalLane.childCount; i++)
        {
            lanes.Add(generalLane.GetChild(i).transform);
        }
    }

    private void OnPlayerSpawn(GameObject player)
    {
        //CancelInvoke();
        this.player = player.transform;
        finishLine = new Vector3(0, 0, this.player.position.z - GameObject.FindObjectOfType<PlayerPointUpdate>(true).final);
        AlignObjects();
        //InvokeRepeating("ActivateObject", 1.0f, 3.0f);
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
        //dontDoAnything = false;
    }

    private void ActivateObject()
    {
        if (GameManager.instance.gameState != GameState.Playing)
        {
            return;
        }

        Vector3 position = new Vector3();
        Vector3 positionObstacle = new Vector3();

        float left = cam.ScreenToWorldPoint(new Vector3(0f + cam.pixelWidth*0.1f, 0f)).x;
        float right = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth - cam.pixelWidth*0.1f, 0f)).x;

        float rightRoute = anyRoute.position.x + (anyRoute.localScale.x/(Mathf.Sign(anyRoute.localScale.x) >= 0 ? -2 : 2) - anyRoute.localScale.x*0.1f);
        if (right < rightRoute)
        {
            right = rightRoute;
        }

        float leftRoute = anyRoute.position.x - (anyRoute.localScale.x/(Mathf.Sign(anyRoute.localScale.x) >= 0 ? -2 : 2) - anyRoute.localScale.x*0.1f);
        if (left > leftRoute)
        {
            left = leftRoute;
        }

        if ((player.transform.position.z - 70) <= (finishLine.z)){
            return;
        }

        position = new Vector3(NotSoRandom(left, right), 1, player.transform.position.z - 70);
        positionObstacle = new Vector3((position.x >= 0 ? -8f : 8f), 1, player.transform.position.z - 75);
        StartCoroutine(DeleteAfter(20f, poolingManager.GetPooledObject(position), true));
        StartCoroutine(DeleteAfter(20f, poolingManagerObstacles.GetPooledObject(positionObstacle), false));
    }

    private float NotSoRandom(float left, float right)
    {
        float steer = swipeCtrl.Steering;

        if (steer == 0)
        {
            return Random.Range(left, right);
        }
        else if (steer > 0)
        {
            return Random.Range(left, right+((left-right)*0.25f));
        }
        else
        {
            return Random.Range(left-((left-right)*0.25f), right);
        }
    }

    public IEnumerator DeleteAfter(float time, GameObject obj, bool bol)
    {
        yield return new WaitForSeconds(time);

        yield return new WaitUntil(() => { return (GameManager.instance.gameState == GameState.Playing); });

        if (bol)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                obj.transform.GetChild(i).gameObject.SetActive(false);
                obj.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }
        obj.SetActive(false);
    }

    public void ClearObjects(GameState state)
    {
        if (state == GameState.Playing)
        {
            dontDoAnything = false;
            //poolingManager.DeactivateObjects();
            //poolingManagerObstacles.DeactivateObjects();
        }
    }

    private void LateUpdate()
    {
        if (dontDoAnything)
        {
            return;
        }

        if (GameManager.instance.gameState == GameState.Crashed)
        {
            positionCoins.Clear();
            activeCoins.Clear();
            poolingManager.DeactivateObjects();
            positionBrokenVehicles.Clear();
            activeBrokenVehicles.Clear();
            poolingManagerObstacles.DeactivateObjects();
            dontDoAnything = true;
        }

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
