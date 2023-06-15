using System;
using System.Collections;
using Features.TagsGame;
using UnityEngine;
using UnityEngine.UI;

namespace Features.PickingUp
{
    public class TagsGamePrompt : MonoBehaviour
    {
        [SerializeField] private GameObject[] prompts;
        [SerializeField] private string promptText;
        [SerializeField] private Text[] prompt;
        [SerializeField] private TagsField tagsField;

        private void Start()
        {
            StartCoroutine(CheckCanPlay());
        }

        private IEnumerator CheckCanPlay()
        {
            while (true)
            {
                if(tagsField.CanPlay)
                    Destroy(gameObject, 0.5f);
                
                yield return new WaitForSeconds(0.1f);
            }
        }
        
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