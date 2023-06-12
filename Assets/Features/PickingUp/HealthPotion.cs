using System;
using System.Collections;
using Features.Health;
using Features.Stamina;
using UnityEngine;

namespace Features.PickingUp
{
    public class HealthPotion : MonoBehaviour, ILifted
    {
        [SerializeField] private float healValue;
        [SerializeField] private float staminaValue;
        
        private PlayerHealthController _playerHealth;
        private StaminaController _staminaController;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealthController health))
            {
                _playerHealth = health;
            }
            if (other.TryGetComponent(out StaminaController stamina))
            {
                _staminaController = stamina;
            }
            
        }

        public void Lift()
        {
            _playerHealth.Heal(healValue);
            _staminaController.Accumulate(staminaValue);
            Destroy(gameObject);
        }
    }
}