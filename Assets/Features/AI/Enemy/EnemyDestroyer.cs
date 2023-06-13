using System;
using System.Collections;
using UnityEngine;

namespace Features.AI.Enemy
{
    public class EnemyDestroyer : MonoBehaviour
    {
        [SerializeField] private GameObject modelParent;
        [SerializeField] private float waitAnim;
        
        private DisolveEnemy _disolve;
        private Animator _animator;
        private int _deathHash;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _disolve = GetComponent<DisolveEnemy>();
            _deathHash = Animator.StringToHash("Die");
        }

        public void Activate()
        {
            StartCoroutine(DestroyParent());
        }

        private IEnumerator DestroyParent()
        {
            transform.SetParent(null);
            modelParent.transform.SetParent(transform);
            Destroy(modelParent);
            
            _animator.SetTrigger(_deathHash);
            yield return new WaitForSeconds(waitAnim);
            StartCoroutine(_disolve.DisolveCo());
        }
    }
}