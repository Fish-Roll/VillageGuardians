using System;
using UnityEngine;

namespace Features.UI
{
    public class Control : MonoBehaviour
    {
        [SerializeField] private GameObject keyboardBoy;
        [SerializeField] private GameObject keyboardGirl;
        
        [SerializeField] private GameObject gamepadBoy;
        [SerializeField] private GameObject gamepadGirl;
        
        private ControlController _controlController;
        public void Awake()
        {
            _controlController = FindObjectOfType<ControlController>();
            if (_controlController.IsGirlKeyboard)
            {
                gamepadGirl.SetActive(false);
                gamepadBoy.SetActive(true);

                keyboardGirl.SetActive(true);
                keyboardBoy.SetActive(false);
            }
            else
            {
                keyboardGirl.SetActive(false);
                keyboardBoy.SetActive(true);
                
                gamepadGirl.SetActive(true);
                gamepadBoy.SetActive(false);
            }
        }
    }
}