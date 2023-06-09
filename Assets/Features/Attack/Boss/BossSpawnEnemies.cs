using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Attack.Boss
{
    public class BossSpawnEnemies : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        [SerializeField] private GameObject meleeEnemy;
        [SerializeField] private Transform[] meleeSpawnPoints;
        
        [SerializeField] private GameObject rangeEnemy;
        [SerializeField] private Transform[] rangeSpawnPoints;
        
        private List<GameObject> _allEnemies;
        private void Start()
        {
            _allEnemies = new List<GameObject>();
        }

        private List<GameObject> Use()
        {
            for (int i = 0; i < meleeSpawnPoints.Length; i++)
            {
                _allEnemies.Add(Instantiate(meleeEnemy, meleeSpawnPoints[i].position, Quaternion.identity));
            }
            for (int i = 0; i < rangeSpawnPoints.Length; i++)
            {
                _allEnemies.Add(Instantiate(rangeEnemy, rangeSpawnPoints[i].position, Quaternion.identity));
            }

            return _allEnemies;
        }
    }
}