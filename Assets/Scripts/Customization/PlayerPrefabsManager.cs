using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabsManager : MonoBehaviour {

    public static PlayerPrefabsManager instance {get; private set;}

    private int actualPlayerModel = 0;
    
    [SerializeField] private List<GameObject> playerModels;
    
    private void Awake() 
    {
        instance = this;
    }

    IEnumerator GetPlayerPrefab()
    {
        yield return new  WaitUntil(() => {return PlayfabManager.instance.isLogged;});

        PlayfabManager.instance.GetSelectedVehicle();

        yield return new  WaitUntil(() => {return PlayfabManager.instance.isInventoryUpdated;});

        for (int i = 0; i < playerModels.Count; i++)
        {
            if (playerModels[i].name == PlayfabManager.instance.selectedVehicle)
                actualPlayerModel = i;
        }
    }

    private void Start() {
        if (PlayfabManager.instance)
            StartCoroutine(GetPlayerPrefab());
        else
            actualPlayerModel = 0;
    }

    public GameObject GetActualPlayerModel(int index = 0)
    {
        return playerModels[index];
    }
}