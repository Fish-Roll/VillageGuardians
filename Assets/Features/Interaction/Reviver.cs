    using System;
using Features.Health;
using UnityEngine;

namespace Features.Interaction
{
    /// <summary>
    /// Отвечает за возрождение игрока. Располагать на возрождаемом игроке
    /// </summary>
    public class Reviver : MonoBehaviour, IInteractable
    {
        private PlayerHealthController _healthController;

        private void Start()
        {
            _healthController = GetComponent<PlayerHealthController>();
        }
        
        public void Interact()
        {
            _healthController.Revive();
        }

    }
}