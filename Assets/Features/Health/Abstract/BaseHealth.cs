using System;
using UnityEngine;

namespace Features.Health.Abstract
{
    public abstract class BaseHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        
        protected float currentHealth;
        protected Action onDeath;
        protected bool isDead;
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;

        public abstract void Damage(float value);
    }
}