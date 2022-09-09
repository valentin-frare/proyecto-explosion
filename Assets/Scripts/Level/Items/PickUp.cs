using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int points = 100;
    private void OnTriggerEnter()
    {
        Debug.Log("Puntos: " + points);
        gameObject.SetActive(false);
    }
}
