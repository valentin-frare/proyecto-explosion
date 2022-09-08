using System;
using System.Collections;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class CanBuy : MonoBehaviour {

    [SerializeField] private int itemId;

    private Button button;
    private ItemInstance itemInstance;
    
    private void Awake() {
        button = GetComponent<Button>();

        button.interactable = false;
    }

    private void Start() {
        StartCoroutine(CheckCanBuy());
    }

    private void FixedUpdate() {
        if (PlayfabManager.instance.isInventoryUpdated == false)
        {
            button.interactable = false;
        }
    }

    IEnumerator CheckCanBuy()
    {
        yield return new  WaitUntil(() => {return PlayfabManager.instance.isSelectedVehicle;});

        button.interactable = false;
        
        yield return new  WaitUntil(() => {return PlayfabManager.instance.isInventoryUpdated;});
        
        itemInstance = PlayfabManager.instance.PlayerHas(itemId);

        button.interactable = (itemInstance == null);
    }

    public void Buy()
    {
        PlayfabManager.instance.PurchaseItem(itemId.ToString());

        StartCoroutine(CheckCanBuy());
    }
}