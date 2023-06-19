using UnityEngine;
using UnityEngine.UI;

namespace Features.Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider healthSlider2;
        
        public Slider HealthSlider => healthSlider;
        public Slider HealthSlider2 => healthSlider2;
        
    }
}