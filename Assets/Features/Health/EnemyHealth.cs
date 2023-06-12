using System;
using Features.Health.Abstract;
using UnityEngine;

namespace Features.Health
{
    public class EnemyHealth : BaseHealth
    {
        public void Init(Action onDeath)
        {
            this.onDeath = onDeath;
        }
        
        public override void Damage(float value)
        {
            currentHealth = Mathf.Clamp(currentHealth - value, 0, MaxHealth);
            if(currentHealth <= 0)
                onDeath.Invoke();
        }
    }
}