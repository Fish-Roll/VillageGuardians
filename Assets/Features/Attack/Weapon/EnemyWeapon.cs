using Features.Health;
using UnityEngine;

namespace Features.Attack.Weapon
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private float damage;
        //private bool _hasAlreadyDamaged;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealthController health))
                health.Damage(damage);
        }

        // private void OnTriggerStay(Collider other)
        // {
        //     if(other.TryGetComponent(out Health.Health health))
        //         health.Damage(damage);
        //
        //     _hasAlreadyDamaged = false;
        // }
    }
}