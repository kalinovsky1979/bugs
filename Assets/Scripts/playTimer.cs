using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playTimer : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI timerText;
	public float timeRemaining = 90f;  // Start with 90 seconds

	[SerializeField] private bugSettings bugSettings;

	void Start()
	{
		var op = bugSettings.Options.options.FirstOrDefault();
		var value = op.values.FirstOrDefault(x => x.title.Equals("Play time"));
		timeRemaining = float.Parse(value.value);
	}

	// Update is called once per frame
	void Update()
	{
		if (timeRemaining > 0)
		{
			timeRemaining -= Time.deltaTime;  // Decrease the time remaining

			if (timeRemaining < 0) timeRemaining = 0;

			UpdateTimerDisplay();
		}
		else
		{
			EndGame();  // Call the function to end the game
		}
	}

	void UpdateTimerDisplay()
	{
		if (timerText != null)
		{
			timerText.text = Mathf.FloorToInt(timeRemaining).ToString();
		}
	}

	void EndGame()
	{
		// Here you can add whatever you want to happen when the timer runs out
		SceneManager.LoadScene("EndGame");  // Example: Load an end game scene
											// Alternatively, pause the game or trigger a game over state
											// Time.timeScale = 0;  // Uncomment this to pause the game
	}
}
