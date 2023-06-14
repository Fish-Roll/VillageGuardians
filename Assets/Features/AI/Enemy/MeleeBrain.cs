using System.Collections;
using Features.Health;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Features.AI.Enemy
{
    public class MeleeBrain : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        [Header("Sounds")]
        [SerializeField] private AudioSource attackSound;
        [SerializeField] private AudioSource idleSound;
        [SerializeField] private AudioSource walkSound;

        [Header("Health")]
        [SerializeField] private EnemyHealthController enemyHealthController;
        [SerializeField] private GameObject droppedHealthPotion;
        
        [Header("DetectPlayer")]
        [SerializeField] private float findPlayerRadius;
        [SerializeField] private LayerMask playerMask;

        [SerializeField] private PlayerDetectionCollider detectionCollider;
        public bool playerDetected;
        
        public Transform player;

        [Header("Attack")]
        [SerializeField] private float attackPlayerRadius;
        [SerializeField] private GameObject weapon;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float attackDuration;
        public bool playerOnAttackDistance;

        public bool alreadyAttacked;
        
        [Header("Patrol")]
        [SerializeField] private Transform leftDownPoint;
        [SerializeField] private Transform rightUpPoint;
        [SerializeField] private float distanceToPoint;

        [SerializeField] private EnemyDestroyer destroyer;
        
        public bool isWalkPointSet;
        public Vector3 walkPoint;

        public NavMeshAgent _navMeshAgent;
        private Rigidbody _rb;
        private int _walkHash;
        private int _attackHash;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            
            _walkHash= Animator.StringToHash("Walk");
            _attackHash= Animator.StringToHash("Melee_attack");
            
            _navMeshAgent = GetComponent<NavMeshAgent>();
            detectionCollider.Init(TriggerEnter, TriggerExit);
            enemyHealthController.Init(OnDeath);
        }

        private void TriggerEnter(GameObject gm)
        {
            if (player != null)
            {
                float distanceGM = (transform.position - gm.transform.position).magnitude;
                float distancePlayer = (transform.position - player.transform.position).magnitude;
                if (distancePlayer > distanceGM)
                    player = gm.transform;
            }
            else
                player = gm.transform;
        }
        
        private void TriggerExit(GameObject gm)
        {
            if(player != null & gm.name == player.name)
                player = null;
        }
        
        private void Update()
        {
            playerDetected = Physics.CheckSphere(transform.position, findPlayerRadius, playerMask);
            playerOnAttackDistance = Physics.CheckSphere(transform.position, attackPlayerRadius, playerMask);
            
            if(!playerDetected && !playerOnAttackDistance)
                Patrol();
            if(playerDetected && !playerOnAttackDistance)
                Chase();
            if(playerDetected && playerOnAttackDistance)
                StartCoroutine(Attack());
        }

        private void Patrol()
        {
            if(!isWalkPointSet)
                StartCoroutine(GetWalkPoint());

            if (isWalkPointSet)
            {
                animator.SetBool(_walkHash, true);
                
                if (!walkSound.isPlaying)
                    walkSound.Play();
                
                transform.LookAt(walkPoint);
                _navMeshAgent.SetDestination(walkPoint);
            }

            Vector3 distanceToWalk = transform.position - walkPoint;
            distanceToWalk.y = 0;
            if (distanceToWalk.magnitude < distanceToPoint)
                isWalkPointSet = false;
        }


        private void Chase()
        {
            if(!animator.GetBool(_walkHash))
                animator.SetBool(_walkHash, true);
            if(!walkSound.isPlaying)
                walkSound.Play();
            transform.LookAt(player);
            _navMeshAgent.SetDestination(player.position);
        }

        private IEnumerator Attack()
        {
            walkSound.Stop();
            animator.SetBool(_walkHash, false);
            transform.LookAt(player);
            _navMeshAgent.SetDestination(transform.position);
            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                attackSound.Play();
                animator.SetTrigger(_attackHash);
                weapon.SetActive(true);
                yield return new WaitForSeconds(attackDuration);
                weapon.SetActive(false);
                yield return new WaitForSeconds(attackCooldown);
                ResetAttack();
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        private IEnumerator GetWalkPoint()
        {
            while (true)
            {
                float randomX = Random.Range(leftDownPoint.position.x, rightUpPoint.position.x);
                float randomZ = Random.Range(leftDownPoint.position.z, rightUpPoint.position.z);

                walkPoint = new Vector3(randomX, 0, randomZ);
                if (walkPoint.x >= leftDownPoint.position.x 
                    && walkPoint.x <= rightUpPoint.position.x
                    && walkPoint.z >= leftDownPoint.position.z
                    && walkPoint.z <= rightUpPoint.position.z)
                    break;

                yield return null;
            }

            isWalkPointSet = true;
        }

        private void OnDeath()
        {
            Destroy(weapon);
            animator.SetBool(_walkHash, false);
            detectionCollider.enabled = false;
            TrySpawnHeal();
            destroyer.Activate();
            Destroy(gameObject);
        }

        private bool TrySpawnHeal()
        {
            int value = Random.Range(1, 10);
            if (value >= 3 || value <= 10)
            {
                Instantiate(droppedHealthPotion, transform.position, Quaternion.identity);
                return true;
            }

            return false;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, findPlayerRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackPlayerRadius);

        }
    }
}