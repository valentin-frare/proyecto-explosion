using UnityEngine;

public abstract class InputControl : MonoBehaviour 
{
    public bool onDragging;

    protected bool returnToZeroAfterStopDragging;
    protected Vector3 startPos;
    protected Vector3 delta;
    protected float steering;
    protected float acceleration;

    public float Steering => steering;
    public float Acceleration => acceleration;
    public float deltaY => delta.y;

    public abstract void Init(bool returnToZeroAfterStopDragging = false);
}