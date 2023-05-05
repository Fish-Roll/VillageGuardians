using System;
using Features.Input;
using UnityEngine;

namespace Features.Attack
{
    public class GirlAttackController : MonoBehaviour
    {
        [SerializeField] private LightGirlAttack lightAttack;
        [SerializeField] private HeavyGirlAttack heavyAttack;
        [SerializeField] private Animator _animator;
        private Movement.Movement _movement;
        private bool _canAttack = true;
        private int lightAttackTrigger;
        private int heavyAttackTrigger;
        private void Awake()
        {
            _movement = GetComponent<Movement.Movement>();
            lightAttackTrigger = Animator.StringToHash("Standart_Attack");
            heavyAttackTrigger = Animator.StringToHash("StrangthAttack_start");
            lightAttack.Init(ref _canAttack);
            heavyAttack.Init(ref _canAttack, _animator);
        }

        public void LightAttack(Vector3 direction, bool isAiming)
        {
            if (_canAttack && isAiming)
            {
                _animator.SetTrigger(lightAttackTrigger);
                StartCoroutine(lightAttack.Attack(direction));
            }
        }

        public void HeavyAttack(bool isAiming)
        {
            if (_canAttack)
            {
                _movement.enabled = false;
                heavyAttack.Attack();
                _animator.SetTrigger(heavyAttackTrigger);
            }
        }
    }
}