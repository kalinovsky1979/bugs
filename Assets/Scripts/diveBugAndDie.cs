using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diveBugAndDie : MonoBehaviour
{
	public float toY = -5.0f;

	public float speed = -0.5f;

	public bool goDive = false;

	private void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if(!goDive) return;

		// Get the current position of the object
		Vector3 currentPosition = transform.position;

		if(currentPosition.y < toY)
		{
			Destroy(gameObject);
		}

		// Calculate the new position by adding to the current y-coordinate
		float newY = currentPosition.y + speed * Time.deltaTime; // Time.deltaTime ensures smooth movement
		Vector3 newPosition = new Vector3(currentPosition.x, newY, currentPosition.z);

		// Update the object's position
		transform.position = newPosition;
	}
}
