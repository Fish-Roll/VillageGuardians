using System.Collections;
using Features.Attack.Abstract;
using Features.Input;
using Features.Stamina;
using UnityEngine;

namespace Features.Attack.Girl
{
    public class GirlHeavyMeleeAttack : BaseMeleeAttack
    {
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        [SerializeField] private float staminaSub;
        [SerializeField] private StaminaController staminaController;
        [SerializeField] private InputSignatory inputSignatory;
        [SerializeField] private AudioSource fireFlowSound;
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
            
            staminaController.Subtract(staminaSub);
            inputSignatory.IsMoving = false;
            inputSignatory.IsDashing = false;
            _animator.SetTrigger(_attackHash);
            yield return waitDelay;
            fireFlowSound.Play();

            _animator.SetBool(_idleAttackHash, true);
            weapon.SetActive(true);
            
            yield return waitDuration;
            fireFlowSound.Stop();

            inputSignatory.isHeavyAttacked = false;
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