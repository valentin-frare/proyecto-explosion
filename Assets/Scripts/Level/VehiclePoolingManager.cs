using UnityEngine;

public class VehiclePoolingManager : PoolingManager 
{
    public VehiclePoolingManager(GameObject objectToPool, int amountToPool, Transform container = null) : base(objectToPool, amountToPool, container)
    {   
    }

    public override GameObject GetPooledObject(Vector3 position, Quaternion rotation = default)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                IVehicleMovement movement = pooledObjects[i].GetComponent<VehicleAiController>().vehicleMovement;

                if (movement != null)
                {
                    pooledObjects[i].SetActive(true);
                    movement.Teleport(position, rotation);
                }

                return pooledObjects[i];
            }
        }
        return null;
    }
}