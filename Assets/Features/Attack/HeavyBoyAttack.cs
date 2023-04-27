using System.Collections;
using UnityEngine;

namespace Features.Attack
{
    public class HeavyBoyAttack : MonoBehaviour, BaseAttack
    {
        [SerializeField] private GameObject _attackCollider;
        [SerializeField] private float duration;
        [SerializeField] private float cooldownDuration;

        public GameObject AttackCollider => _attackCollider;
        
        private bool _canAttack;
        private int _heavyAttackHash;
        private Animator _animator;
        
        public void Init(ref bool canAttack, Animator animator)
        {
            _canAttack = canAttack;
            _animator = animator;
            _heavyAttackHash = Animator.StringToHash("");
        }

        public void Attack()
        {
            _animator.SetTrigger(_heavyAttackHash);
            _attackCollider.SetActive(true);
            _canAttack = false;
            
            StartCoroutine(ResetAttack());
        }

        private IEnumerator ResetAttack()
        {
            yield return new WaitForSeconds(duration);
            _attackCollider.SetActive(false);
            _animator.SetBool(_heavyAttackHash, false);
            
            yield return new WaitForSeconds(cooldownDuration);
            _canAttack = true;
        }
    }
}