using System;
using System.Collections;
using UnityEngine;

namespace Features
{
    public class Gate : MonoBehaviour
    {
        /// <summary>
        /// если duration < 0, ворота открыты перманентно 
        /// </summary>
        [SerializeField] private float duration;
        [SerializeField] private Vector3 endAngle;
        [SerializeField] private Transform lever;
        [SerializeField] private AudioSource openGateSound;
        [SerializeField] private float durationClose;
        [SerializeField] private Vector3 endCloseAngle;

        private bool _open;
        private bool _close;

        public void Open()
        {
            _close = false;
            _open = true;
            if(!openGateSound.isPlaying)
                openGateSound.Play();
            StartCoroutine(RotateCoroutine(endAngle, duration));
        }
        
        public void Close()
        {
            _open = false;
            _close = true;
            StartCoroutine(RotateCoroutine2(endAngle, durationClose));
        }
        
        private IEnumerator RotateCoroutine2(Vector3 rotationEulerAngles, float rotationDuration)
        {
            Quaternion currentRotation = lever.localRotation;
            Quaternion goalRotation = Quaternion.identity;

            float timePassed = 0.0f;
            float fracTime = 0.0f;

            while (fracTime < 1 && _close)
            {
                lever.transform.localRotation = Quaternion.Lerp(currentRotation, goalRotation, fracTime);

                timePassed += Time.deltaTime;

                fracTime = timePassed / rotationDuration;

                yield return null;
            }

            if (!_close) yield break;
            
            lever.localRotation = goalRotation;
        }

        
        private IEnumerator RotateCoroutine(Vector3 rotationEulerAngles, float rotationDuration)
        {
            Quaternion currentRotation = lever.localRotation;
            Quaternion goalRotation = Quaternion.Euler(rotationEulerAngles);

            float timePassed = 0.0f;
            float fracTime = 0.0f;

            while (fracTime < 1 && _open)
            {
                lever.transform.localRotation = Quaternion.Lerp(currentRotation, goalRotation, fracTime);

                timePassed += Time.deltaTime;

                fracTime = timePassed / rotationDuration;

                yield return null;
            }
            
            if (!_open) yield break;

            lever.localRotation = goalRotation;
        }
    }
}