using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraInit : MonoBehaviour
{
    private Transform referenceObject;
    private Transform player;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake() {
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    void OnPlayerSpawn(GameObject player)
    {
        referenceObject = new GameObject("CamReference").transform;

        this.player = player.transform;

        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        cinemachineVirtualCamera.Follow = referenceObject;
    }

    void Update()
    {
        if (player == null) return;

        referenceObject.position = player.position.ooZ();
    }
}
