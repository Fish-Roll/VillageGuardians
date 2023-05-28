using System.Collections;
using Features.Input;
using UnityEngine;

namespace Features.Movement.Abstract
{
    public abstract class BasePlayerMovement : BaseMovement
    {
        [SerializeField] protected float dashDuration;
        protected InputSignatory inputSignatory;
        
        public abstract void Init(Rigidbody rb, Animator animator, InputSignatory inputSignatory);
        public abstract IEnumerator Dash(Vector3 direction);
    }
}