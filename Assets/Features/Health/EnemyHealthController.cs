using Features.Health.Abstract;
using UnityEngine;

namespace Features.Health
{
    public class EnemyHealthController : EnemyBaseHealthController
    {
        private EnemyHealth _enemyHealth;
        [SerializeField] private Animator animator;

        private int _deathHash;
        
        private bool _isDead;
        public bool IsDead => _isDead;

        public void Start()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _deathHash = Animator.StringToHash("Die");
            _enemyHealth.Init(OnDeath);
        }

        public void Damage(float value)
        {
            if (_isDead) return;
            _enemyHealth.Damage(value);
        }
        
        private void OnDeath()
        {
            _isDead = true;
            animator.SetTrigger(_deathHash);
        }
        
    }
}