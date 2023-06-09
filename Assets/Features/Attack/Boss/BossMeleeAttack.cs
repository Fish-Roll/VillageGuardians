using System;
using System.Collections;
using Features.Attack.Abstract;
using UnityEngine;

namespace Features.Attack.Boss
{
    public class BossMeleeAttack : BaseMeleeAttack
    {
        [SerializeField] private float resetDelay;
        
        private Animator _animator;
        private int _attackHash;

        private void Start()
        {
            _attackHash = Animator.StringToHash("");
        }

        public override IEnumerator Attack()
        {
            weapon.SetActive(true);
            _animator.SetTrigger(_attackHash);
            
            yield return new WaitForSeconds(resetDelay);
            ResetAttack();
        }

        protected override void ResetAttack()
        {
            weapon.SetActive(false);
        }

        public override void Init(Animator animator)
        {
            _animator = animator;
        }
    }
}