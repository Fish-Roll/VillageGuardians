using System;
using Features.Health.Abstract;
using UnityEngine;

namespace Features.Health
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private PlayerBaseHealth health;
        [SerializeField] private ParticleSystem healParticle;
        [SerializeField] private Animator animator;
        
        [SerializeField] private float reviveHealth;


        private HealthView _healthView;
        private bool _isDead;
        private int _deathHash;
        private int _reviveHash;
        
        public bool IsDead => _isDead;

        public void Start()
        {

            _reviveHash = Animator.StringToHash("Revival");
            _deathHash = Animator.StringToHash("Death");
            
            _healthView = GetComponent<HealthView>();
            _healthView.HealthSlider.maxValue = health.MaxHealth;
            _healthView.HealthSlider.value = health.CurrentHealth;
            
            health.Init(OnDeath, OnRevive);
        }

        public void Damage(float value)
        {
            if (_isDead) return;
            health.Damage(value);
            _healthView.HealthSlider.value = health.CurrentHealth;
        }

        public void Heal(float value)
        {
            if (_isDead) return;
            healParticle.Play();
            health.Heal(value);
            _healthView.HealthSlider.value = health.CurrentHealth;
        }

        public void Revive()
        {
            if (!_isDead) return;
            healParticle.Play();
            health.Revive(reviveHealth);
            _healthView.HealthSlider.value = health.CurrentHealth;
        }

        private void OnDeath()
        {
            _isDead = true;
            animator.SetTrigger(_deathHash);
        }

        private void OnRevive()
        {
            _isDead = false;
            animator.SetTrigger(_reviveHash);
        }
    }
}