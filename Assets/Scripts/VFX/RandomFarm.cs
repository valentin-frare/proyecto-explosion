using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFarm : MonoBehaviour
{
    void Start()
    {
        GameObject objToActivate = transform.GetChild(Random.Range(0, transform.childCount)).gameObject;

        objToActivate.SetActive(Mathf.PerlinNoise(gameObject.transform.position.x * .8f, gameObject.transform.position.z * .8f) > .5f ? true : false);
    }
}
