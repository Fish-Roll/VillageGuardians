using System.Collections;
using UnityEngine;

namespace Features.Attack.Abstract
{
    public abstract class BaseAttack : MonoBehaviour
    {
        /// <summary>
        /// Activate weapon collider
        /// </summary>
        public abstract IEnumerator Attack();

        protected abstract void ResetAttack();
        public abstract void Init(Animator animator);
    }
}