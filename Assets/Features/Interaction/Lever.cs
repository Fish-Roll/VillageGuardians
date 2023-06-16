using System;
using System.Collections;
using UnityEngine;

namespace Features.Interaction
{
    public class Lever : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private Vector3 endAngle;
        [SerializeField] private Transform lever;
        [SerializeField] private AudioSource turnLeverSound;
        private bool _turned;
        private void Start()
        {
            if (_turned) return;
            StartCoroutine(RotateCoroutine(endAngle, duration));
        }
        
        private IEnumerator RotateCoroutine(Vector3 rotationEulerAngles, float rotationDuration)
        {
            _turned = true;
            Quaternion currentRotation = lever.localRotation;
            Quaternion goalRotation = currentRotation * Quaternion.Euler(rotationEulerAngles);

            float timePassed = 0.0f;
            float fracTime = 0.0f;
            turnLeverSound.Play();
            while (fracTime < 1)
            {
                lever.transform.localRotation = Quaternion.Lerp(currentRotation, goalRotation, fracTime);

                timePassed += Time.deltaTime;

                fracTime = timePassed / rotationDuration;

                yield return null;
            }
            lever.localRotation = goalRotation;
        }
    }
}