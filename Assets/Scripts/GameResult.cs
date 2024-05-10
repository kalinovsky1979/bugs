using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : MonoBehaviour
{
	public static GameResult Instance;  // Singleton instance
	public int playerScore = 0;  // Example of data to persist across scenes

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);  // Prevents the GameManager from being destroyed on scene load
		}
		else
		{
			Destroy(gameObject);  // Ensures that only one GameManager exists
		}
	}
}
