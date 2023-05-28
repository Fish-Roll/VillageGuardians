using System;
using UnityEngine;

namespace Features.PickingUp
{
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField] private Gates gates;
        
        private void OnTriggerEnter(Collider other)
        {
            gates.Open();
        }
        
        private void OnTriggerExit(Collider other)
        {
            gates.Close();
        }
    }
}