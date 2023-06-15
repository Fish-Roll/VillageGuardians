using System;
using System.Collections;
using Features.Attack.Abstract;
using Features.Input;
using Features.Movement;
using Features.Stamina;
using UnityEngine;

namespace Features.Attack.Boy
{
    public class BoyHeavyMeleeAttack : BaseMeleeAttack
    {
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        [SerializeField] private GameObject _newParent;
        [SerializeField] private GameObject particleSystem;
        [SerializeField] private float staminaSub;
        [SerializeField] private StaminaController staminaController;
        
        [SerializeField] private InputSignatory _inputSignatory;
        private GameObject _oldParent;
        private Animator _animator;
        private int _attackHash;
        
        public override void Init(Animator animator)
        {
            _oldParent = gameObject;
            _animator = animator;
            _attackHash = Animator.StringToHash("StrongAttack");
        }

        private GameObject _gm;
        public override IEnumerator Attack()
        {
            _newParent.transform.position = new Vector3(_newParent.transform.position.x, 0, _newParent.transform.position.z);
            var waitDuration = new WaitForSeconds(duration);
            var waitDelay = new WaitForSeconds(delay);
            BaseAttackController.canAttack = false;

            staminaController.Subtract(staminaSub);
            
            _inputSignatory.IsMoving = false;
            _inputSignatory.IsDashing = false;

            _newParent.transform.SetParent(null);
            gameObject.transform.SetParent(_newParent.transform);
            gameObject.transform.localPosition = Vector3.zero;

            _animator.SetTrigger(_attackHash);

            yield return waitDelay;

            //particleSystem.SetActive(true);
            weapon.SetActive(true);
            yield return waitDuration;
            
            _oldParent.transform.SetParent(null);
            _newParent.transform.SetParent(_oldParent.transform);
            _newParent.transform.localPosition = new Vector3(0,0,0);

            //particleSystem.SetActive(false);
            
            ResetAttack();
        }

        protected override void ResetAttack()
        {
            weapon.SetActive(false);
            BaseAttackController.canAttack = true;
            _inputSignatory.isHeavyAttacked = false;
        }
    }
}