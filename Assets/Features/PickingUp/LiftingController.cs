using System;
using UnityEngine;

namespace Features.PickingUp
{
    public class LiftingController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out ILifted lifted))
            {
                lifted.Lift();
            }
        }
    }
}