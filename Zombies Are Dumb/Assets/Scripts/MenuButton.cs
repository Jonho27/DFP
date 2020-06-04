using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

    public static bool audioOn;

    public void Start()
    {

    }

    private void Update()
    {

    }

    public void PlayGame()
	{
        SceneManager.LoadScene("SampleScene");
    }

	public void QuitGame()
	{
		Application.Quit();
	}    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }    public void Options()
    {
        SceneManager.LoadScene("Options");
    }    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }    public void Intro()
    {
        SceneManager.LoadScene("Intro");
    }
}
