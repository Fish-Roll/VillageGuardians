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
        [SerializeField] private Material newMaterials;
        [SerializeField] private SkinnedMeshRenderer[] oldMaterials;
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
            // playerModel.SetActive(false);
            // rb.AddForce(playerModel.transform.forward * dashSpeed, ForceMode.Impulse);
            Material materials = oldMaterials[0].material;
            for (int i = 0; i < oldMaterials.Length; i++)
            {
                oldMaterials[i].material = newMaterials;
            }
            yield return new WaitForSeconds(2.5f);
            for (int i = 0; i < oldMaterials.Length; i++)
            {
                oldMaterials[i].material = materials;
            }
            // playerModel.SetActive(true);
            inputSignatory.IsDashing = false;
            yield return null;
        }
    }
}