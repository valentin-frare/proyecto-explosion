using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VehicleWheels
{
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public List<Wheel> backWheels;

    public VehicleWheels(Transform frontLeftWheel, Transform frontRightWheel, List<Wheel> backWheels)
    {
        this.frontLeftWheel = frontLeftWheel;
        this.frontRightWheel = frontRightWheel;
        this.backWheels = backWheels;
    }
}