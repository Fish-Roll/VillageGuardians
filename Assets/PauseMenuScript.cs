using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject controlWindow;
    
    public void OpenControl()
    {
        controlWindow.SetActive(true);
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void Continue(GameObject window)
    {
        Time.timeScale = 1;
        window.SetActive(false);
    }
    
    public void Close(GameObject window)
    {
        window.SetActive(false);
    }
}
