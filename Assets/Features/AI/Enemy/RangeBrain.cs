using Features.Attack.Weapon;
using Features.Health;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Features.AI.Enemy
{
    public class RangeBrain : MonoBehaviour
    {
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
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float attackPlayerRadius;
        
        public bool playerOnAttackDistance;

        public bool alreadyAttacked;
        
        [Header("Patrol")]
        [SerializeField] private Vector3 patrolRange;
        [SerializeField] private float distanceToPoint;

        [SerializeField] private EnemyDestroyer destroyer;
        
        public bool isWalkPointSet;
        public Vector3 walkPoint;

        public NavMeshAgent _navMeshAgent;

        private void Start()
        {
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
                Attack();
        }

        private void Patrol()
        {
            if(!isWalkPointSet)
                GetWalkPoint();

            if (isWalkPointSet)
            {
                transform.LookAt(walkPoint);
                _navMeshAgent.SetDestination(walkPoint);
            }

            Vector3 distanceToWalk = transform.position - walkPoint;
            
            if (distanceToWalk.magnitude < distanceToPoint)
                isWalkPointSet = false;
        }

        private void Chase()
        {
            transform.LookAt(player);
            _navMeshAgent.SetDestination(player.position);
        }

        private void Attack()
        {
            transform.LookAt(player);
            _navMeshAgent.SetDestination(transform.position);
            if (!alreadyAttacked)
            {
                var gm = Instantiate(projectile, spawnPosition.position, Quaternion.identity);
                gm.GetComponent<EnemyFireball>().Init(player.position);
                
                Invoke(nameof(ResetAttack), attackCooldown);
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        private void GetWalkPoint()
        {
            float randomX = Random.Range(-patrolRange.x, patrolRange.x);
            float randomZ = Random.Range(-patrolRange.z, patrolRange.z);

            walkPoint = new Vector3(transform.position.x + randomX, 0, transform.position.z + randomZ);
            isWalkPointSet = true;
        }

        private void OnDeath()
        {
            Destroy(projectile);
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