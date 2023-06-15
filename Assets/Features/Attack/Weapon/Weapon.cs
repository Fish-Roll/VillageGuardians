using Features.Health;
using Features.Health.Abstract;
using UnityEngine;

namespace Features.Attack.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float damage;
        //private bool _hasAlreadyDamaged;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wall"))
                other.gameObject.SetActive(false);
            if (other.TryGetComponent(out EnemyBaseHealthController health))
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