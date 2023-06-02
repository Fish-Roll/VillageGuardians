using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Features.Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private Health.Health health;
        [SerializeField] private GameObject healPotion;
        [SerializeField] private Animator animator;
        [SerializeField] private float speed;
        [SerializeField] private Transform[] points;
        [SerializeField] private NavMeshAgent agent;
        private bool _isPatrolling = true;

        private int _randomPoint;
        private int _deathHash;
        private Vector3 _moveDirection;
        private Rigidbody _rb;
        private bool _isTriggered;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            health.Init(OnDeath);
            _deathHash = Animator.StringToHash("Death");
            agent.speed = speed;
            _randomPoint = Random.Range(0, points.Length - 1);
            Patroll();
        }

        private void Update()
        {
            //if(_isPatrolling)
        }
        
        // private void FixedUpdate()
        // {
        //     if(_isTriggered)
        //         _rb.velocity = -_moveDirection.normalized * speed;
        // }

        private void OnDeath()
        {
            animator.SetTrigger(_deathHash);
            int dropHeal = Random.Range(0, 1);
            if (dropHeal == 0)
            {
                //Destroy(this.gameObject);
                return;
            }

            Vector3 potionPlace = transform.forward;
            potionPlace.z += 2;
            Instantiate(healPotion, potionPlace, Quaternion.identity);
            //Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            _isPatrolling = false;
            if (other.CompareTag("Player"))
            {
                agent.Move(other.transform.position);
                //_moveDirection = other.transform.position;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _isPatrolling = true;
        }

        private void Patroll()
        {
            agent.Move(points[_randomPoint].position);
            
            if (Vector3.Distance(points[_randomPoint].position, transform.position) == 0.01f)
            {
                _randomPoint = Random.Range(0, points.Length);
            }
        }
    }
}