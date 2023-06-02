using System.Collections;
using Features.Attack.Abstract;
using UnityEngine;

namespace Features.Attack.Boy
{
    public class BoyLightMeleeAttack : BaseMeleeAttack
    {
        [SerializeField] private float duration;
        [SerializeField] private ParticleSystem attackEffect;
        
        private Animator _animator;
        private int _attackHash;
        
        public override void Init(Animator animator)
        {
            _animator = animator;
            _attackHash = Animator.StringToHash("LightAttack");
        }

        public override IEnumerator Attack()
        {
            var wait = new WaitForSeconds(duration);
            BaseAttackController.canAttack = false;
            _animator.SetTrigger(_attackHash);
            attackEffect.Play();
            weapon.SetActive(true);
            yield return wait;
            ResetAttack();
        }
        
        protected override void ResetAttack()
        {
            attackEffect.Stop();
            weapon.SetActive(false);
            BaseAttackController.canAttack = true;
        }
    }
}