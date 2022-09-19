using System.Collections.Generic;
using UnityEngine;

public class LaneHelper : MonoBehaviour {
    public static LaneHelper instance {get; private set;}

    private Transform player;

    [SerializeField] private List<GameObject> leftLanes = new List<GameObject>();
    [SerializeField] private List<GameObject> rightLanes = new List<GameObject>();

    private void Awake() {
        instance = this;

        GameEvents.OnPlayerSpawn += (GameObject player) => {
            this.player = player.transform;
        };
    }

    private void Start() {
        leftLanes.AddRange(GameObject.FindGameObjectsWithTag("LeftLane"));
        rightLanes.AddRange(GameObject.FindGameObjectsWithTag("RightLane"));
    }

    public Vector3 GetLeftLanePosition()
    {
        return leftLanes[0].transform.position;
    }

    public Vector3 CloserLeftLaneToPlayer
    {
        get
        {
            Vector3 closestLane = default;

            foreach (var lane in leftLanes)
            {
                if (Mathf.Abs(player.position.x - lane.transform.position.x) < Mathf.Abs(player.position.x - closestLane.x))
                {
                    closestLane = lane.transform.position;
                }
            }

            return closestLane;
        }
    }

    public Vector3 CloserRightLaneToPlayer
    {
        get
        {
            Vector3 closestLane = default;

            foreach (var lane in rightLanes)
            {
                if (Mathf.Abs(player.position.x - lane.transform.position.x) < Mathf.Abs(player.position.x - closestLane.x))
                {
                    closestLane = lane.transform.position;
                }
            }

            return closestLane;
        }
    }

    public Vector3 GetCloserLeftLane(Vector3 position)
    {
        Vector3 closestLane = default;

        foreach (var lane in leftLanes)
        {
            if (Mathf.Abs(position.x - lane.transform.position.x) < Mathf.Abs(position.x - closestLane.x) || closestLane == default)
            {
                closestLane = lane.transform.position;
            }
        }

        return closestLane;
    }

    public Vector3 GetCloserRightLane(Vector3 position)
    {
        Vector3 closestLane = default; // default = new Vector3(0,0,0)

        foreach (var lane in rightLanes)
        {
            if (Mathf.Abs(position.x - lane.transform.position.x) < Mathf.Abs(position.x - closestLane.x) || closestLane == default)
            {
                closestLane = lane.transform.position;
            }
        }

        return closestLane;
    }
}