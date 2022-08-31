using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    public bool onDragging;

    private Vector3 startPos;
    private Vector3 delta;
    private float steering;
    private float acceleration;
    private float maxSteeringAngle;
    private float maxTorque;

    public float Steering => steering;
    public float Acceleration => acceleration;

    public void Init(float maxSteeringAngle, float maxTorque)
    {
        this.maxSteeringAngle = maxSteeringAngle;
        this.maxTorque = maxTorque;
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
        startPos = position;

        delta = startPos - position;

        steering = (delta.x / Screen.width) * maxSteeringAngle;

        acceleration = (delta.y / Screen.height) * maxTorque;

        onDragging = true;
    }

    private void OnDragging(Vector3 position)
    {
        delta = startPos - position;

        acceleration = (delta.y / Screen.height) * maxTorque;

        steering = (delta.x / Screen.width) * maxSteeringAngle;
    } 

    private void OnStopDragging()
    {
        startPos = new Vector3(0,0,0);

        onDragging = false;
    }
}
