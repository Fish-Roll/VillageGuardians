using System.Collections;
using Features.Attack.Abstract;
using UnityEngine;

namespace Features.Attack
{
    public class EnemyMeleeAttack : BaseMeleeAttack
    {
        private Animator _animator;
        [SerializeField] private float attackCooldown;
        private int _hashAttack;
        
        private void Start()
        {
            _hashAttack = Animator.StringToHash("Melee_attack");
        }

        public void MeleeAttack()
        {
        }
        
        public override IEnumerator Attack()
        {
            // _animator.SetTrigger(_hashAttack);
            // weapon.SetActive(true);
            yield return new WaitForSeconds(attackCooldown);
            // ResetAttack();
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