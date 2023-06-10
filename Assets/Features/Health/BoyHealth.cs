using Features.Health.Abstract;
using Features.Rage;
using UnityEngine;

namespace Features.Health
{
    public class BoyHealth : PlayerBaseHealth
    {
        [SerializeField] private RageController rageController;
        
        public override void Damage(float value)
        {
            if (rageController.Activated)
                value /= 2;
            
            currentHealth = Mathf.Clamp(currentHealth - value, 0, MaxHealth);
            rageController.TryAccumulate(value);
            
            if(currentHealth <= 0)
                onDeath.Invoke();
        }
    }
}