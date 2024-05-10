using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class returnMainMenuTap : MonoBehaviour
{
    private TapGesture tapGesture;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnEnable()
	{
		tapGesture = GetComponent<TapGesture>();

        if(tapGesture != null)
        {
			tapGesture.Tapped += TapGesture_Tapped;
		}
        else
        {
			Debug.Log("No TapGesture");
		}
	}

	private void TapGesture_Tapped(object sender, System.EventArgs e)
	{
        Debug.Log("tapped to return main menu");
	}

	private void OnDisable()
	{
		if (tapGesture != null)
			tapGesture.Tapped -= TapGesture_Tapped;
	}
}
