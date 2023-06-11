using System;
using UnityEngine;

namespace Features.Attack.Boss
{
    public class ProtectionState : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        //[SerializeField] private Health.Health health;

        private int _protectionHash;
        private void Start()
        {
            _protectionHash = Animator.StringToHash("");
        }

        /// <summary>
        /// Призыв мобов и переход в защиту
        /// </summary>
        public void Protect()
        {
            animator.SetTrigger(_protectionHash);
            //health.enabled = false;
            
        }
        /// <summary>
        /// Отключение защиты
        /// </summary>
        public void StopProtect()
        {
            //health.enabled = true;
            
        }
    }
}