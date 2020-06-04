﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ProgressSceneLoaderSaving : MonoBehaviour
{
	[SerializeField]
	private Text progressText;
	[SerializeField]
	private Slider slider;



	private AsyncOperation operation;
	private Canvas canvas;
    public GameObject population;

    private void Awake()
	{
		canvas = GetComponentInChildren<Canvas>(true);
		DontDestroyOnLoad(gameObject);
	}

    public void LoadScene(string SceneName)
	{
		UpdateProgressUI(0);
		canvas.gameObject.SetActive(true);

		StartCoroutine(BeginLoad(SceneName));
	}

    private IEnumerator BeginLoad(string sceneName)
	{
        operation = SceneManager.LoadSceneAsync(sceneName);
		while (!operation.isDone)
		{
			UpdateProgressUI(operation.progress);
			yield return null;
		}

		UpdateProgressUI(operation.progress);
		operation = null;
		canvas.gameObject.SetActive(false);


	}

    private void UpdateProgressUI(float progress)
	{
		slider.value = progress;
		progressText.text = (int)(progress * 100f) + "%";
	}


}
