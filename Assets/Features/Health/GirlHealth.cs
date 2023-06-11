using Features.Health.Abstract;
using UnityEngine;

namespace Features.Health
{
    public class GirlHealth : PlayerBaseHealth
    {
        public override void Damage(float value)
        {
            currentHealth = Mathf.Clamp(currentHealth - value, 0, MaxHealth);
            
            if(currentHealth <= 0)
                onDeath.Invoke();
        }
    }
}