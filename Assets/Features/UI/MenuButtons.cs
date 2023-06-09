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
    
    public void OpenGameScene()
    {
        SceneManager.LoadScene(levelName);
    }
    
    public void FindLobby()
    {
        mainMenu.SetActive(false);
        findLobby.SetActive(true);
    }
    
    public void ControlWindow()
    {
        mainMenu.SetActive(false);
        control.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Close(GameObject window)
    {
        window.SetActive(false);
        mainMenu.SetActive(true);
    }
}
