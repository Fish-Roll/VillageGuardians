﻿using System;
using System.Collections;
using System.Collections.Generic;
using Features.Attack;
using Features.Attack.Abstract;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyRangeBrain : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Transform> patrolPoints;
    
    [SerializeField] private float attackTime;
    [SerializeField] private float attackRange;
    [SerializeField] private float sightRange;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform player;
    [SerializeField] private EnemyRangeAttack attack;
    
    [SerializeField] private Animator animator;
    
    private NavMeshAgent _agent;
    private bool _playerInAttackRange;
    private bool _playerInSightRange;
    private bool _walkPointSet;
    private bool alreadyAttacked;
    private int _moveHash;
    private int _deathHash;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = speed;
        _moveHash = Animator.StringToHash("Walk");
        
        attack.Init(animator);
        
        _deathHash = Animator.StringToHash("Death");
    }

    private void FixedUpdate()
    {
        _playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        _playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!_playerInSightRange && !_playerInAttackRange) Patrolling();
        if(_playerInSightRange && !_playerInAttackRange) ChasePlayer();
        if(_playerInSightRange && _playerInAttackRange) StartCoroutine(AttackPlayer()); 
    }
    
    private int _pointIndex;
    private void Patrolling()
    {
        animator.SetBool(_moveHash, true);
        if (!_walkPointSet) _pointIndex = GetRandomPoint();

        if (_walkPointSet)
        {
            _agent.SetDestination(patrolPoints[_pointIndex].position);
            transform.LookAt(patrolPoints[_pointIndex]);
        }

        if ((transform.position - patrolPoints[_pointIndex].position).magnitude < 1f)
        {
            _walkPointSet = false;
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool(_moveHash, true);
        _agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
        Debug.Log("Chase");
    }

    private IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(1);
        _agent.SetDestination(transform.position);
        animator.SetBool(_moveHash, false);
        if (!alreadyAttacked)
        {
            Invoke(nameof(attack.RangeAttack), attackTime);
        }

        Debug.Log("Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            player = other.transform;
    }

    private static int value = 0;
    private int GetRandomPoint()
    {
        int randomPoint = Random.Range(0, patrolPoints.Count - 1);
        _walkPointSet = true;
        return randomPoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
