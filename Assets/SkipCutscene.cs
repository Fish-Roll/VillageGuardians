using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipCutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    // Start is called before the first frame update
    void Update()
    {
        if (player.isPaused || Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("Main Menu");
    }
}
