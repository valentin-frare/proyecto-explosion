using UnityEngine;
using UnityEngine.Events;

public enum CollisionType
{
    CivilCar,
    Objects
}

public class OnCollision : MonoBehaviour 
{
    [SerializeField] private UnityEvent onCollision;
    [SerializeField] private CollisionType collisionType;
    [SerializeField] private GameObject go;

    private void OnCollisionEnter(Collision other) 
    {
        onCollision.Invoke();

        foreach (ContactPoint contact in other.contacts)
        {
            IDamageable damageable = contact.otherCollider.gameObject.GetComponentInChildren<IDamageable>();

            if (damageable != null)
            {
                switch (collisionType)
                {
                    case CollisionType.CivilCar:
                        EndLevelCoins.instance.AddDestructionCoins(25);
                        go.SetActive(false);
                        break;
                    case CollisionType.Objects:
                        EndLevelCoins.instance.AddDestructionCoins(10);
                        break;
                    default:
                        break;
                }
                damageable.Damage();
            }
        }
    }

    public void DestroyCollider()
    {
        Destroy(GetComponent<Collider>());
    }
}