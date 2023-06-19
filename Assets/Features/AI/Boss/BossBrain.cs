using System;
using System.Collections;
using System.Collections.Generic;
using Features.AI.Enemy;
using Features.Health;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Features.AI.Boss
{
    public class BossBrain : MonoBehaviour
    {
        [Header("AttackDetection")]
        [SerializeField] private float attackRange;
        [SerializeField] private LayerMask playerMask;

        [Header("LightAttack")] 
        [SerializeField] private float lightAttackDuration;
        [SerializeField] private float lightAttackCooldown;
        [SerializeField] private GameObject weapon;
        [SerializeField] private AudioSource lightAttackSound;

        [Header("HeavyAttack")]
        [SerializeField] private float heavyAttackTimer;
        [SerializeField] private float heavyAttackWaitAfterSpawn;
        [SerializeField] private float heavyAttackDelay;
        [SerializeField] private GameObject heavyAttack;
        [SerializeField] private AudioSource heavyAttackSound;
        
        [Header("Movement")]
        [SerializeField] private float speed;
        [SerializeField] private EnemyDestroyer enemyDestroyer;
        
        private int _targetPlayer;

        [Header("SpawnEnemy")] 
        [SerializeField] private float spawnDelay;
        [SerializeField] private GameObject meleeEnemy;
        [SerializeField] private GameObject rangeEnemy;
        [SerializeField] private List<Transform> meleeSpawnPoint;
        [SerializeField] private List<Transform> rangeSpawnPoint;
        //[SerializeField] private AudioSource spawnEnemySound;
        
        /// <summary>
        /// Need to check when leave from protect
        /// </summary>
        private List<GameObject> enemyList;
        private NavMeshAgent _navMeshAgent;
        
        [Header("Other")]
        [SerializeField] private GameObject[] players;
        [SerializeField] private float endProtectTime;

        public bool _canAttack;
        public bool alreadyAttacked;
        public bool heavyAttacked;
        public bool isProtected;
        
        private BossHealthController _healthController;
        private Animator _animator;
        
        private int _moveHash;
        private int _lightAttackHash;
        private int _heavyAttackHash;
        
        private int _protectHash;
        private int _deathHash;
        private int _spawnEnemyHash;
        
        private void Start()
        {
            enemyList = new List<GameObject>();
            
            _animator = GetComponentInChildren<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            
            _navMeshAgent.speed = speed;
            players = GameObject.FindGameObjectsWithTag("Player");
            
            _healthController = GetComponent<BossHealthController>();
            _healthController.Init(OnDeath, OnProtect);
            
            _moveHash = Animator.StringToHash("Walk");
            _lightAttackHash = Animator.StringToHash("LightAttack");
            _heavyAttackHash = Animator.StringToHash("HeavyAttack");
            _protectHash = Animator.StringToHash("IsProtect");
            _deathHash = Animator.StringToHash("Die");
            _spawnEnemyHash = Animator.StringToHash("SpawnEnemy");
            OnProtect();
            StartCoroutine(HeavyAttack());
        }

        public void Update()
        {
            CheckEnemies();
            _canAttack = Physics.CheckSphere(transform.position, attackRange, playerMask);
            
            if (_canAttack && !heavyAttacked && !isProtected)
            {
                StartCoroutine(LightAttack());
            }
            else if (!_canAttack && !heavyAttacked && !isProtected)
            {
                _targetPlayer = FindPlayer();
                MoveToPlayer(_targetPlayer);
            }
            
            // if (!_canAttack && !_isProtect)
            // {
            // }
            // else if (_canAttack && !_isProtect)
            // {
            //     StartCoroutine(LightAttack());
            // }
        }

        public int countDeadEnemy;
        private void CheckEnemies()
        {
            if (isProtected)
            {
                countDeadEnemy = 0;
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i] == null)
                        countDeadEnemy++;
                }
                if (countDeadEnemy == (meleeSpawnPoint.Count + rangeSpawnPoint.Count))
                {
                    StartCoroutine(StopProtect());
                }
            }
        }
        
        private int FindPlayer()
        {
            float minDistance = Single.MaxValue;
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
            if(!_animator.GetBool(_moveHash))
                _animator.SetBool(_moveHash, true);

            transform.LookAt(players[targetPlayer].transform);
            _navMeshAgent.SetDestination(players[targetPlayer].transform.position);
        }

        private IEnumerator LightAttack()
        {

            _animator.SetBool(_moveHash, false);
            transform.LookAt(players[_targetPlayer].transform);
            _navMeshAgent.SetDestination(transform.position);
            
            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                
                lightAttackSound.Stop();
                if(!lightAttackSound.isPlaying)
                    lightAttackSound.Play();
                _animator.SetTrigger(_lightAttackHash);
                weapon.SetActive(true);
                yield return new WaitForSeconds(lightAttackDuration);
                weapon.SetActive(false);
                yield return new WaitForSeconds(lightAttackCooldown);
                alreadyAttacked = false;
            }
        }
        
        private IEnumerator HeavyAttack()
        {
            while (true)
            {
                if (!isProtected)
                {
                    lightAttackSound.Stop();
                    
                    yield return new WaitForSeconds(heavyAttackTimer);

                    heavyAttacked = true;
                    _animator.SetBool(_moveHash, false);
                    _navMeshAgent.SetDestination(transform.position);
                    _animator.SetTrigger(_heavyAttackHash);
                    if(!heavyAttackSound.isPlaying)
                        heavyAttackSound.Play();

                    yield return new WaitForSeconds(heavyAttackDelay);
                    //_audio.Play();
                    heavyAttack.SetActive(true);
                    yield return new WaitForSeconds(heavyAttackWaitAfterSpawn);
                    heavyAttack.SetActive(false);
                    heavyAttacked = false;
                }
                else
                {
                    yield return null;
                }
            }
        }
        
        
        private void OnProtect()
        {
            StartCoroutine(Protect());
        }

        private IEnumerator Protect()
        {
            isProtected = true;
            _animator.SetBool(_moveHash, false);
            _navMeshAgent.speed = 0;
            //_navMeshAgent.SetDestination(transform.position);
            
            _animator.SetTrigger(_spawnEnemyHash);
            // if(!spawnEnemySound.isPlaying)
            //     spawnEnemySound.Play();

            yield return new WaitForSeconds(spawnDelay);
            
            for (int i = 0; i < meleeSpawnPoint.Count; i++)
            {
                try
                {
                    enemyList.Add(
                        Instantiate(meleeEnemy, meleeSpawnPoint[i].position, Quaternion.identity)
                    );
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
                //TODO: add init check enemy count script 
            }

            for (int i = 0; i < rangeSpawnPoint.Count; i++)
            {
                try
                {
                    enemyList.Add(
                        Instantiate(rangeEnemy, rangeSpawnPoint[i].position, Quaternion.identity)
                    );
                }catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

                //TODO: add init check enemy count script
            }
            
            if(!_animator.GetBool(_protectHash))
            {
                _animator.SetBool(_protectHash, true);
                
            }
            _healthController.OnProtect(isProtected);
        }

        private IEnumerator StopProtect()
        {
            _animator.SetBool(_protectHash, false);
            yield return new WaitForSeconds(endProtectTime);
            isProtected = false;
            _healthController.OnProtect(isProtected);
            _navMeshAgent.speed = speed;
        }
        
        private void OnDeath()
        {
            _animator.SetBool(_moveHash, false);
            _animator.SetBool(_protectHash, false);
            enemyDestroyer.Activate();
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}