using System;
using System.Collections;
using UnityEngine;

namespace Features
{
    public class CheckConnection : MonoBehaviour
    {
        [SerializeField] private GameObject checkConnectionWindow;
        [SerializeField] private GameObject connectionWindow;
        private bool activated;
        private void Awake()
        {
            StartCoroutine(ConnectionChecker());
        }

        private void Update()
        {
            connectionWindow.transform.Rotate(Vector3.forward * Time.deltaTime);
        }

        private IEnumerator ConnectionChecker()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    if(!activated){
                        checkConnectionWindow.SetActive(true);
                        Time.timeScale = 0;
                    }
                }
                else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
                {
                    if (activated)
                    {
                        checkConnectionWindow.SetActive(false);
                        Time.timeScale = 1;
                    }
                }
            }
        }
    }
}