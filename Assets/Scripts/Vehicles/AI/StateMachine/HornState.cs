using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HornState : IState
{
    public Action OnHorn;

    private bool onPlaySound;
    private bool onEndSound;
    private MonoBehaviour coroutineHelper;
    private IVehicleMovement enemyMovement;

    public HornState(MonoBehaviour coroutineHelper, IVehicleMovement enemyMovement)
    {
        this.coroutineHelper = coroutineHelper;
        this.enemyMovement = enemyMovement;
    }

    public void Update()
    {
        enemyMovement.Idle();

        if (onPlaySound == false)
        {
            //SoundManager.instance?.StartSound("car_horn_01");
            coroutineHelper.StartCoroutine(WaitForSound(1f));
        }

        if (onEndSound)
        {
            onEndSound = false;
            onPlaySound = false;
            OnHorn.Invoke();
        }
    }

    IEnumerator WaitForSound(float seconds)
    {
        onPlaySound = true;

        yield return new WaitForSeconds(seconds);

        onEndSound = true;
    }
}
