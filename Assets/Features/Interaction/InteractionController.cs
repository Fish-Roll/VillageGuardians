using System;
using System.Collections;
using System.Collections.Generic;
using Features.Input;
using Features.TagsGame;
using UnityEngine;

namespace Features.Interaction
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private float leverDuration;
        [SerializeField] private float supportDuration;
        [SerializeField] private float pushDuration;

        [SerializeField] private GameObject interactionPromptWindow;
        
        private InputSignatory _inputSignatory;
        private IInteractable _interactable;
        private int _reviveHash;
        private int _leverHash;
        private int _knuckleHash;
        
        public void Awake()
        {
            _inputSignatory = GetComponent<InputSignatory>();
            _reviveHash = Animator.StringToHash("Support");
            _leverHash = Animator.StringToHash("Lever");
            _knuckleHash = Animator.StringToHash("Push");
        }

        public IEnumerator HandleInteraction()
        {
            if (_interactable != null)
            {
                if (_interactable.GetType() == typeof(Reviver))
                {
                    _inputSignatory.IsMoving = false;
                    _inputSignatory.IsDashing = false;

                    animator.SetTrigger(_reviveHash);
                    yield return new WaitForSeconds(leverDuration);
                    _inputSignatory.isInteracting = false;

                }
                else if (_interactable.GetType() == typeof(LeverHandler))
                {
                    _inputSignatory.IsMoving = false;
                    _inputSignatory.IsDashing = false;

                    animator.SetTrigger(_leverHash);
                    yield return new WaitForSeconds(supportDuration);
                    _inputSignatory.isInteracting = false;

                }
                else if (_interactable.GetType() == typeof(Knuckle))
                {
                    _inputSignatory.IsMoving = false;
                    _inputSignatory.IsDashing = false;

                    if (gameObject.name == "KeyboardBoy" || gameObject.name == "GamepadBoy")
                    {
                        modelTransform.SetParent(null);
                        transform.SetParent(modelTransform);
                        gameObject.transform.localPosition = Vector3.zero;

                        animator.SetTrigger(_knuckleHash);

                        yield return new WaitForSeconds(pushDuration);

                        transform.SetParent(null);
                        modelTransform.SetParent(transform);
                        modelTransform.transform.localPosition = new Vector3(0, 0, 0);

                        _inputSignatory.isInteracting = false;
                        // Vector3 poss = transform.transform.position;
                        // modelTransform.position = new Vector3(poss.x, 0f, poss.z);
                    }
                    else
                    {
                        animator.SetTrigger(_knuckleHash);
                        yield return new WaitForSeconds(pushDuration);
                        _inputSignatory.isInteracting = false;
                    }
                }

                _interactable.Interact();
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IInteractable>(out var interactable))
            {
                interactionPromptWindow.SetActive(true);
                _interactable = interactable;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out var interactable))
            {
                interactionPromptWindow.SetActive(false);
                _interactable = null;
            }
        }
        
        
    }
}