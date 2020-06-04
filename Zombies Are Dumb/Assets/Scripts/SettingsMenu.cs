using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{

    public static AudioSource audioSource;

    public void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void changeSound(bool activated)
    {
        if (activated)
        {
            MenuButton.audioOn = true;
            this.GetComponent<AudioSource>().volume = 0.5f;
        }

        else
        {
            MenuButton.audioOn = false;
            this.GetComponent<AudioSource>().volume = 0;
        }
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
