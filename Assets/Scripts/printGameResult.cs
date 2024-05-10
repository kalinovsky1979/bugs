using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class printGameResult : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI text;

	// Start is called before the first frame update
	void Start()
	{
		text.text = GameResult.Instance.playerScore.ToString();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
