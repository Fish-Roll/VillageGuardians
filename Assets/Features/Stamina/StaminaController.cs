using System;
using UnityEngine;

namespace Features.Stamina
{
    public class StaminaController : MonoBehaviour
    {
        [SerializeField] private StaminaModel stamina;
        
        private StaminaView _staminaView;

        public void Awake()
        {
            _staminaView = GetComponent<StaminaView>();
            stamina.CurrentStamina = stamina.MaxStamina;
            
            _staminaView.StaminaSlider.maxValue = stamina.MaxStamina;
            _staminaView.StaminaSlider.value = stamina.CurrentStamina;
        }

        public void Accumulate(float value)
        {
            stamina.Add(value);
            _staminaView.StaminaSlider.value = stamina.CurrentStamina;
        }

        public void Subtract(float value)
        {
            stamina.Subtract(value);
            _staminaView.StaminaSlider.value = stamina.CurrentStamina;
        }
        
    }
}