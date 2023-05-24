using System.Collections;
using Features.Attack.Abstract;
using UnityEngine;

namespace Features.Attack.Girl
{
    public class GirlHeavyMeleeAttack : BaseMeleeAttack
    {
        [SerializeField] private float duration;
        [SerializeField] private float delay;

        private Animator _animator;
        private int _attackHash;
        private int _idleAttackHash;
        
        public override void Init(Animator animator)
        {
            _animator = animator;
            _attackHash = Animator.StringToHash("StrangthAttack_start");
            _idleAttackHash = Animator.StringToHash("StrangthAttack_idle");
        }

        public override IEnumerator Attack()
        {
            var waitDuration = new WaitForSeconds(duration);
            var waitDelay = new WaitForSeconds(delay);
            BaseAttackController.canAttack = false;
            
            _animator.SetTrigger(_attackHash);
            yield return waitDelay;
            
            _animator.SetBool(_idleAttackHash, true);
            weapon.SetActive(true);
            
            yield return waitDuration;
            ResetAttack();
        }

        protected override void ResetAttack()
        {
            weapon.SetActive(false);
            _animator.SetBool(_idleAttackHash, false);
            BaseAttackController.canAttack = true;
        }

    }
}