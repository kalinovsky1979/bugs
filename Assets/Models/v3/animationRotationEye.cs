using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationRotationEye : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 3f;

    public Vector3 rotationAxis = Vector3.one;

    void Update()
    {
		// Rotate GameObject A around its up axis (Y axis)
		transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
	}
}
