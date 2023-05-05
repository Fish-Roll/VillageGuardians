using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private Health.Health health;
        [SerializeField] private GameObject healPotion;
        [SerializeField] private float speed;
        private Vector3 _moveDirection;
        private Rigidbody _rb;
        private bool _isTriggered;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            health.Init(OnDeath);
        }

        private void FixedUpdate()
        {
            if(_isTriggered)
                _rb.velocity = -_moveDirection.normalized * speed;
        }

        private void OnDeath()
        {
            int dropHeal = Random.Range(0, 1);
            if (dropHeal == 0)
            {
                Destroy(this.gameObject);
                return;
                
            }

            Instantiate(healPotion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && !_isTriggered)
            {
                _moveDirection = other.transform.position;
            }
        }
    }
}