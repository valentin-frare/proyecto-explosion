using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VehicleWheels
{
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform frontLeftWheelAxis;
    public Transform frontRightWheelAxis;
    public WheelCollider frontLeftWheelCol;
    public WheelCollider frontRightWheelCol;
    public List<Wheel> backWheels;

    public VehicleWheels(Transform frontLeftWheel, Transform frontRightWheel, Transform backLeftWheel, Transform backRightWheel, WheelCollider frontLeftWheelCol, WheelCollider frontRightWheelCol, List<Wheel> backWheels)
    {
        this.frontLeftWheel = frontLeftWheel;
        this.frontRightWheel = frontRightWheel;
        this.frontLeftWheelCol = frontLeftWheelCol;
        this.frontRightWheelCol = frontRightWheelCol;
        this.backWheels = backWheels;
    }
}