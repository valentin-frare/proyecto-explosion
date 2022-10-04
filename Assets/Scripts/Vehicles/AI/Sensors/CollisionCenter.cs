using System;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCenter : MonoBehaviour 
{
    [SerializeField] private UnityEvent<ContactPoint[]> OnEnemyCollides;
    [SerializeField] private UnityEvent<ContactPoint[]> OnPlayerCollides;

    private void Awake() 
    {
        GameEvents.OnCarCollision += HandleCollision;
    }

    private void HandleCollision(ContactPoint[] contactPoints, ColliderType colliderType)
    {
        Debug.Log(colliderType);
        switch (colliderType)
        {
            case ColliderType.Enemy:
                OnEnemyCollides.Invoke(contactPoints);
                break;
            case ColliderType.Player:
                OnPlayerCollides.Invoke(contactPoints);
                break;
            default:
                break;
        }
    }
}