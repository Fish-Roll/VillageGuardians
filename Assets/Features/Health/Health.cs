using System;
using UnityEngine;

namespace Features.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        
        private float _currentHealth;
        private Action _onDeath;
        private Action _onRevive;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void Init(Action onDeath, Action onRevive = null)
        {
            _onDeath = onDeath;
            _onRevive = onRevive;
        }

        public void Damage(float value)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - value, 0, maxHealth);
            if (_currentHealth == 0)
                _onDeath?.Invoke();
        }

        public void Heal(float value)
        {
            if (_currentHealth == 0)
                _onRevive?.Invoke();
            _currentHealth = Mathf.Clamp(_currentHealth + value, 0, maxHealth);
        }
    }
}