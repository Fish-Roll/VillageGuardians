using System;
using System.Collections;
using Features.Health.Abstract;
using UnityEngine;

namespace Features.Health
{
    public class EnemyHealthController : EnemyBaseHealthController
    {
        private EnemyHealth _enemyHealth;
        
        private bool _isDead;
        public bool IsDead => _isDead;

        public void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyHealth.currentHealth = _enemyHealth.MaxHealth;
            //_enemyHealth.Init(OnDeath);
        }

        public void Init(Action onDeath)
        {
            _enemyHealth.Init(onDeath);
        }
        
        public void Damage(float value)
        {
            if (_isDead) return;
            _enemyHealth.Damage(value);
        }
    }
}