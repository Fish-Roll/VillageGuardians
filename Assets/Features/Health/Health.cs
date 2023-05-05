﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float revivedHealth;

        [SerializeField] private float _currentHealth;
        
        [SerializeField] private Slider leftHealth;
        [SerializeField] private Slider rightHealth;
        
        private bool _isDead;
        
        private Action _onDeath;
        private Action _onRevive;

        private void Awake()
        {
            _currentHealth = maxHealth;
            
            leftHealth.maxValue = maxHealth;
            rightHealth.maxValue = maxHealth;
            
            leftHealth.value = maxHealth;
            rightHealth.value = maxHealth;
            
        }

        public void Init(Action onDeath, Action onRevive = null)
        {
            _onDeath = onDeath;
            _onRevive = onRevive;
        }

        public void Damage(float value)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - value, 0, maxHealth);
            leftHealth.value = _currentHealth;
            rightHealth.value = _currentHealth;
            if (_currentHealth == 0 && !_isDead)
            {
                _isDead = true;
                _onDeath?.Invoke();
            }
        }

        public void Heal(float value)
        {
            if (_currentHealth == 0) return;

            leftHealth.value = _currentHealth;
            rightHealth.value = _currentHealth;

            _currentHealth = Mathf.Clamp(_currentHealth + value, 0, maxHealth);
        }

        public void Revive()
        {
            if (!_isDead) return;
            
            _currentHealth = Mathf.Clamp(revivedHealth, 0, maxHealth);
            leftHealth.value = _currentHealth;
            rightHealth.value = _currentHealth;

            _isDead = false;
            _onRevive.Invoke();
        }
    }
}