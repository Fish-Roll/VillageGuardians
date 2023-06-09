using System;
using System.Collections;
using UnityEngine;

namespace Features.Attack.Weapon
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float lifetime;
        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private bool _hasDamage;
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            StartCoroutine(KillObject());
        }

        public void Init(Vector3 moveDirection)
        {
            _moveDirection = moveDirection;
        }
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(_moveDirection * Time.deltaTime * speed, Space.Self);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") 
                && TryGetComponent(out Health.Health health) && !_hasDamage)
            {
                health.Damage(damage);
                _hasDamage = true;
            }
            Destroy(this.gameObject);
        }

        private IEnumerator KillObject()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(this.gameObject);
        }
    }
}
