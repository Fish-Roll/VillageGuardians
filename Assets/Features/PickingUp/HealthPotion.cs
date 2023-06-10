using System;
using System.Collections;
using Features.Health;
using UnityEngine;

namespace Features.PickingUp
{
    public class HealthPotion : MonoBehaviour, ILifted
    {
        [SerializeField] private float healValue;
        
        private PlayerHealthController _playerHealth;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealthController health))
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