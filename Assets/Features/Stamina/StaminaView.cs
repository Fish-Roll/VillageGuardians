using UnityEngine;
using UnityEngine.UI;

namespace Features.Stamina
{
    public class StaminaView : MonoBehaviour
    {
        [SerializeField] private Slider staminaSlider;

        public Slider StaminaSlider => staminaSlider;
    }
}