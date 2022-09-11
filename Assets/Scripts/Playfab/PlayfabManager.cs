using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Linq;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance {get; private set;}

    public int coins {get; private set;}

    public bool isLogged;
    public bool isInventoryUpdated;
    public bool isSelectedVehicle;
    public string selectedVehicle;

    private List<ItemInstance> inventory;

    private void Awake() {
        instance = this;
    }

    private void Start()
    {
        isSelectedVehicle = false;

        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    private void OnSuccess(LoginResult result)
    {
        //Debug.Log("Login");

        GetVirtualCurrencies();

        UpdateInventory();

        isLogged = true;
    }

    private void UpdateInventory()
    {
        isInventoryUpdated = false;
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) => {
            inventory = result.Inventory;
            
            isInventoryUpdated = true;

            //Debug.Log(inventory);
        }, OnError);
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
    }

    private void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySucces, OnError);
    }

    private void OnGetUserInventorySucces(GetUserInventoryResult result)
    {
        coins = result.VirtualCurrency["CN"];

        //Debug.Log(coins);

        GetCatalog();
    }

    public void GetCatalog()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), (result) => {
            //Debug.Log(result.Catalog[0].DisplayName);
        }, OnError);
    }

    public ItemInstance PlayerHas(int itemId)
    {
        //Debug.Log(inventory);

        var item = inventory.Find((item) => {
            return int.Parse(item.ItemId) == itemId;
            });

        return item;
    }

    public void PurchaseItem(string itemId)
    {
        isInventoryUpdated = false;

        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest() {
            
            CatalogVersion = "Vehicles",
            ItemId = itemId,
            VirtualCurrency = "CN"

        }, (result) => {
            UpdateInventory();
        }, OnError);
    }

    public void GetSelectedVehicle()
    {
        isInventoryUpdated = false;

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result => {
            if (result.Data.ContainsKey("SelectedVehicle") == false)
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("SelectedVehicle", "Starter Vehicle");

                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                    Data = data
                }, result => {
                    selectedVehicle = "Starter Vehicle";
                    isSelectedVehicle = true;
                    UpdateInventory();
                }, OnError);

                return;
            }

            selectedVehicle = result.Data["SelectedVehicle"].ToString();
            isSelectedVehicle = true;
            UpdateInventory();
        }, OnError);
    }

    public void SetSelectedVehicle(string itemId)
    {

    }
}
