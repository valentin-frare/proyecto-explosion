using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour 
{
    [SerializeField] private UnityEvent onCollision;

    private void OnCollisionEnter(Collision other) 
    {
        onCollision.Invoke();

        foreach (ContactPoint contact in other.contacts)
        {
            IDamageable damageable = contact.otherCollider.gameObject.GetComponentInChildren<IDamageable>();

            Debug.Log(other.collider.gameObject);

            if (damageable != null)
                damageable.Damage();
        }
    }

    public void DestroyCollider()
    {
        Destroy(GetComponent<Collider>());
    }
}