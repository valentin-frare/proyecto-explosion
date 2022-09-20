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

    public HornState(MonoBehaviour coroutineHelper)
    {
        this.coroutineHelper = coroutineHelper;
    }

    public void Update()
    {
        if (onPlaySound == false)
        {
            SoundManager.instance?.StartSound("car_horn_01");
            coroutineHelper.StartCoroutine(WaitForSound(1f));
        }

        if (onEndSound)
            OnHorn.Invoke();
    }

    IEnumerator WaitForSound(float seconds)
    {
        onPlaySound = true;

        yield return new WaitForSeconds(seconds);

        onEndSound = true;
    }
}
