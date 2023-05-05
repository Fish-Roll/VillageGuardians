using UnityEngine;

namespace Features.Attack
{
    public class BoyAttackController : MonoBehaviour
    {
        [SerializeField] private LightBoyAttack lightAttack;
        [SerializeField] private HeavyBoyAttack heavyAttack;
        [SerializeField] private Animator animator;
        private Movement.Movement _movement;

        private bool _canAttack = true;
        private int lightAttackTrigger;
        private int heavyAttackTrigger;

        private void Awake()
        {
            _movement = GetComponent<Movement.Movement>();

            lightAttackTrigger = Animator.StringToHash("Standart_Attack");
            heavyAttackTrigger = Animator.StringToHash("StrongAttack");

            lightAttack.Init(ref _canAttack, animator);
            heavyAttack.Init(ref _canAttack, animator);
        }

        public void LightAttack(bool isAiming)
        {
            if (_canAttack && isAiming)
            {
                animator.SetTrigger(lightAttackTrigger);
                StartCoroutine(lightAttack.Attack());
            }
        }

        public void HeavyAttack()
        {
            if (_canAttack)
            {
                _movement.enabled = false;
                heavyAttack.Attack();
                animator.SetTrigger(heavyAttackTrigger);
            }
        }
    }
}