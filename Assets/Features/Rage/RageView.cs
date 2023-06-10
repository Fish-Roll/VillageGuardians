using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Rage
{
    public class RageView : MonoBehaviour
    {
        [SerializeField] private Slider rage;
        public Slider RageSlider => rage;
    }
}