using System.Collections.Generic;
using UnityEngine;

public class LaneHelper : MonoBehaviour {
    public static LaneHelper instance {get; private set;}

    [SerializeField] private List<GameObject> leftLanes = new List<GameObject>();
    private List<GameObject> rightLanes = new List<GameObject>();

    private void Awake() {
        instance = this;
    }

    private void Start() {
        leftLanes.AddRange(GameObject.FindGameObjectsWithTag("LeftLane"));
        rightLanes.AddRange(GameObject.FindGameObjectsWithTag("RightLane"));
    }

    public Vector3 GetLeftLanePosition()
    {
        return leftLanes[0].transform.position;
    }
}