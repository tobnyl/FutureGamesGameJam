using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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

	// Update is called once per frame
	void Update()
	{

	}

	static string GenTimeSpanFromHours(double seconds)
	{
		// Create a TimeSpan object and TimeSpan string from 
		// a number of hours.
		System.TimeSpan interval = System.TimeSpan.FromSeconds(seconds);
		string timeInterval = interval.ToString();

		// Pad the end of the TimeSpan string with spaces if it 
		// does not contain milliseconds.
		int pIndex = timeInterval.IndexOf(':');
		pIndex = timeInterval.IndexOf('.', pIndex);
		if (pIndex < 0) timeInterval += "        ";
		return timeInterval;
	}
}
