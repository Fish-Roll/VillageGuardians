using System;
using UnityEngine;

namespace Features.Interaction
{
    public class Interaction : MonoBehaviour
    {
        public bool canInteract;
        public Health.Health otherPlayerHealth;
        [SerializeField] private Animator _animator;
        private int supportHash;
        private void Awake()
        {
            supportHash = Animator.StringToHash("IsSupporting");
            //_animator = GetComponentInChildren<Animator>();
        }

        public void Interact()
        {
            if (canInteract)
            {
                _animator.SetTrigger(supportHash);
                otherPlayerHealth.Revive();
            }
        }
    }
}