using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Awake()
    {
        GameEvents.OnPlayerSpawn += Init;
    }

    public void Init(GameObject target)
    {
        this.target = target.transform;
    }

    private void Update()
    {
        if (target == null) return;

        gameObject.transform.position = target.position.ooZ();
    }
}
 