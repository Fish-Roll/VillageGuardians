using System;
using Cinemachine;
using UnityEngine;

namespace Features
{
    public class FocusCamera : MonoBehaviour
    {
        private static CinemachineVirtualCamera _camera;
        
        public void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
        }

        public static void FocusOnPlayer()
        {
            _camera.Follow = GameObject.Find("FollowTarget").transform;
            _camera.LookAt = GameObject.Find("LookAt").transform;
        }

        public void FocusOnEnemy()
        {
            
        }

        public void Unfocus()
        {
            
        }
    }
}