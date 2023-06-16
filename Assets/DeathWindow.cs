using System;
using System.Collections;
using System.Collections.Generic;
using Features.Health;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathWindow : MonoBehaviour
{
    [SerializeField] private Checkpoints checkpoints;
    public int deadCount;
    
    public void Continue()
    {
        checkpoints.Respawn();
    }
    
    public void LoadScene()
    {
        SceneManager.LoadScene("Main Menu");
    }


}
