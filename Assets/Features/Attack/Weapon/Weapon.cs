using UnityEngine;

namespace Features.Attack.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float damage;
        private bool _hasAlreadyDamaged;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_hasAlreadyDamaged)
            {
                _hasAlreadyDamaged = true;
                other.TryGetComponent(out Health.Health health);
                health.Damage(damage);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            _hasAlreadyDamaged = false;
        }
    }
}