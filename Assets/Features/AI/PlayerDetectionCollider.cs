using System;
using UnityEngine;

namespace Features.AI
{
    public class PlayerDetectionCollider : MonoBehaviour
    {
        private Action<GameObject> _triggerEnter;
        private Action<GameObject> _triggerExit;
        
        public void Init(Action<GameObject> triggerEnter, Action<GameObject> triggerExit)
        {
            _triggerEnter = triggerEnter;
            _triggerExit = triggerExit;
        }

        private void OnTriggerStay(Collider other)
        {
            _triggerEnter.Invoke(other.gameObject);
        }
        
        private void OnTriggerExit(Collider other)
        {
            _triggerExit.Invoke(other.gameObject);
        }
    }
}