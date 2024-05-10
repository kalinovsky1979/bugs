using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bugsManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI Text;

	int scores = 0;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void AddScore(int scores)
	{
		this.scores += scores;
		Text.text = this.scores.ToString();
		GameResult.Instance.playerScore = this.scores;
	}
}
