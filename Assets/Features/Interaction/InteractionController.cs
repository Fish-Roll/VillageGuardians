using System;
using UnityEngine;

namespace Features.Interaction
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private IInteractable _interactable;
        private int _reviveHash;
        private int _leverHash;
        
        public void Awake()
        {
            _reviveHash = Animator.StringToHash("IsSupport");
            _leverHash = Animator.StringToHash("IsLever");
        }

        public void HandleInteraction()
        {
            if (_interactable != null)
            {
                if (_interactable.GetType() == typeof(Reviver))
                    animator.SetTrigger(_reviveHash);
                else if(_interactable.GetType() == typeof(LeverHandler))
                    animator.SetTrigger(_leverHash);    
                _interactable.Interact();
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IInteractable>(out var interactable))
            {
                _interactable = interactable;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out var interactable))
                _interactable = null;
        }

        private void OnDestroyInteractable()
        {
            _interactable = null;
        }
    }
}