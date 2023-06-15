using System.Collections;
using System.Timers;
using Features.Health;
using UnityEngine;

namespace Features.Attack.Weapon
{
    public class EnemyFireball : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float lifetime;
        
        private Vector3 _moveDirection;
        
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
            //transform.position = _moveDirection.normalized * speed * Time.deltaTime;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealthController health))
                health.Damage(damage);
            Destroy(gameObject);
        }

        private IEnumerator KillObject()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }
    }
}