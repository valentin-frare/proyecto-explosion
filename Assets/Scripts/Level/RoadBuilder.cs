using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
    [SerializeField] private int maxDistance;
    [SerializeField] private int roadCount;
    [SerializeField] private List<GameObject> roadPrefabs;
    [SerializeField] private GameObject finishLine;
    private Transform player;
    private List<Transform> roads = new List<Transform>();
    private Transform levelContainer;
    private Transform roadsContainer;
    private PlayerPointUpdate playerPointUpdate;
    private float playerSpawnZ;

    private int generaciones = 0;

    private int farDistance = 0;

    private void Awake() 
    {
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    private void OnPlayerSpawn(GameObject player)
    {
        this.player = player.GetComponent<VehicleController>().roadBuilderHelper;
        playerSpawnZ = this.player.position.z;
        InitRoad();
    }

    private void Start()
    {
        InitRoad();
    }

    private void InitRoad()
    {
        if(roadsContainer != null)
        {
            Destroy(roadsContainer.gameObject);
            roads = new List<Transform>();
        }
        levelContainer = GameObject.FindGameObjectWithTag("LvlContainer").transform;

        roadsContainer = new GameObject("RoadsContainer").transform;
        roadsContainer.SetParent(levelContainer);

        for (int i = 0; i <= roadCount; i++)
        {
            roads.Add(Instantiate(roadPrefabs[Random.Range(0, roadPrefabs.Count)], new Vector3(0, 0, 120) - new Vector3(0, 0, maxDistance * i), transform.rotation, roadsContainer).transform);
        }
        playerPointUpdate = GameObject.FindObjectOfType<PlayerPointUpdate>(true);
        Instantiate(finishLine, new Vector3(0, 0, playerSpawnZ - maxDistance - playerPointUpdate.final), transform.rotation, roadsContainer);

        farDistance = ((int)roads[roads.Count - 1].position.z);
    }

    void LateUpdate()
    {
        if (player == null) return;

        foreach (Transform road in roads)
        {
            if(road == null)
            {
                break;
            }
            
            if ((player.position.z - road.position.z) < (-maxDistance * 3))
            {
                var pos = (player.position.ooZ_Rounded() - new Vector3(0,0,maxDistance * roadCount));
                road.position = new Vector3(pos.x, pos.y, farDistance - maxDistance);
                generaciones++;
                farDistance -= maxDistance;
            }
        }
    }
}
