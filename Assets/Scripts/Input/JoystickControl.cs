using UnityEngine;

public class JoystickControl : InputControl 
{
    [SerializeField] private Joystick joystick;

    public override void Init(bool returnToZeroAfterStopDragging = false)
    {
        this.returnToZeroAfterStopDragging = returnToZeroAfterStopDragging;
    }

    private void Awake() 
    {
        joystick = FindObjectOfType<Joystick>();
    }

    private void Update() 
    {
        if (joystick == null) return;

        onDragging = joystick.Horizontal != 0 || joystick.Vertical != 0;

        steering = -joystick.Horizontal;
        acceleration = -joystick.Vertical;

        delta.y = -joystick.Vertical;

        Debug.Log(joystick.Vertical);
    }
}