using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int points = 100;
    private void OnTriggerEnter()
    {
        EndLevelCoins.instance.AddCoins(points);
        SoundManager.instance?.StartSound("coin_pickup_01");
        gameObject.SetActive(false);
    }
}
