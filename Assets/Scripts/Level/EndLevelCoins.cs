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
        float differenceCoin = 0;
        float differenceTimer = 0;
        float numberCoinTimer = 0;

        if (timer <= minTime && timer >= midTime)
        {
            differenceCoin = midCoin - minCoin;
            differenceTimer = (minTime - midTime);
            numberCoinTimer = differenceTimer / differenceCoin;

            levelCoins = levelCoins + midCoin - (int)Mathf.Floor(timer/numberCoinTimer);
        }
        else if (timer <= midTime && timer >= maxTime)
        {
            differenceCoin = maxCoin - midCoin;
            differenceTimer = (midTime - maxTime);
            numberCoinTimer = differenceTimer / differenceCoin;

            levelCoins = levelCoins + maxCoin - (int)Mathf.Floor(timer/numberCoinTimer);
        }
        else if (timer < maxTime)
        {
            levelCoins = levelCoins + minCoin;
        }
        else if (timer > minTime)
        {
            levelCoins = levelCoins + minCoin;
        }
    }

    public void AddCoins(int n)
    {
        levelCoins = levelCoins + n;
    }

    public void AddTotalCoins()
    {
        totalCoins = totalCoins + levelCoins;
    }
}
