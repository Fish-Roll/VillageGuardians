using System;
using System.Collections;
using UnityEngine;

namespace Features.Health.Abstract
{
    public abstract class BaseEnemyHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        
        public float currentHealth;
        protected Action onDeath;
        protected bool isDead;
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;

        public abstract void Damage(float value);
    }
}