using System;
using System.Collections;
using UnityEngine;

namespace Features.Health
{
    public class Rage : MonoBehaviour
    {
        [SerializeField] private float maxRage;
        [SerializeField] private float currRage;

        [SerializeField] private float addedRage;
        [SerializeField] private float subRage;

        private bool canAdd;
        private void Awake()
        {
            currRage = 0;
        }

        private void AddRage()
        {
            if(canAdd)
                currRage = Mathf.Clamp(currRage + addedRage, 0, maxRage);
        }

        private IEnumerator Activate()
        {
            canAdd = false;
            yield return new WaitForSeconds(0.05f);
            currRage -= subRage;
        }
    }
}