using UnityEngine;

public class DamageReciver : MonoBehaviour, IDamageable
{
    public IDamageable vehicle;

    public void Damage(int damage) 
    {
        vehicle.Damage(damage);
    }
}