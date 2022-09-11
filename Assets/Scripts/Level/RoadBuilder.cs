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

    private void Awake() 
    {
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    private void OnPlayerSpawn(GameObject player)
    {
        this.player = player.GetComponent<VehicleController>().roadBuilderHelper;
        playerSpawnZ = this.player.position.z;
    }

    private void Start() 
    {
        levelContainer = GameObject.FindGameObjectWithTag("LvlContainer").transform;

        roadsContainer = new GameObject("RoadsContainer").transform;
        roadsContainer.SetParent(levelContainer);

        for (int i = 0; i <= roadCount; i++)
        {
            roads.Add(Instantiate(roadPrefabs[Random.Range(0, roadPrefabs.Count)], new Vector3(0,0,30) - new Vector3(0,0,maxDistance * i),  transform.rotation, roadsContainer).transform);
        }
        playerPointUpdate = GameObject.FindObjectOfType<PlayerPointUpdate>(true);
        Instantiate(finishLine, new Vector3(0,0,playerSpawnZ-playerPointUpdate.final),  transform.rotation, roadsContainer);
    }

    void LateUpdate()
    {
        if (player == null) return;

        foreach (Transform road in roads)
        {
            if ((player.position.z - road.position.z) < -maxDistance)
            {
                var pos = (player.position.ooZ_Rounded() - new Vector3(0,0,maxDistance * roadCount));
                road.position = new Vector3(pos.x, pos.y, Mathf.CeilToInt(pos.z / 10) * 10);
                generaciones++;
                //Debug.Log(generaciones);
            }
        }
    }
}
