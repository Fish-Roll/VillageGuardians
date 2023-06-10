using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Health.Abstract
{
    public abstract class PlayerBaseHealth : BaseHealth
    {
        protected Action onRevive;

        private void Awake()
        {
            currentHealth = MaxHealth;
        }

        public void Init(Action onDeath, Action onRevive)
        {
            this.onDeath = onDeath;
            this.onRevive = onRevive;
        }
        
        public void Heal(float value)
        {
            currentHealth = Mathf.Clamp(currentHealth + value, 0, MaxHealth);
        }

        public void Revive(float reviveHealth)
        {
            currentHealth = reviveHealth;
            onRevive.Invoke();
        }
    }
}