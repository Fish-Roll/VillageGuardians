using System;
using UnityEngine;

namespace Features.Health
{
    public class HealthPotion : MonoBehaviour
    {
        [SerializeField] private float healValue;

        public static Action OnHealByPotion;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Health>().Heal(healValue);
                OnHealByPotion.Invoke();
                Destroy(this.gameObject);
            }
        }
    }
}