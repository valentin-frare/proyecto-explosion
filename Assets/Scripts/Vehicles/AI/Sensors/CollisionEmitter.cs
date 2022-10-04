using UnityEngine;

public enum ColliderType
{
    Enemy,
    Player
}

public class CollisionEmitter : MonoBehaviour
{
    [SerializeField] private ColliderType collisionType;
    private void OnCollisionEnter(Collision other) => GameEvents.OnCarCollision.Invoke(other.contacts, collisionType);
}