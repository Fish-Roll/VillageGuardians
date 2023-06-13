using System;
using System.Collections;
using System.Collections.Generic;
using Features.Health;
using UnityEngine;
using UnityEngine.AI;

namespace Features.AI.Boss
{
    public class BossBrain : MonoBehaviour
    {
        [Header("AttackDetection")]
        [SerializeField] private float attackRange;
        [SerializeField] private LayerMask playerMask;

        [Header("LightAttack")] 
        [SerializeField] private float lightAttackDuration;
        [SerializeField] private float lightAttackDelay;
        [SerializeField] private GameObject weapon;
        
        [Header("HeavyAttack")]
        [SerializeField] private float heavyAttackTimer;
        [SerializeField] private float heavyAttackWaitAfterSpawn;
        [SerializeField] private float heavyAttackDelay;
        [SerializeField] private GameObject heavyAttack;
        [SerializeField] private AudioSource _audio;
        
        [Header("Movement")]
        [SerializeField] private float speed;

        private int _targetPlayer;

        [Header("SpawnEnemy")] 
        [SerializeField] private int countSpawns;
        [SerializeField] private float spawnDelay;
        [SerializeField] private GameObject meleeEnemy;
        [SerializeField] private GameObject rangeEnemy;
        [SerializeField] private List<Transform> meleeSpawnPoint;
        [SerializeField] private List<Transform> rangeSpawnPoint;
        
        /// <summary>
        /// Need to check when leave from protect
        /// </summary>
        private List<GameObject> enemyList;
        
        [Header("Other")]
        private NavMeshAgent _navMeshAgent;
        [SerializeField] private GameObject[] players;
        [SerializeField] private float deathDuration;
        
        private bool _isDead;
        private bool _canMove = true;
        private bool _canHeavyAttack = true;
        private bool _canAttack = true;
        private bool _isProtect;

        private BossHealthController _healthController;
        private Animator _animator;
        private bool _isMove;
        
        private int _moveHash;
        private int _lightAttackHash;
        private int _heavyAttackHash;
        
        private int _protectHash;
        private int _deathHash;
        private int _spawnEnemyHash;
        
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            
            _navMeshAgent.speed = speed;
            players = GameObject.FindGameObjectsWithTag("Player");
            
            _healthController = GetComponent<BossHealthController>();
            _healthController.Init(OnDeath, OnProtect, countSpawns);
            
            _moveHash = Animator.StringToHash("Walk");
            _lightAttackHash = Animator.StringToHash("LightAttack");
            _heavyAttackHash = Animator.StringToHash("HeavyAttack");
            _protectHash = Animator.StringToHash("IsProtect");
            _deathHash = Animator.StringToHash("Die");
            _spawnEnemyHash = Animator.StringToHash("SpawnEnemy");
            
            StartCoroutine(HeavyAttack());
        }
        
        public void Update()
        {
            _canAttack = Physics.CheckSphere(transform.position, attackRange, playerMask);

            // if (!_canAttack && !_isProtect)
            // {
            //     _targetPlayer = FindPlayer();
            //     MoveToPlayer(_targetPlayer);
            // }
            // else if (_canAttack && !_isProtect)
            // {
            //     StartCoroutine(LightAttack());
            // }
        }

        private int FindPlayer()
        {
            float minDistance = -1;
            int playerIndex = -1;
            for (int i = 0; i < players.Length; i++)
            {
                var distance = Vector3.Distance(transform.position, players[i].transform.position);
                if (distance < minDistance){
                    minDistance = distance;
                    playerIndex = i;
                }
            }

            return playerIndex;
        }
        
        private void MoveToPlayer(int targetPlayer)
        {
            // if (!_isMove)
            // {
                _isMove = true;
                _animator.SetBool(_moveHash, true);
                //_navMeshAgent.destination = players[targetPlayer].transform.position;
                _navMeshAgent.SetDestination(players[targetPlayer].transform.position);
            //}
        }

        private IEnumerator LightAttack()
        {
            //_isMove = false;
            _canAttack = false;
            _canHeavyAttack = false;
            _canMove = false;
            _animator.SetBool(_moveHash, false);
            _animator.SetTrigger(_lightAttackHash);
            weapon.SetActive(true);
            yield return new WaitForSeconds(lightAttackDuration);
            weapon.SetActive(false);
            yield return new WaitForSeconds(lightAttackDelay);
            _canAttack = true;
            _canHeavyAttack = true;
            _canMove = true;
        }
        
        private IEnumerator HeavyAttack()
        {
            while (!_isDead)
            {
                yield return new WaitForSeconds(heavyAttackTimer);
                // if(_isProtect) continue;
                // if (_canHeavyAttack)
                // {
                    _animator.SetBool(_moveHash, false);
                    _navMeshAgent.speed = 0;
                    //_isMove = false;
                    _canMove = false;
                    _canAttack = false;
                    _canHeavyAttack = false;
                    _animator.SetTrigger(_heavyAttackHash);
                    yield return new WaitForSeconds(heavyAttackDelay);
                    //_audio.Play();
                    heavyAttack.SetActive(true);
                    yield return new WaitForSeconds(heavyAttackWaitAfterSpawn);
                    heavyAttack.SetActive(false);
                    _canMove = true;
                    _canHeavyAttack = true;
                    _navMeshAgent.speed = speed;
                //}
            }
        }
        
        
        private IEnumerator OnProtect()
        {
            _animator.SetBool(_moveHash, false);
            //_isMove = false;
            _canHeavyAttack = false;
            _animator.SetTrigger(_spawnEnemyHash);
            yield return new WaitForSeconds(spawnDelay);
            
            for (int i = 0; i < meleeSpawnPoint.Count; i++)
            {
                enemyList.Add(
                    Instantiate(meleeEnemy, meleeSpawnPoint[i].position, Quaternion.identity)
                    );
                //TODO: add init check enemy count script 
            }

            for (int i = 0; i < rangeSpawnPoint.Count; i++)
            {
                enemyList.Add(
                    Instantiate(rangeEnemy, rangeSpawnPoint[i].position, Quaternion.identity)
                );
                //TODO: add init check enemy count script
            }
            
            _animator.SetBool(_protectHash, true);
            _healthController.enabled = false;
        }

        private void OnDeath()
        {
            _animator.SetBool(_moveHash, false);
            _animator.SetBool(_protectHash, false);
            _isDead = true;
            StartCoroutine(Death());
        }

        private IEnumerator Death()
        {
            _animator.SetTrigger(_deathHash);
            yield return new WaitForSeconds(deathDuration);
            //activate disolve model
            Destroy(gameObject);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}