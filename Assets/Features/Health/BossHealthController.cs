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

        private int _deathHash;
        private int _protectHash;
        private int _spawnEnemyHash;
        private Action onProtect;
        private bool _isProtected;
        public bool _isDead;
        
        public void Awake()
        {
            _bossHealth = GetComponent<BossHealth>();
        }

        public void Init(Action onDeath, Action onProtect)
        {
            _bossHealth.Init(onDeath);
            this.onProtect = onProtect;
        }

        private bool _alreadyProtected;
        public override void Damage(float value)
        {
            if (_isDead || _isProtected) return;
            _bossHealth.Damage(value);
            if (_bossHealth.CurrentHealth <= _bossHealth.MaxHealth/2 && !_alreadyProtected)
            {
                _alreadyProtected = true;
                onProtect.Invoke();
            }
        }

        public void OnProtect(bool isProtected)
        {
            _isProtected = isProtected;
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