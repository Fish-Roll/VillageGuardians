using System.Collections;
using Features.Input;
using Features.Movement.Abstract;
using UnityEngine;

namespace Features.Movement.Girl
{
    public class GirlMovement : BasePlayerMovement
    {
        [SerializeField] private GameObject playerModel;
        [SerializeField] private float dashSpeed;
        
        public override void Init(Rigidbody rb, Animator animator, InputSignatory inputSignatory)
        {
            this.rb = rb;
            this.animator = animator;
            this.inputSignatory = inputSignatory;
        }

        public override void Move(Vector3 direction)
        {
            float verticalMovement = rb.velocity.y;
            Vector3 moveDirection = direction.normalized * moveSpeed;
            rb.velocity = new Vector3(moveDirection.x, verticalMovement, moveDirection.z);
        }
        
        public override IEnumerator Dash(Vector3 direction)
        {
            //float currentTime = 0;
            playerModel.SetActive(false);
            rb.AddForce(playerModel.transform.forward * dashSpeed, ForceMode.Impulse);
            yield return new WaitForSeconds(0.2f);
            playerModel.SetActive(true);
            inputSignatory.IsDashing = false;
            yield return null;
        }
    }
}