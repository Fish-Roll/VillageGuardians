using System.Collections;
using Features.Attack.Abstract;
using UnityEngine;

namespace Features.Attack.Boy
{
    public class BoyLightMeleeAttack : BaseMeleeAttack
    {
        [SerializeField] private float duration;
        
        private Animator _animator;
        private int _attackHash;
        
        public override void Init(Animator animator)
        {
            _animator = animator;
            _attackHash = Animator.StringToHash("StandardAttack");
        }

        public override IEnumerator Attack()
        {
            var wait = new WaitForSeconds(duration);
            BaseAttackController.canAttack = false;
            _animator.SetTrigger(_attackHash);
            weapon.enabled = true;
            yield return wait;
            
            ResetAttack();
        }
        
        protected override void ResetAttack()
        {
            weapon.enabled = false;
            BaseAttackController.canAttack = true;
        }
    }
}