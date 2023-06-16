using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDeath : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(StopGame());
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
