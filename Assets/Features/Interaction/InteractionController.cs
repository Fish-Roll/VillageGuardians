using System;
using Features.TagsGame;
using UnityEngine;

namespace Features.Interaction
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private float duration;
        
        private IInteractable _interactable;
        private int _reviveHash;
        private int _leverHash;
        private int _knuckleHash;
        
        public void Awake()
        {
            _reviveHash = Animator.StringToHash("Support");
            _leverHash = Animator.StringToHash("Lever");
            _knuckleHash = Animator.StringToHash("Push");
        }

        public void HandleInteraction()
        {
            if (_interactable != null)
            {
                if (_interactable.GetType() == typeof(Reviver))
                    animator.SetTrigger(_reviveHash);
                else if(_interactable.GetType() == typeof(LeverHandler))
                    animator.SetTrigger(_leverHash);
                else if (_interactable.GetType() == typeof(Knuckle))
                {
                    Transform parent = transform;
                    modelTransform.SetParent(null);
                    transform.SetParent(modelTransform);

                    animator.SetTrigger(_knuckleHash);
                    
                    transform.SetParent(null);
                    modelTransform.SetParent(transform);
                    Vector3 poss = transform.transform.position;
                    modelTransform.position = new Vector3(poss.x, 0f, poss.z);
                }

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
        
        
    }
}