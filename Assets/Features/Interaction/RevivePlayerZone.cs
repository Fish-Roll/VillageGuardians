// using System;
// using UnityEngine;
//
// namespace Features.Interaction
// {
//     public class RevivePlayerZone : MonoBehaviour
//     {
//         [SerializeField] private Interaction interaction; 
//         private void OnTriggerEnter(Collider other)
//         {
//             interaction.otherPlayerHealth = other.GetComponent<Health.Health>();
//             interaction.canInteract = true;
//         }
//     }
// }