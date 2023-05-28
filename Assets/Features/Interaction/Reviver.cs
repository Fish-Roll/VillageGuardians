using System;
using UnityEngine;

namespace Features.Interaction
{
    /// <summary>
    /// Отвечает за возрождение игрока. Располагать на возрождаемом игроке
    /// </summary>
    public class Reviver : MonoBehaviour, IInteractable
    {
        [SerializeField] private Health.Health health;
        public void Interact()
        {
            health.Revive();
        }

    }
}