using System;
using System.Collections;
using Features.Health.Abstract;
using UnityEngine;

namespace Features.Health
{
    public class BossHealthController : EnemyBaseHealthController
    {
        private BossHealth _bossHealth;
        [SerializeField] private Animator animator;

        private int _maxCountSpawns;
        private int _countSpawns = 1;
        private int _deathHash;
        private int _protectHash;
        private int _spawnEnemyHash;
        private Func<IEnumerator> onProtect;
        private bool _isDead;
        public bool IsDead => _isDead;
        
        public void Awake()
        {
            _bossHealth = GetComponent<BossHealth>();
        }

        public void Init(Action onDeath, Func<IEnumerator> onProtect, int countSpawns)
        {
            _bossHealth.Init(onDeath);
            _maxCountSpawns = countSpawns;
        }
        
        public void Damage(float value)
        {
            if (_isDead) return;
            _bossHealth.Damage(value);
            if (_bossHealth.MaxHealth / _countSpawns < _bossHealth.CurrentHealth)
            {
                _countSpawns++;
                onProtect.Invoke();
            }
        }

        private void OnDeath()
        {
            _isDead = true;
            //animator.SetTrigger(_deathHash);
        }
        
        private void OnProtect()
        {
            if (_isDead) return;
            //animator.SetBool(_protectHash, true);
            //TODO:Protect
        }

        private void OnStopProtect()
        {
            //animator.SetBool(_protectHash, false);
            enabled = true;
        }
        
        private void OnSpawnEnemy()
        {
            if (_isDead) return;
            //animator.SetTrigger(_spawnEnemyHash);
            enabled = false;
            //TODO:Spawn state
        }
    }
}