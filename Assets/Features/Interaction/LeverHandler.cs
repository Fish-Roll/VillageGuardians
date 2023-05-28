using System;
using UnityEngine;

namespace Features.Interaction
{
    [RequireComponent(typeof(Lever))]
    public class LeverHandler: MonoBehaviour, IInteractable
    {
        private bool _isActivated;
        
        public void Interact()
        {
            if (!_isActivated)
            {
                _isActivated = true;
                GetComponent<Lever>().enabled = true;
            }
        }

    }
}