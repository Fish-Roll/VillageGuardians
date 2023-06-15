using System.Collections;
using Features.Health.Abstract;
using UnityEngine;

namespace Features.Attack.Weapon
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float lifetime;
        
        private Vector3 _moveDirection;
        private bool _hasDamage;
        
        private void Start()
        {
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
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (TryGetComponent(out EnemyBaseHealthController health) && !_hasDamage)
            {
                health.Damage(damage);
                _hasDamage = true;
            }
            Destroy(gameObject);
        }

        private IEnumerator KillObject()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(this.gameObject);
        }
    }
}
