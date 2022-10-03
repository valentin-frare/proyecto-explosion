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
        referenceObject = new GameObject("CamReference").transform;
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    void OnPlayerSpawn(GameObject player)
    {
        this.player = player.transform;

        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        cinemachineVirtualCamera.Follow = referenceObject;
    }

    void LateUpdate()
    {
        if (player == null) return;

        referenceObject.position = player.position.ooZ();
    }
}
