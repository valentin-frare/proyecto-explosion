using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawnByDistance : MonoBehaviour
{
    private GameObject player;
    private Vector3 originalScale;

    [SerializeField] private AnimationCurve spawnCurve;

    private void Awake() 
    {
        originalScale = gameObject.transform.localScale;

        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    private void OnPlayerSpawn(GameObject player)
    {
        this.player = player;
    }

    private void Update() 
    {
        if (player == null) 
        {
            player = RespawnManager.instance?.GetActualPlayer();
            return;
        }

        Debug.Log(Vector3.Distance(player.transform.position, transform.position));

        transform.localScale = Vector3.Lerp(new Vector3(0,0,0), originalScale, spawnCurve.Evaluate(
            Mathf.Clamp01(
                Vector3.Distance(player.transform.position, transform.position)-90
            )
        ));
    }
}
