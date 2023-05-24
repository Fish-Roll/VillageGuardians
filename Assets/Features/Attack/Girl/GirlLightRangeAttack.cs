using System.Collections;
using Features.Attack.Abstract;
using UnityEngine;

namespace Features.Attack.Girl
{
    public class GirlLightRangeAttack : BaseRangeAttack
    {
        [SerializeField] private float delay;
        [SerializeField] private float delayReset;
        
        private Animator _animator;
        private int _attackHash;
        
        public override void Init(Animator animator)
        {
            _animator = animator;
            _attackHash = Animator.StringToHash("Standart_Attack");
        }

        public override IEnumerator Attack()
        {
            var waitDelay = new WaitForSeconds(delay);
            BaseAttackController.canAttack = false;
            
            _animator.SetTrigger(_attackHash);
            yield return waitDelay;
            Instantiate(projectile, spawnPosition.position, Quaternion.identity);
            yield return new WaitForSeconds(delayReset);
            
            ResetAttack();
        }

        protected override void ResetAttack()
        {
            BaseAttackController.canAttack = true;
        }
    }
}