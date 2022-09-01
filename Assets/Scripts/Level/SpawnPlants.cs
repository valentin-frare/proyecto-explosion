using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    private List<PoolingManager> poolingManagers;
    [SerializeField]
    private List<GameObject> allObjects;
    [SerializeField]
    private int amount;
    // Start is called before the first frame update
    void Start()
    {
        poolingManagers = new List<PoolingManager>();
        foreach (GameObject obj in allObjects)
        {
            PoolingManager pm = new PoolingManager(obj, amount);
            pm.Init();
            poolingManagers.Add(pm);
        }

        //InvokeRepeating("ActivateObject", 1.0f, 3.0f);
    }

    private void ActivateObject(){
        //Vector3

        //poolingManagers[1].GetPooledObject()
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
