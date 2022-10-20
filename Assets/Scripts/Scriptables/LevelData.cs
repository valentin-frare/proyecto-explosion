using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "LevelData", menuName = "Trinity/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    [Header("Identifier")]
    public Level level;

    [Space]
    [Header("Road Builder Settings")]
    public List<GameObject> roadChunks;

    [Space]
    [Header("Spawn Plants Settings")]
    public GameObject plants;
    public GameObject obstacles;
    public GameObject civilCars;

    [Space]
    [Header("Lane Helper Settings")]
    public float finishLine;
    public List<float> lanes;

    [Space]
    [Header("Spawn Coins")]
    public float positionCoinStart;
    public float positionBetweenCoins;
    public float posBetCoinsMinOffset;
    public float posBetCoinsMaxOffset;

    [Space]
    [Header("Spawn Broken Vehicles")]
    public float positionBvStart;
    public float positionBetweenBv;
    public float posBetBvMinOffset;
    public float posBetBvMaxOffset;

    [Space]
    [Header("Spawn Civilian Vehicles Left")]
    public float positionCvlStart;
    public float positionBetweenCvl;

    [Space]
    [Header("Spawn Civilian Vehicles Right")]
    public float positionCvrStart;
    public float positionBetweenCvr;
}