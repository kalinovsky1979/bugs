using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawningArea : MonoBehaviour
{
	public Vector3 size = new Vector3(5, 1, 5); // Customize size as needed
	public Vector3 pos => gameObject.transform.position;

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue; // Set Gizmo color
		Gizmos.DrawWireCube(transform.position, size); // Draw wireframe cube
	}
}
