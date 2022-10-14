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

    [Space]
    [Header("Lane Helper Settings")]
    public float finishLine;
    public List<float> lanes;
}