using System;
using System.Collections;
using Features.Input;
using UnityEngine;

namespace Features.Movement
{
    [Serializable]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float dashSpeed;
        [SerializeField] private Transform newDashParent;
        [SerializeField] private float dashDuration;
        /// <summary>
        /// Придумать куда деть кд деша и уворота, в MoveController или оставить тут
        /// </summary>
        [SerializeField] private float dashCooldown;

        private float _dodgeTimer;
        
        private Rigidbody _rb;
        private Animator _animator;

        private int _walkHash;
        private int _dodgeHash;
        private InputSignatory _inputSignatory;

        public void Init(Rigidbody rb, Animator animator, InputSignatory inputSignatory)
        {
            _rb = rb;
            _animator = animator;
            _inputSignatory = inputSignatory;
            GetHashAnim();
        }

        private void GetHashAnim()
        {
            _walkHash = Animator.StringToHash("RunAnimation");
            _dodgeHash = Animator.StringToHash("Dodge");
        }
        
        public void Move(Vector3 direction)
        {
            float verticalMovement = _rb.velocity.y;
            Vector3 moveDirection = direction.normalized * moveSpeed;
            _animator.SetBool(_walkHash, true);
            _rb.velocity = new Vector3(moveDirection.x, verticalMovement, moveDirection.z);
        }
        
        public IEnumerator Dash(Vector3 direction)
        {
            Transform oldDashParent = gameObject.transform;
            newDashParent.SetParent(null);
            gameObject.transform.SetParent(newDashParent);
            _animator.SetTrigger(_dodgeHash);
            
            yield return new WaitForSeconds(dashDuration);
            
            _inputSignatory.IsDashing = false;
            oldDashParent.SetParent(null);
            newDashParent.SetParent(oldDashParent);
        }
    }
}