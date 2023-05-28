using System.Collections;
using Features.Input;
using UnityEngine;

namespace Features.Movement
{
    public abstract class BaseMovement : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        
        protected Rigidbody rb;
        protected Animator animator;

        public abstract void Move(Vector3 direction);

    }
}