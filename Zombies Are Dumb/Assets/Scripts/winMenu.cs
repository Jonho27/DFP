using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class winMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject playerUI;

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }


    }


    public void Resume()
    {
        AudioListener.volume = 0.5f;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }

    public void Pause()
    {
        AudioListener.volume = 0f;
        PauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }

    public void QuitGame()
    {
        Application.Quit();
    }    public void GoMenu()
    {
        Time.timeScale = 1f;
        FindObjectOfType<ProgressSceneLoader>().LoadScene("MainMenu");
    }
}
