using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraInit : MonoBehaviour
{
    private Transform referenceObject;
    private Transform player;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    void Awake()
    {
        referenceObject = new GameObject("CamReference").transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        cinemachineVirtualCamera.Follow = referenceObject;
    }

    void Update()
    {
        referenceObject.position = player.position.ooZ();
    }
}
