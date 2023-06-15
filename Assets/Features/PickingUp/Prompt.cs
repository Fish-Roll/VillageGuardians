using System;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

namespace Features.PickingUp
{
    public class Prompt : MonoBehaviour
    {
        [SerializeField] private GameObject[] prompts;
        [SerializeField] private string promptText;
        [SerializeField] private Text[] prompt;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.name == "KeyboardBoy" || other.name == "GamepadBoy")
                {
                    prompt[0].text = promptText;
                    prompts[0].SetActive(true);
                }
                else if (other.name == "KeyboardGirl" || other.name == "GamepadGirl")
                {
                    prompt[1].text = promptText;
                    prompts[1].SetActive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.name == "KeyboardBoy" || other.name == "GamepadBoy")
                {
                    prompts[0].SetActive(false);
                }
                else if (other.name == "KeyboardGirl" || other.name == "GamepadGirl")
                {
                    prompts[1].SetActive(false);
                }
            }
        }
    }
}