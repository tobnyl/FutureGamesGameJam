using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

	[SerializeField] Text timeText;
	TimeSpan span;

	// Use this for initialization
	void Start()
	{
		float _time = PlayerPrefs.GetFloat("Time");
		span = TimeSpan.FromSeconds(_time);

		timeText.text = ("You saved the moon for " + span.Minutes + " minutes and " + span.Seconds + " seconds!");

	}

	public void PlayAgain()
	{
		SceneManager.LoadScene(1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
