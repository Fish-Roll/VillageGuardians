using System;
using System.Collections;
using Features.Health.Abstract;
using UnityEngine;

namespace Features.Health
{
    public class EnemyHealthController : EnemyBaseHealthController
    {
        private EnemyHealth _enemyHealth;
        private HealthView _healthView;
        private bool _isDead;
        public bool IsDead => _isDead;

        public void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _healthView = GetComponent<HealthView>();

            _enemyHealth.currentHealth = _enemyHealth.MaxHealth;
            
            _healthView.HealthSlider.maxValue = _enemyHealth.MaxHealth;
            _healthView.HealthSlider.value = _enemyHealth.CurrentHealth;
            _healthView.HealthSlider2.maxValue = _enemyHealth.MaxHealth;
            _healthView.HealthSlider2.value = _enemyHealth.CurrentHealth;
            
            //_enemyHealth.Init(OnDeath);
        }

        public void Init(Action onDeath)
        {
            _enemyHealth.Init(onDeath);
        }
        
        public override void Damage(float value)
        {
            if (_isDead) return;
            _enemyHealth.Damage(value);
            _healthView.HealthSlider.value = _enemyHealth.CurrentHealth;
            _healthView.HealthSlider2.value = _enemyHealth.CurrentHealth;
        }
    }
}