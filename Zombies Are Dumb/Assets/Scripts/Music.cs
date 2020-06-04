using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{

    static Music instance = null;

    public AudioClip menuTheme;
    public AudioClip mainTheme;
    public AudioClip winTheme;
    public AudioClip nothingTheme;
    public AudioClip level2Theme;
    public AudioClip level3Theme;

    private string sceneName;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void ToggleSound()
    {
        if(PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }

        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }
    }

    public void changeTheme()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            this.GetComponent<AudioSource>().clip = menuTheme;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Play();
            }
            
        }

        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            this.GetComponent<AudioSource>().clip = level2Theme;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Play();
            }

        }


        else if (SceneManager.GetActiveScene().name == "Win")
        {
            this.GetComponent<AudioSource>().clip = winTheme;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Play();
            }
        }

        else if (SceneManager.GetActiveScene().name == "Level1")
        {
            this.GetComponent<AudioSource>().clip = mainTheme;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Play();
            }
        }

        else if (SceneManager.GetActiveScene().name == "GameOver1")
        {
            this.GetComponent<AudioSource>().clip = nothingTheme;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Play();
            }
        }

        else if (SceneManager.GetActiveScene().name == "GameOver2")
        {
            this.GetComponent<AudioSource>().clip = nothingTheme;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Play();
            }
        }
        else if (SceneManager.GetActiveScene().name == "GameOver3")
        {
            this.GetComponent<AudioSource>().clip = nothingTheme;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Play();
            }
        }

        else if (SceneManager.GetActiveScene().name == "Intro")
        {
            this.GetComponent<AudioSource>().clip = nothingTheme;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Play();
            }
        }

        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            this.GetComponent<AudioSource>().clip = level3Theme;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Play();
            }
        }
    }


}
