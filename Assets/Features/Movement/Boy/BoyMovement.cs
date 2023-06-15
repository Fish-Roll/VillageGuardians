using System;
using System.Collections;
using Cinemachine;
using Features.Attack.Boy;
using Features.Input;
using Features.Movement.Abstract;
using UnityEngine;

namespace Features.Movement.Boy
{
    public class BoyMovement : BasePlayerMovement
    {
        [SerializeField] private Transform newDashParent;
        private BoyHeavyMeleeAttack _heavyMeleeAttack;

        private int _dodgeHash;

        private void Start()
        {
            _heavyMeleeAttack = GetComponent<BoyHeavyMeleeAttack>();
        }

        public override void Init(Rigidbody rb, Animator animator, InputSignatory inputSignatory)
        {
            this.rb = rb;
            this.animator = animator;
            this.inputSignatory = inputSignatory;
            GetHashAnim();
        }
        private void GetHashAnim()
        {
            _dodgeHash = Animator.StringToHash("Dodge");
        }

        public override void Move(Vector3 direction)
        {
            float verticalMovement = rb.velocity.y;
            Vector3 moveDirection = direction.normalized * moveSpeed;
            rb.velocity = new Vector3(moveDirection.x, verticalMovement, moveDirection.z);
        }

        private Transform _follow;
        private Transform _look;
        
        public override IEnumerator Dash(Vector3 direction)
        {
            //moveController.enabled = false;
            if (!inputSignatory.isHeavyAttacked)
            {
                Transform oldDashParent = gameObject.transform;

                inputSignatory.IsMoving = false;
                inputSignatory.IsDashing = true;

                newDashParent.SetParent(null);
                gameObject.transform.SetParent(newDashParent);
                gameObject.transform.localPosition = Vector3.zero;

                // _follow = virtualCamera.Follow;
                //_look = virtualCamera.LookAt;
                //
                // virtualCamera.Follow = follow;
                // virtualCamera.LookAt = look;


                animator.SetTrigger(_dodgeHash);

                yield return new WaitForSeconds(dashDuration);

                oldDashParent.SetParent(null);
                newDashParent.SetParent(oldDashParent);
                inputSignatory.IsDashing = false;
                inputSignatory.IsMoving = true;
                newDashParent.transform.localPosition = new Vector3(0, 0, 0);
            }
            // Vector3 poss = oldDashParent.transform.position;
            //moveController.enabled = true;
            
            // virtualCamera.Follow = _follow;
            // virtualCamera.LookAt = _look;

        }
    }
}