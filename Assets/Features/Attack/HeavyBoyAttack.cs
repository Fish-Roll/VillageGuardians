using System.Collections;
using UnityEngine;

namespace Features.Attack
{
    public class HeavyBoyAttack : MonoBehaviour, BaseAttack
    {
        [SerializeField] private GameObject _attackCollider;
        [SerializeField] private float duration;
        [SerializeField] private float cooldownDuration;
        [SerializeField] private Transform newDashParent;

        public GameObject AttackCollider => _attackCollider;
        
        private bool _canAttack;
        private Animator _animator;
        
        public void Init(ref bool canAttack, Animator animator)
        {
            _canAttack = canAttack;
            _animator = animator;
        }

        public void Attack()
        {
            Transform oldDashParent = gameObject.transform;
            newDashParent.SetParent(null);
            gameObject.transform.SetParent(newDashParent);

            _attackCollider.SetActive(true);
            _canAttack = false;
            
            StartCoroutine(ResetAttack(oldDashParent, newDashParent));
        }

        private IEnumerator ResetAttack(Transform oldDashParent, Transform newDashParent)
        {
            yield return new WaitForSeconds(duration);
            _attackCollider.SetActive(false);
            oldDashParent.SetParent(null);
            newDashParent.SetParent(oldDashParent);
            yield return new WaitForSeconds(cooldownDuration);
            _canAttack = true;
        }
    }
}