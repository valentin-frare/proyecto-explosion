using UnityEngine;

public class TrailController
{
    private TrailRenderer trail;
    private float width = 0.1f;

    public TrailController(TrailRenderer trail)
    {
        this.trail = trail;
    }

    public void Update()
    {
        if (trail.widthCurve.keys[0].value < width)
        {
            trail.widthCurve.keys[0].value += Time.fixedDeltaTime;
        }
        else if (trail.widthCurve.keys[0].value > width)
        {
            trail.widthCurve.keys[0].value -= Time.fixedDeltaTime;
        }
    }

    public void SetTrailWidth(float width)
    {
        this.width = width;
    }
}