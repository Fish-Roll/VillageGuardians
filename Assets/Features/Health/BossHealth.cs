using System;
using Features.Health.Abstract;
using UnityEngine;

namespace Features.Health
{
    public class BossHealth : BaseHealth
    {
        [SerializeField] private int countProtect;
        
        private Action _onProtectState;
        private Action _onSpawnState;

        public void Init(Action onProtectState, Action onSpawnState, Action onDeath)
        {
            _onProtectState = onProtectState;
            _onSpawnState = onSpawnState;
            this.onDeath = onDeath;
        }
        
        public override void Damage(float value)
        {
            currentHealth = Mathf.Clamp(currentHealth - value, 0, MaxHealth);
            if (MaxHealth / countProtect < currentHealth)
            {
                _onProtectState.Invoke();
                _onSpawnState.Invoke();
            }
            
            if(currentHealth <= 0)
                onDeath.Invoke();
        }
    }
}