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
        
        private int _lightAttackTrigger;
        private int _heavyAttackTrigger;
        private int _bustAttackTrigger;
        
        private void Awake()
        {
            _movement = GetComponent<Movement.Movement>();

            _lightAttackTrigger = Animator.StringToHash("StandardAttack");
            _heavyAttackTrigger = Animator.StringToHash("StrongAttack");
            _bustAttackTrigger = Animator.StringToHash("IsBoost");
            
            lightAttack.Init(ref _canAttack, animator);
            heavyAttack.Init(ref _canAttack, animator);
            
        }

        public void LightAttack(bool isAiming)
        {
            if (_canAttack && isAiming)
            {
                animator.SetTrigger(_lightAttackTrigger);
                StartCoroutine(lightAttack.Attack());
            }
        }

        public void HeavyAttack()
        {
            if (_canAttack)
            {
                _movement.enabled = false;
                
                heavyAttack.Attack();
                animator.SetTrigger(_heavyAttackTrigger);
            }
        }
    }
}