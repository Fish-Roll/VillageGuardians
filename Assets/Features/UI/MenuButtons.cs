using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private string levelName;
    
    [Header("Windows")]
    [SerializeField] private GameObject findLobby;
    [SerializeField] private GameObject control;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private AudioSource buttonAudio;
    [SerializeField] private GameObject teachMenu;
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenTeachMenu()
    {
        buttonAudio.Play();
        mainMenu.SetActive(false);
        findLobby.SetActive(false);
        teachMenu.SetActive(true);
    }
    
    public void OpenGameScene()
    {
        buttonAudio.Play();
        SceneManager.LoadScene(levelName);
    }
    
    public void FindLobby()
    {
        buttonAudio.Play();
        mainMenu.SetActive(false);
        findLobby.SetActive(true);
    }
    
    public void ControlWindow()
    {
        buttonAudio.Play();
        mainMenu.SetActive(false);
        control.SetActive(true);
    }

    public void Exit()
    {
        buttonAudio.Play();
        Application.Quit();
    }

    public void Close(GameObject window)
    {
        buttonAudio.Play();
        window.SetActive(false);
        mainMenu.SetActive(true);
    }
    
}
