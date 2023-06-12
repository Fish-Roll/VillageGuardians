using UnityEngine;

namespace Features
{
    public class LeverGates : MonoBehaviour
    {
        [SerializeField] private Gatee[] gates;
        
        public void Open()
        {
            gates[0].enabled = true;
            gates[1].enabled = true;
        }
    }
}