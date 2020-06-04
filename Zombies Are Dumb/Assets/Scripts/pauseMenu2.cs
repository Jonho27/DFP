using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseMenu2 : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject playerUI;
    public GameObject messageObjective;
    public Slider volumeSlider;
    bool exitGame;

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

        if (exitGame)
        {

        }
    }

    public void UpdateVolume()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = volumeSlider.value;
        }
    }

    public void Resume()
    {
        PauseMenu.isPaused = false;
        Cursor.visible = false;
        UpdateVolume();
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    public void Pause()
    {
        PauseMenu.isPaused = true;
        AudioListener.volume = 0f;
        PauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        messageObjective.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }

    public void GoMenu()
    {
        if (SceneManager.GetActiveScene().name == "Level2")
        {

            if (PlayerPrefs.GetInt("Muted", 0) == 0)
            {
                AudioListener.volume = volumeSlider.value;
            }
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");

        }

    }
}
