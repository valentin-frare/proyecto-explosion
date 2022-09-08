using System;
using UnityEngine;

[Serializable]
public class Sensors
{
    private GameObject objectToUseSensors;
    private SensorsConfig sensorsConfig;

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
        vehicleInFront = Physics.Raycast(transform.position + new Vector3(0.9f,0,0), transform.TransformDirection(Vector3.back), out var hitFront, sensorsConfig.frontMax) || Physics.Raycast(transform.position - new Vector3(0.9f,0,0), transform.TransformDirection(Vector3.back), out var hitFront2, sensorsConfig.frontMax);
        vehicleFarInFront = Physics.Raycast(transform.position + new Vector3(0.9f,0,0), transform.TransformDirection(Vector3.back), out var hitFrontFar, sensorsConfig.frontFarMax) || Physics.Raycast(transform.position - new Vector3(0.9f,0,0), transform.TransformDirection(Vector3.back), out var hitFrontFar2, sensorsConfig.frontFarMax);
        vehicleBehind = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hitBehind, sensorsConfig.backMax);
        vehicleOnRight = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out var hitRight, sensorsConfig.rightMax);
        vehicleOnLeft = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out var hitLeft, sensorsConfig.leftMax);
    }

    public void DebugMode()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * sensorsConfig.frontMax, Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * sensorsConfig.backMax, Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * sensorsConfig.leftMax, Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * sensorsConfig.rightMax, Color.yellow);
    }
}
