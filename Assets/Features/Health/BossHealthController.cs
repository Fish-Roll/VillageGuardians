using System;
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

        private bool _isDead;
        public bool IsDead => _isDead;
        
        public void Start()
        {
            _bossHealth = GetComponent<BossHealth>();
            _deathHash = Animator.StringToHash("Die");
            _protectHash = Animator.StringToHash("IsSleep");
            _spawnEnemyHash = Animator.StringToHash("Intimidate_3");
            _bossHealth.Init(OnProtect, OnSpawnEnemy, OnDeath);
        }

        public void Damage(float value)
        {
            if (_isDead) return;
            _bossHealth.Damage(value);
        }

        private void OnDeath()
        {
            _isDead = true;
            animator.SetTrigger(_deathHash);
        }
        
        private void OnProtect()
        {
            if (_isDead) return;
            animator.SetBool(_protectHash, true);
            //TODO:Protect
        }

        private void OnStopProtect()
        {
            animator.SetBool(_protectHash, false);
            enabled = true;
        }
        
        private void OnSpawnEnemy()
        {
            if (_isDead) return;
            animator.SetTrigger(_spawnEnemyHash);
            enabled = false;
            //TODO:Spawn state
        }
    }
}