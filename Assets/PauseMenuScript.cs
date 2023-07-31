using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject controlWindow;
    [SerializeField] private AudioSource pushSound;
    public void OpenControl()
    {
        pushSound.Play();
        controlWindow.SetActive(true);
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void Continue(GameObject window)
    {
        Time.timeScale = 1;
        pushSound.Play();
        window.SetActive(false);
        BlockCursor();
    }
    
    public void Close(GameObject window)
    {
        pushSound.Play();
        window.SetActive(false);
    }

    public void BlockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
