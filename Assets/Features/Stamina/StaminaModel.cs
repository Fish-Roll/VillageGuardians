using System;
using UnityEngine;

namespace Features.Stamina
{
    [Serializable]
    public class StaminaModel
    {
        [SerializeField] private float maxStamina;

        public float MaxStamina => maxStamina;
        public float CurrentStamina { get; set; }

        public void Add(float value)
        {
            CurrentStamina = Mathf.Clamp(CurrentStamina + value, 0, maxStamina);
        }
        
        public void Subtract(float value)
        {
            CurrentStamina = Mathf.Clamp(CurrentStamina - value, 0, maxStamina);
        }

    }
}