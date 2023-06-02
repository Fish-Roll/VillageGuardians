using System;
using Features.Interaction;
using Features.PickingUp;
using UnityEngine;

namespace Features.TagsGame
{
    public class Knuckle : MonoBehaviour, ILifted, IInteractable
    {
        [SerializeField] private ushort id;
        private Vector3 _position;

        private Action _onInteract;

        private void Start()
        {
            _position = transform.position;
        }
        
        public void Init(Action onInteract)
        {
            _onInteract = onInteract;
        }
        
        /// <summary>
        /// Move cube to game pole
        /// </summary>
        public void Lift()
        {
            
        }

        public void Interact()
        {
            _onInteract.Invoke();
        }
        
    }
}