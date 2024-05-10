using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class terrainManager : MonoBehaviour
{
	private PressGesture pressGesture;
	private Camera activeCamera;
	public ParticleSystem Particles;

	private AudioSource audioSource;

	private void OnEnable()
	{
		pressGesture = GetComponent<PressGesture>();

		if (pressGesture != null)
			pressGesture.Pressed += PressGesture_Pressed;

		activeCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	private void PressGesture_Pressed(object sender, System.EventArgs e)
	{
		if (pressGesture == null) return;

		var ray = activeCamera.ScreenPointToRay(pressGesture.ScreenPosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit) && hit.transform == transform)
		{
			Instantiate(Particles, hit.point, Quaternion.Euler(-90f, 0f, 0f));
			PlayGroundHitSound();
		}

	}

	// Start is called before the first frame update
	void Start()
    {
		audioSource = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void PlayGroundHitSound()
	{
		// Play the sound if it is not already playing
		//if (!audioSource.isPlaying)
		//{
		//	audioSource.Play();
		//}

		audioSource.Play();
	}
}
