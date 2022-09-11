using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    public bool onDragging;

    private bool returnToZeroAfterStopDragging;
    private Vector3 startPos;
    private Vector3 delta;
    private float steering;
    private float acceleration;
    private float maxSteeringAngle;
    private float maxTorque;

    public float Steering => steering;
    public float Acceleration => acceleration;
    public float deltaY => delta.y;
    public float MaxTorque => maxTorque;

    public void Init(float maxSteeringAngle, float maxTorque, bool returnToZeroAfterStopDragging = false)
    {
        this.maxSteeringAngle = maxSteeringAngle;
        this.maxTorque = maxTorque;
        this.returnToZeroAfterStopDragging = returnToZeroAfterStopDragging;
    }

    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                OnStartDragging(touch.position);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                OnDragging(touch.position);
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                OnStopDragging();
            }
        } 
    }

    private void OnStartDragging(Vector3 position)
    {
        startPos = new Vector3(Screen.width/2, position.y, 0);

        delta = startPos - position;

        steering = (delta.x / (Screen.width / 2)) * maxSteeringAngle;

        acceleration = (delta.y / (Screen.height / 5)) * maxTorque;

        onDragging = true;
    }

    private void OnDragging(Vector3 position)
    {
        delta = startPos - position;

        acceleration = (delta.y / (Screen.height / 5)) * maxTorque;

        steering = (delta.x / (Screen.width / 2)) * maxSteeringAngle;
    } 

    private void OnStopDragging()
    {
        startPos = new Vector3(0,0,0);

        if (returnToZeroAfterStopDragging)
        {
            acceleration = 0;
            steering = 0;
        }

        onDragging = false;
    }
}
