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
                keyboardGirl.SetActive(true);
                keyboardBoy.SetActive(false);
                
                gamepadGirl.SetActive(false);
                gamepadBoy.SetActive(true);
            }
        }
    }
}