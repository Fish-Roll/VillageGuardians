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
            _bossHealth.Damage(value);
        }

        private void OnDeath()
        {
            animator.SetTrigger(_deathHash);
        }
        
        private void OnProtect()
        {
            animator.SetBool(_protectHash, true);
            _bossHealth.enabled = false;
            //TODO:Protect
        }

        private void OnStopProtect()
        {
            animator.SetBool(_protectHash, false);
            _bossHealth.enabled = true;
        }
        
        private void OnSpawnEnemy()
        {
            animator.SetTrigger(_spawnEnemyHash);
            //TODO:Spawn state
        }
    }
}