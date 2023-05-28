using System.Collections;
using Features.Input;
using Features.Movement.Abstract;
using UnityEngine;

namespace Features.Movement.Boy
{
    public class BoyMovement : BasePlayerMovement
    {
        [SerializeField] private Transform newDashParent;
        
        private int _dodgeHash;

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

        public override IEnumerator Dash(Vector3 direction)
        {
            Transform oldDashParent = gameObject.transform;
            newDashParent.SetParent(null);
            gameObject.transform.SetParent(newDashParent);
            animator.SetTrigger(_dodgeHash);
            
            yield return new WaitForSeconds(dashDuration);
            
            inputSignatory.IsDashing = false;
            oldDashParent.SetParent(null);
            newDashParent.SetParent(oldDashParent);

            newDashParent.transform.position = oldDashParent.transform.position;
        }
    }
}