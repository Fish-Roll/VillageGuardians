using System;
using System.Collections;
using UnityEngine;

namespace Features.PickingUp
{
    public class HealthPotion : MonoBehaviour, ILifted
    {
        [SerializeField] private float healValue;
        
        private Health.Health _playerHealth;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Health.Health health))
            {
                _playerHealth = health;
            }
        }

        public void Lift()
        {
            _playerHealth.Heal(healValue);
            Destroy(gameObject);
        }
    }
}