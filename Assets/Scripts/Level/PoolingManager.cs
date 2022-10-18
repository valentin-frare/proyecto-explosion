using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    private Transform container;

    public PoolingManager(GameObject objectToPool, int amountToPool, Transform container = null)
    {
        this.objectToPool = objectToPool;
        this.amountToPool = amountToPool;
        this.container = container;
    }

    public void Init()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = GameObject.Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
            tmp.transform.SetParent(container);
        }
    }

    public GameObject GetPooledObject(Vector3 position, Quaternion rotation = default(Quaternion))
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(true);
                if (pooledObjects[i].transform.childCount > 0)
                {
                    pooledObjects[i].transform.GetChild(Random.Range(0,pooledObjects[i].transform.childCount)).gameObject.SetActive(true);
                }
                pooledObjects[i].transform.position = position;
                pooledObjects[i].transform.rotation = rotation;
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void DeactivateObjects()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (pooledObjects[i].tag == "Coins")
            {
                for (int x = 0; x < pooledObjects[i].transform.childCount; x++)
                {
                    pooledObjects[i].transform.GetChild(x).gameObject.SetActive(false);
                    pooledObjects[i].transform.GetChild(x).GetChild(0).gameObject.SetActive(true);
                }
            }
            pooledObjects[i].SetActive(false);
        }
    }
}
