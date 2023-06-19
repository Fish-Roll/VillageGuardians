using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    [SerializeField] private string cameraName;
    public Camera camera;
    
    private void Awake()
    {
        var cameras = FindObjectsOfType<Camera>();
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].name == cameraName)
                camera = cameras[i];
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(camera.transform);
    }
}
