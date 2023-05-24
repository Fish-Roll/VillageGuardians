using System.Collections;
using Features.Attack.Abstract;
using UnityEngine;

namespace Features.Attack.Boy
{
    public class BoyHeavyMeleeAttack : BaseMeleeAttack
    {
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        
        //TODO: Добавить вычитание выносливости
        
        private Animator _animator;
        private int _attackHash;
        
        public override void Init(Animator animator)
        {
            _animator = animator;
            _attackHash = Animator.StringToHash("StrongAttack");
        }

        public override IEnumerator Attack()
        {
            var waitDuration = new WaitForSeconds(duration);
            var waitDelay = new WaitForSeconds(delay);
            BaseAttackController.canAttack = false;
            
            _animator.SetTrigger(_attackHash);
            yield return waitDelay;
            
            weapon.SetActive(true);
            yield return waitDuration;
            
            ResetAttack();
        }

        protected override void ResetAttack()
        {
            weapon.SetActive(false);
            BaseAttackController.canAttack = true;
        }
    }
}