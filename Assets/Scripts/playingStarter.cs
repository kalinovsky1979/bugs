using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playingStarter : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI timerText;
	public float timeRemaining = 5f;  // Start with 90 seconds
	public List<GameObject> drivingItems;

	// Start is called before the first frame update
	void Start()
    {
		UpdateTimerDisplay();

		foreach (var item in drivingItems)
        {
            item.gameObject.SetActive(false);
        }
    }

	bool completed = false;

    // Update is called once per frame
    void Update()
    {
		if (timeRemaining > 1)
		{
			timeRemaining -= Time.deltaTime;  // Decrease the time remaining

			if (timeRemaining < 1) timeRemaining = 1;

			UpdateTimerDisplay();
		}
		else
		{
			if (!completed)
			{
				foreach (var item in drivingItems)
				{
					item.gameObject.SetActive(true);
				}

				timerText.gameObject.SetActive(false);

				Destroy(gameObject);
			}

			completed = true;
		}
	}

	void UpdateTimerDisplay()
	{
		if (timerText != null)
		{
			timerText.text = Mathf.FloorToInt(timeRemaining).ToString();
		}
	}
}
