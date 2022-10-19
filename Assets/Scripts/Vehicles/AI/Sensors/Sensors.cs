using System;
using UnityEngine;

[Serializable]
public class Sensors
{
    private GameObject objectToUseSensors;
    private SensorsConfig sensorsConfig;
    
    public LayerMask layerMask;
    public bool vehicleOnLeft;
    public bool vehicleOnRight;
    public bool vehicleInFront;
    public bool vehicleBehind;
    public bool vehicleFarInFront;

    public Transform transform => objectToUseSensors.transform;

    public Sensors(GameObject objectToUseSensors, SensorsConfig sensorsConfig) 
    {
        this.objectToUseSensors = objectToUseSensors;
        this.sensorsConfig = sensorsConfig;
    }

    public void Init()
    {
        
    }

    public void Update()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            vehicleInFront = Physics.Raycast(transform.position + new Vector3(0.5f,0,0), Vector3.forward, out var hitFront, sensorsConfig.frontMax, layerMask) 
                       || Physics.Raycast(transform.position - new Vector3(0.5f,0,0), Vector3.forward, out var hitFront2, sensorsConfig.frontMax, layerMask);
            vehicleFarInFront = Physics.Raycast(transform.position + new Vector3(0.5f,0,0), Vector3.forward, out var hitFrontFar, sensorsConfig.frontFarMax, layerMask) 
                            || Physics.Raycast(transform.position - new Vector3(0.5f,0,0), Vector3.forward, out var hitFrontFar2, sensorsConfig.frontFarMax, layerMask);
            vehicleBehind = Physics.Raycast(transform.position, Vector3.back, out var hitBehind, sensorsConfig.backMax, layerMask);
            vehicleOnRight = Physics.Raycast(transform.position, Vector3.left, out var hitLeft, sensorsConfig.leftMax, layerMask);
            vehicleOnLeft = Physics.Raycast(transform.position, Vector3.right, out var hitRight, sensorsConfig.rightMax, layerMask);

            if (hitFrontFar.collider != null)
                Debug.Log(hitFrontFar.collider.gameObject.name);
        }
        else if (transform.rotation.eulerAngles.y == 180)
        {
            vehicleInFront = Physics.Raycast(transform.position + new Vector3(0.5f,0,0), Vector3.back, out var hitFront, sensorsConfig.frontMax, layerMask) 
                       || Physics.Raycast(transform.position - new Vector3(0.5f,0,0), Vector3.back, out var hitFront2, sensorsConfig.frontMax, layerMask);
            vehicleFarInFront = Physics.Raycast(transform.position + new Vector3(0.5f,0,0), Vector3.back, out var hitFrontFar, sensorsConfig.frontFarMax, layerMask) 
                            || Physics.Raycast(transform.position - new Vector3(0.5f,0,0), Vector3.back, out var hitFrontFar2, sensorsConfig.frontFarMax, layerMask);
            vehicleBehind = Physics.Raycast(transform.position, Vector3.forward, out var hitBehind, sensorsConfig.backMax, layerMask);
            vehicleOnRight = Physics.Raycast(transform.position, Vector3.right, out var hitRight, sensorsConfig.rightMax, layerMask);
            vehicleOnLeft = Physics.Raycast(transform.position, Vector3.left, out var hitLeft, sensorsConfig.leftMax, layerMask);

            if (hitFrontFar.collider != null)
                Debug.Log(hitFrontFar.collider.gameObject.name);
        }
    }

    public void DebugMode()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            Debug.DrawRay(transform.position + new Vector3(0.5f,0,0), Vector3.forward * sensorsConfig.frontFarMax, vehicleFarInFront ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position - new Vector3(0.5f,0,0), Vector3.forward * sensorsConfig.frontFarMax, vehicleFarInFront ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position + new Vector3(0.5f,0,0), Vector3.forward * sensorsConfig.frontMax, vehicleInFront ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position - new Vector3(0.5f,0,0), Vector3.forward * sensorsConfig.frontMax, vehicleInFront ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position, Vector3.back * sensorsConfig.backMax, vehicleBehind ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position, Vector3.right * sensorsConfig.leftMax, vehicleOnLeft ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position, Vector3.left * sensorsConfig.rightMax, vehicleOnRight ? Color.red : Color.yellow);
        }
        else if (transform.rotation.eulerAngles.y == 180)
        {
            Debug.DrawRay(transform.position + new Vector3(0.5f,0,0), Vector3.back * sensorsConfig.frontFarMax, vehicleFarInFront ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position - new Vector3(0.5f,0,0), Vector3.back * sensorsConfig.frontFarMax, vehicleFarInFront ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position + new Vector3(0.5f,0,0), Vector3.back * sensorsConfig.frontMax, vehicleInFront ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position - new Vector3(0.5f,0,0), Vector3.back * sensorsConfig.frontMax, vehicleInFront ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position, Vector3.forward * sensorsConfig.backMax, vehicleBehind ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position, Vector3.left * sensorsConfig.leftMax, vehicleOnLeft ? Color.red : Color.yellow);
            Debug.DrawRay(transform.position, Vector3.right * sensorsConfig.rightMax, vehicleOnRight ? Color.red : Color.yellow);
        }
    }
}
