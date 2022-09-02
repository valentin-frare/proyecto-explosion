using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        gameObject.transform.position = target.position.ooZ();
    }
}
 