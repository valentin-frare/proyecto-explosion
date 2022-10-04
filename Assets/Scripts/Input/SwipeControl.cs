using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : InputControl
{
    public override void Init(bool returnToZeroAfterStopDragging = false)
    {
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
        startPos = new Vector3(Screen.width/2, position.y, position.z);

        delta = startPos - position;

        steering = (delta.x / (Screen.width / 2));

        acceleration = (delta.y / (Screen.height / 2));

        onDragging = true;
    }

    private void OnDragging(Vector3 position)
    {
        delta = startPos - position;

        acceleration = (delta.y / (Screen.height / 2));

        steering = (delta.x / (Screen.width / 2));
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
