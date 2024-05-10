using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.Events;

public class buttonGesturePressHandler : MonoBehaviour
{
	private PressGesture pressGesture;

	public UnityEvent onPress;

	// Start is called before the first frame update
	void Start()
    {
		pressGesture = GetComponent<PressGesture>();

		if (pressGesture != null)
			pressGesture.Pressed += PressGesture_Pressed;
	}

	private void PressGesture_Pressed(object sender, System.EventArgs e)
	{
		onPress?.Invoke();
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
