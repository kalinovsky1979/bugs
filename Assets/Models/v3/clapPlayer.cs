//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class clapPlayer : MonoBehaviour
{
	[SerializeField] AudioSource[] clpas;

	private AudioSource[] _playlist;

	int currentIndex = 0;

	AudioSource[] generatePlaylist(AudioSource[] originalSonglist)
	{
		AudioSource[] newArray = new AudioSource[originalSonglist.Length * 5];

		AudioSource lastElement = originalSonglist[0];
		AudioSource secondLastElement = originalSonglist[0];

		for (int i = 0; i < newArray.Length; i++)
		{
			AudioSource element;

			// Randomly select an element from the original array
			do
			{
				element = originalSonglist[Random.Range(0, originalSonglist.Length)];
			}
			// Ensure that the same element is not repeated more than twice in a row
			while (element == lastElement && element == secondLastElement);

			newArray[i] = element;
			secondLastElement = lastElement;
			lastElement = element;
		}

		return newArray;
	}

	private void Start()
	{
		_playlist = generatePlaylist(clpas);

		//var str = string.Join(", ", _playlist.Select(x => x.name));
		//Debug.Log(str);
	}

	public void Play()
	{
		if(currentIndex >= _playlist.Length) currentIndex = 0;

		_playlist[currentIndex].Play();

		currentIndex++;
	}
}
