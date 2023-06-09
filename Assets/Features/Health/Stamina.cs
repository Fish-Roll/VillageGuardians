using System;
using System.Collections;
using UnityEngine;

namespace Features.Health
{
    public class Stamina : MonoBehaviour
    {
        [SerializeField] private float maxStamina;
        [SerializeField] private float currStamina;
        [SerializeField] private float addedStamina;
        
        [SerializeField] private float regenDelay;

        private bool _attack;
        private void Awake()
        {
            currStamina = maxStamina;
        }

        public bool SubStamina(float value)
        {
            _attack = true;
            StartCoroutine(RegenerationStamina());
            if (currStamina - value < 0)
                return false;
            currStamina -= value;
            return true;
        }
        
        private IEnumerator RegenerationStamina()
        {
            yield return new WaitForSeconds(regenDelay);
            while (currStamina < maxStamina && !_attack)
            {
                currStamina = Mathf.Clamp(currStamina + addedStamina, currStamina, maxStamina);
                yield return null;
            }
        }
    }
}