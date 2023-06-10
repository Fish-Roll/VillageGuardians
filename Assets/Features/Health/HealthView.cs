using UnityEngine;
using UnityEngine.UI;

namespace Features.Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        
        public Slider HealthSlider => healthSlider;
        
    }
}