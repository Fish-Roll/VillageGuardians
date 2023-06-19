using System;
using Features.Health.Abstract;
using Features.Input;
using UnityEngine;

namespace Features.Health
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private PlayerBaseHealth health;
        [SerializeField] private ParticleSystem healParticle;
        [SerializeField] private Animator animator;
        
        [SerializeField] private AudioSource deathSound;
        [SerializeField] private AudioSource reviveSound;
        
        [SerializeField] private float reviveHealth;
        [SerializeField] private DeathWindow deathWindow;
        [SerializeField] private GameObject deathWindowObject;
        
        private HealthView _healthView;
        private bool _isDead;
        private int _deathHash;
        private int _reviveHash;
        private InputSignatory _inputSignatory;
        public bool IsDead => _isDead;

        public void Start()
        {

            _reviveHash = Animator.StringToHash("Revival");
            _deathHash = Animator.StringToHash("Death");
            
            _healthView = GetComponent<HealthView>();
            _healthView.HealthSlider.maxValue = health.MaxHealth;
            _healthView.HealthSlider.value = health.CurrentHealth;
            _inputSignatory = GetComponent<InputSignatory>();
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
            if (_isDead) return;
            _isDead = true;
            deathWindow.deadCount++;
            _inputSignatory.MoveDirection = Vector3.zero;

            animator.SetLayerWeight(1, 0);
            
            if(!deathSound.isPlaying)
                deathSound.Play();
            
            animator.SetTrigger(_deathHash);
            _inputSignatory.IsAiming = false;
            _inputSignatory.IsMoving = false;
            _inputSignatory.IsDashing = false;
            
            _inputSignatory.enabled = false;
            if (deathWindow.deadCount == 2)
                deathWindowObject.SetActive(true);
        }

        private void OnRevive()
        {
            if (!_isDead) return;
            deathWindow.deadCount--;
            _isDead = false;
            if(!reviveSound.isPlaying)
                reviveSound.Play();
            animator.SetTrigger(_reviveHash);
            _inputSignatory.enabled = true;
        }
    }
}