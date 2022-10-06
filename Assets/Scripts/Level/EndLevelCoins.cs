using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndLevelCoins : MonoBehaviour
{
    public static EndLevelCoins instance {get; private set;}
    public int totalCoins = 0;
    public int levelCoins = 0;
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
            levelCoins = levelCoins + Mathf.CeilToInt(lerp);
        }
        else if (timer < midTime && timer >= maxTime)
        {
            lerp = Mathf.Lerp(midCoin,maxCoin,Mathf.Clamp01(((midTime - timer) / maxTime)));
            levelCoins = levelCoins + Mathf.CeilToInt(lerp);
        }
        else if (timer < maxTime)
        {
            levelCoins = levelCoins + maxCoin;
        }
        else if (timer > minTime)
        {
            levelCoins = levelCoins + minCoin;
        }
    }

    public void AddCoins(int n)
    {
        levelCoins = levelCoins + n;
        Debug.Log("agarraste: " + n);
    }

    public void AddTotalCoins()
    {
        totalCoins = totalCoins + levelCoins;
    }
}
