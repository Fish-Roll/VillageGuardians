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
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private AudioSource dashSound;
        [SerializeField] private AudioSource moveSound;
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
            if (!moveSound.isPlaying)
                moveSound.Play();
            rb.velocity = new Vector3(moveDirection.x, verticalMovement, moveDirection.z);
        }
        
        public override IEnumerator Dash(Vector3 direction)
        {
            moveSound.Stop();
            if (!inputSignatory.IsDashing)
            {
                inputSignatory.IsDashing = true;
                particleSystem.Play();
                dashSound.Play();

                rb.AddForce(playerModel.transform.forward * dashSpeed, ForceMode.Impulse);
                Material materials = oldMaterials[0].material;
                for (int i = 0; i < oldMaterials.Length; i++)
                {
                    oldMaterials[i].material = newMaterials;
                }

                yield return new WaitForSeconds(dashDuration);
                for (int i = 0; i < oldMaterials.Length; i++)
                {
                    oldMaterials[i].material = materials;
                }

                inputSignatory.IsDashing = false;
                yield return null;
            }
        }
    }
}