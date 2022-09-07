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

        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject GetActualPlayerModel() => playerModels[actualPlayerModel];
}