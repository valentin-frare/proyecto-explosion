using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayfabManager : MonoBehaviour
{
    public PlayfabManager instance {get; private set;}

    public int coins {get; private set;}

    private void Awake() {
        instance = this;
    }

    private void Start()
    {
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
        Debug.Log("Login");

        GetVirtualCurrencies();
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

        Debug.Log(coins);
    }
}
