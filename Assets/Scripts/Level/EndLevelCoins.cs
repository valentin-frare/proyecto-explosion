using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndLevelCoins : MonoBehaviour
{
    public static EndLevelCoins instance {get; private set;}
    public int totalCoins = 0;
    public int levelCoins = 0;
    public int objectCoins = 0;
    public int timerCoins = 0;
    private const float crazyNumber = 4.4f;

    private void Awake()
    {
        instance = this;
    }

    public void GenerateCoinsEndLevel(float finishLine, float torque, float timer)
    {
        float minTime = 0;
        float midTime = 0;
        float maxTime = 0;

        minTime = finishLine / 1 * crazyNumber;
        midTime = finishLine / (torque / 2) * crazyNumber;
        maxTime = finishLine / torque * crazyNumber;

        int minCoin = 1;
        int midCoin = 25;
        int maxCoin = 100;

        float lerp = 0;

        if (timer <= minTime && timer >= midTime)
        {
            lerp = Mathf.Lerp(minCoin,midCoin,Mathf.Clamp01(((minTime - timer) / midTime)));
            timerCoins = Mathf.CeilToInt(lerp);
        }
        else if (timer < midTime && timer >= maxTime)
        {
            lerp = Mathf.Lerp(midCoin,maxCoin,Mathf.Clamp01(((midTime - timer) / maxTime)));
            timerCoins = Mathf.CeilToInt(lerp);
        }
        else if (timer < maxTime)
        {
            timerCoins = maxCoin;
        }
        else if (timer > minTime)
        {
            timerCoins = minCoin;
        }
        levelCoins += timerCoins;

        AddTotalCoins();
    }

    public void AddCoins(int n)
    {
        objectCoins += n;
        levelCoins += n;
    }

    public void AddTotalCoins()
    {
        totalCoins += levelCoins;
    }

    public void RestartCoins()
    {
        levelCoins = 0;
        objectCoins = 0;
        timerCoins = 0;
    }
}
