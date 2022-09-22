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
    }

    private void OnPlayerSpawn(GameObject player)
    {
        CancelInvoke();
        this.player = player.transform;
        finishLine = new Vector3(0, 0, this.player.position.z - GameObject.FindObjectOfType<PlayerPointUpdate>(true).final);
        InvokeRepeating("ActivateObject", 1.0f, 3.0f);
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
            poolingManager.DeactivateObjects();
            poolingManagerObstacles.DeactivateObjects();
        }
    }
}
