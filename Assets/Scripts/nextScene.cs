using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartGame()
	{
		SceneManager.LoadScene("GamingScene");
	}

	public void GoMainMenu()
	{
		SceneManager.LoadScene("MainMenuScene");
	}

	public void QuitGame()
	{
		// Application.Quit() does not work in the editor or web player,
		// so it's often good to have a way to check it is being called.
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;  // Stop playing the game in the Unity Editor
#else
            Application.Quit();  // Quit the game when built
#endif
	}
}
