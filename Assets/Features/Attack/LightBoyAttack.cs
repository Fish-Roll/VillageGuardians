using System.Collections;
using UnityEngine;

namespace Features.Attack
{
    public class LightBoyAttack : MonoBehaviour//, BaseAttack
    {
        [SerializeField] private GameObject _attackCollider;
        [SerializeField] private float duration;
        [SerializeField] private float lightAttackDelay;
        
        public GameObject AttackCollider => _attackCollider;
        
        private bool _canAttack;
        private Animator _animator;
        public void Init(ref bool canAttack, Animator animator)
        {
            _canAttack = canAttack;
            _animator = animator;
        }

        public IEnumerator Attack()
        {
            yield return new WaitForSeconds(lightAttackDelay);

            _canAttack = false;
            _attackCollider.SetActive(true);
            StartCoroutine(ResetAttack());
        }

        private IEnumerator ResetAttack()
        {
            yield return new WaitForSeconds(duration);
            _canAttack = true;
            _attackCollider.SetActive(false);
        }
    }
}