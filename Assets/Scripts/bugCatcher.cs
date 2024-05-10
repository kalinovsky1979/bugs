using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class gugCatcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// Check if the left mouse button was clicked
		if (Input.GetMouseButtonDown(0))
		{
			// Cast a ray from the camera to the mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check if the ray hits any object
			if (Physics.Raycast(ray, out hit))
			{
				// The ray hit this object
				GameObject clickedObject = hit.transform.gameObject;

				if (clickedObject.tag == "bag")
				{
					// You can now do something with the clicked object
					Debug.Log("Clicked on object: " + clickedObject.name);
				}

				//// You can now do something with the clicked object
				//Debug.Log("Clicked on object: " + clickedObject.name);
			}
		}
	}
}
