using System;
using UnityEngine;

namespace Features.Rage
{
    [Serializable]
    public class RageModel
    {
        [SerializeField] private float rageMaxValue;
        [SerializeField] private float rageValue;

        public float RageValue => rageValue;
        public float RageMaxValue => rageMaxValue;

        public void Add(float value)
        {
            rageValue = Mathf.Clamp(rageValue + value, 0, rageMaxValue);
        }

        public void Subtract(float value)
        {
            rageValue = Mathf.Clamp(rageValue - value, 0, rageMaxValue);
        }
    }
}