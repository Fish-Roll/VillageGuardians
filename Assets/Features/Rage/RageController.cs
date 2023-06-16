using System;
using System.Collections;
using UnityEngine;

namespace Features.Rage
{
    public class RageController : MonoBehaviour
    {
        [SerializeField] private RageModel rageModel;
        [SerializeField] private float subtractValue;
        [SerializeField] private float tick;
        [SerializeField] private Animator animator;
        private RageEffect _rageEffect;
        private WaitForSeconds _tickWait;
        private RageView _rageView;
        private bool _activated;
        private int _rageHash;

        public bool Activated => _activated;
        
        private void Awake()
        {
            _rageHash = Animator.StringToHash("Rage");
            _rageEffect = GetComponent<RageEffect>();
            _rageView = GetComponent<RageView>();
            _rageView.RageSlider.maxValue = rageModel.RageMaxValue;
            _rageView.RageSlider.value = rageModel.RageValue;
        }
        
        private void Start()
        {
            _tickWait = new WaitForSeconds(tick);
        }

        public bool TryAccumulate(float damage)
        {
            if (_activated) return false;
            
            rageModel.Add(damage / 2);
            _rageView.RageSlider.value = rageModel.RageValue;
            return true;
        }
        
        /// <summary>
        /// Запуск ярости при нажатии на кнопку
        /// </summary>
        public bool TryActivate()
        {
            if (rageModel.RageValue < rageModel.RageMaxValue || _activated)
                return false;
            
            _activated = true;
            animator.SetTrigger(_rageHash);
            StartCoroutine(_rageEffect.ActivateEffect());
            StartCoroutine(Activate());
            
            return true;
        }

        private IEnumerator Activate()
        {
            while (rageModel.RageValue > 0)
            {
                rageModel.Subtract(subtractValue);
                _rageView.RageSlider.value = rageModel.RageValue;
                
                yield return _tickWait;
            }
            StartCoroutine(_rageEffect.DisableEffect());
            _activated = false;
        }
    }
}