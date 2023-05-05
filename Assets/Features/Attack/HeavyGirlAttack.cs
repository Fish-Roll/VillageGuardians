using System.Collections;
using UnityEngine;

namespace Features.Attack
{
    public class HeavyGirlAttack : MonoBehaviour//, BaseAttack
    {
        [SerializeField] private GameObject _attackCollider;
        [SerializeField] private float delay;
        [SerializeField] private float duration;
        [SerializeField] private float cooldownDuration;
        
        public GameObject AttackCollider => _attackCollider;
        private bool _canAttack;
        private int _isRunHeavyAttack;
        private int _isHeavyAttack;
        private Animator _animator;
        public void Init(ref bool canAttack, Animator animator)
        {
            _canAttack = canAttack;
            _animator = animator;
            _isRunHeavyAttack = Animator.StringToHash("StrangthAttack_start");
            _isHeavyAttack = Animator.StringToHash("StrangthAttack_idle");
        }

        public IEnumerator Attack()
        {
            _animator.SetTrigger(_isRunHeavyAttack);
            _animator.SetBool(_isHeavyAttack, true);

            yield return new WaitForSeconds(delay);
            
            _attackCollider.SetActive(true);
            _canAttack = false;

            StartCoroutine(ResetAttack(/*alreadyAttack*/));
        }

        private IEnumerator ResetAttack()
        {
            yield return new WaitForSeconds(duration);
            _attackCollider.SetActive(false);
            _animator.SetBool(_isHeavyAttack, false);
            //alreadyAttack = false;
            yield return new WaitForSeconds(cooldownDuration);
            _canAttack = true;
        }
    }
}