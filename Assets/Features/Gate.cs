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

        private void Start()
        {
            StartCoroutine(RotateCoroutine(endAngle, duration));
        }
        
        private IEnumerator RotateCoroutine(Vector3 rotationEulerAngles, float rotationDuration)
        {
            Quaternion currentRotation = lever.localRotation;
            Quaternion goalRotation = currentRotation * Quaternion.Euler(rotationEulerAngles);

            float timePassed = 0.0f;
            float fracTime = 0.0f;

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