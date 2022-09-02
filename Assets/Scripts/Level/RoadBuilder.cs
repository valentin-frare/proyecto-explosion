using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
    [SerializeField] private List<GameObject> roadPrefabs;
    private Transform player;
    private List<Transform> roads = new List<Transform>();
    [SerializeField] private int maxDistance;
    [SerializeField] private int roadCount;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<VehicleController>().roadBuilderHelper;
    }

    private void Start() {
        for (int i = 0; i <= roadCount; i++)
        {
            roads.Add(Instantiate(roadPrefabs[Random.Range(0, roadPrefabs.Count)], player.position.ooZ() - new Vector3(0,0,maxDistance * i),  transform.rotation).transform);
        }
    }

    void Update()
    {
        foreach (Transform road in roads)
        {
            if ((player.position.z - road.position.z) < -maxDistance)
            {
                road.position = player.position.ooZ_Rounded() - new Vector3(0,0,maxDistance * roadCount);
            }
        }
    }
}
