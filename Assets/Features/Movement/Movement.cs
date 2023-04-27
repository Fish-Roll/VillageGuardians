using System;
using UnityEngine;

namespace Features.Movement
{
    [Serializable]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float dashSpeed;
        
        [SerializeField] private float dashDuration;
        /// <summary>
        /// Придумать куда деть кд деша и уворота, в MoveController или оставить тут
        /// </summary>
        [SerializeField] private float dashCooldown;

        private Rigidbody _rb;
        private Animator _animator;

        private int _walkHash;
        public void Init(Rigidbody rb, Animator animator)
        {
            _rb = rb;
            _animator = animator;
            GetHashAnim();
        }

        private void GetHashAnim()
        {
            _walkHash = Animator.StringToHash("RunAnimation");
        }
        
        public void Move(Vector3 direction)
        {
            float verticalMovement = _rb.velocity.y;
            Vector3 moveDirection = direction.normalized * moveSpeed;
            _animator.SetBool(_walkHash, true);
            _rb.velocity = new Vector3(moveDirection.x, verticalMovement, moveDirection.z);
        }
        
        public void Dash(Vector3 direction)
        {
            float verticalMovement = _rb.velocity.y;
            Vector3 moveDirection = direction.normalized * dashSpeed;
            _rb.velocity = new Vector3(moveDirection.x, verticalMovement, moveDirection.z);
        }
        
    }
}