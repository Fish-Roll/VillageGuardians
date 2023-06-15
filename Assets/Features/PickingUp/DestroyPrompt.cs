using System;
using UnityEngine;

namespace Features.PickingUp
{
    public class DestroyPrompt : MonoBehaviour
    {
        [SerializeField] private GameObject prompts;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                Destroy(prompts, 0.5f);
        }
    }
}