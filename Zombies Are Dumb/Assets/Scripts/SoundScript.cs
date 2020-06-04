using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{

    private Music music;
    public Button musicToggleButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("El start funciona");
        music = GameObject.FindObjectOfType<Music>();
        UpdateIcon();
    }

    public void PauseMusic()
    {
        Debug.Log("Boton pulsado");
        music.ToggleSound();
        UpdateIcon();
    }

    

    private void UpdateIcon()
    {
        Debug.Log("El updateIcon funciona");

        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            Debug.Log("El audio a 0.5");
            AudioListener.volume = 0.5f;
            musicToggleButton.GetComponent<Image>().sprite = musicOnSprite;
        }

        else
        {
            Debug.Log("El audio a 0");
            AudioListener.volume = 0;
            musicToggleButton.GetComponent<Image>().sprite = musicOffSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
