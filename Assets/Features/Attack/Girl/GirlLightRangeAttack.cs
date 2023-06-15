using System.Collections;
using Features.Attack.Abstract;
using Features.Attack.Weapon;
using Features.Input;
using UnityEngine;

namespace Features.Attack.Girl
{
    public class GirlLightRangeAttack : BaseRangeAttack
    {
        [SerializeField] private float delay;
        [SerializeField] private float delayReset;
        [SerializeField] private Transform targetRotation;
        
        private Animator _animator;
        private int _attackHash;
        private InputSignatory _inputSignatory;

        private void Start()
        {
            _inputSignatory = GetComponent<InputSignatory>();
        }
        
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
            var GO = Instantiate(projectile, spawnPosition.position, targetRotation.rotation);
            var moveDirection = _inputSignatory.GetMouseHitVector();
            GO.GetComponent<Fireball>().Init(moveDirection);
            yield return new WaitForSeconds(delayReset);
            
            ResetAttack();
        }

        protected override void ResetAttack()
        {
            BaseAttackController.canAttack = true;
        }
    }
}