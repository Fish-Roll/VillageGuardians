using System.Collections;
using Features.Attack.Abstract;
using UnityEngine;

namespace Features.Attack
{
    public class EnemyRangeAttack : BaseRangeAttack
    {
        [SerializeField] private float delay;
        [SerializeField] private float delayReset;
        
        private Animator _animator;
        private int _hashAttack;
        
        private void Start()
        {
            _hashAttack = Animator.StringToHash("Ranged_attack");
        }

        public void RangeAttack()
        {
            
        } 
         //Rotated enemy spawn projectile, that will shout to players направлении
        public override IEnumerator Attack()
        {
            yield return new WaitForSeconds(4);
        }
        
        protected override void ResetAttack()
        {
            
        }
        
        public override void Init(Animator animator)
        {
            _animator = animator;
        }
    }
}