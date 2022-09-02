using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private SwipeControl swipeControl;

    private Material material => gameObject.GetComponent<MeshRenderer>().material;

    void Update()
    {
        gameObject.transform.position = target.position.ooZ();

        material.SetTextureOffset("_BaseMap", new Vector2(0, -(gameObject.transform.position.z / 10)));
    }
}
 