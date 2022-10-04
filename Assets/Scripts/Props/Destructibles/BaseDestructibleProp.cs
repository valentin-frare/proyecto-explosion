using UnityEngine;

public class BaseDestructibleProp : MonoBehaviour, IDestructibleProp
{
    [SerializeField] protected ParticleSystem particles;

    public virtual void Destroy()
    {
        particles.Play();
    }
}