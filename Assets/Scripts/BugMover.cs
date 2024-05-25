using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using TouchScript.Gestures;
using UnityEngine;

public class BugMover : MonoBehaviour, IBugMover
{
	private AudioSource audioSource;

	[SerializeField] private GameObject splashObj;
	[SerializeField] private float Speed = 1.0f;

	private Animator childObjectAnimator = null;
	private Animator parentObjectAnimator = null;
	private bool isDying = false;

	public float correctingYRotation = 0;

	private bugManager bugsManager;

	public Vector3 targetPoint;

	private PressGesture pressGesture;

	private float arrivalThreshold = 0.8f;

	[SerializeField] private float yPos;

	private void OnEnable()
	{
		pressGesture = GetComponent<PressGesture>();

		if(pressGesture != null)
			pressGesture.Pressed += PressGesture_Pressed;
	}

	private void OnDisable()
	{
		if (pressGesture != null)
			pressGesture.Pressed -= PressGesture_Pressed;
	}

	private void PressGesture_Pressed(object sender, System.EventArgs e)
	{
		if (pressGesture == null) return;

		if (isDying) return;

		isDying = true;

		bugsManager.AddScore(1);

		//var cube = gameObject.transform;

		////cube.transform.localScale = cube.transform.localScale.

		//cube.GetComponent<Rigidbody>().AddForce(10.0f * Vector3.up, ForceMode.Impulse);
		////cube.GetComponent<Rigidbody>().AddTorque(10.0f * cube.transform.forward, ForceMode.Impulse);

		//Destroy(gameObject, 0.6f);

		PlayDie();
	}

	// Start is called before the first frame update
	void Start()
	{
		bugsManager = FindObjectOfType<bugManager>();

		if (bugsManager == null)
		{
			Debug.LogWarning("BugManager not found in the scene.");
		}

		audioSource = GetComponent<AudioSource>();

		parentObjectAnimator = GetComponent<Animator>();

		Vector3 landedPos = new Vector3(transform.position.x, yPos, transform.position.z);

		transform.position = landedPos;

		transform.LookAt(targetPoint);
		transform.rotation *= Quaternion.AngleAxis(correctingYRotation, Vector3.up);
	}

	void PlayDieSound()
	{
		audioSource.pitch = Random.Range(0.5f, 1.5f);
		audioSource.Play();
	}

	void PlayDie()
	{
		gameObject.GetComponent<Rigidbody>().isKinematic = true;
		gameObject.GetComponent<BoxCollider>().enabled = false;

		if (parentObjectAnimator != null)
			parentObjectAnimator.SetTrigger("die");
		else if (childObjectAnimator != null)
			childObjectAnimator.SetTrigger("die");

		float splashYRotation = Random.Range(0, 180);

		//Instantiate(splashObj, gameObject.transform.position, Quaternion.Euler(-90, splashYRotation, 0));
		Instantiate(splashObj, gameObject.transform.position, Quaternion.identity);

		PlayDieSound();

		Destroy(gameObject, 1.3f);
	}

	// Update is called once per frame
	void Update()
	{
		if (isDying) return;

		if (CheckArrival(transform.position, targetPoint))
		{
			Destroy(gameObject);
		}
		else
		{
			MoveTowardsTarget();
		}
	}

	void MoveTowardsTarget()
	{
		if (targetPoint != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPoint, Speed * Time.deltaTime);
		}
	}

	bool CheckArrival(Vector3 currentPos, Vector3 targetPos)
	{
		Vector2 p1 = new Vector2(currentPos.x, currentPos.z);
		Vector2 p2 = new Vector2(targetPos.x, targetPos.z);

		return Vector2.Distance(p1, p2) <= arrivalThreshold;
	}

	public void SetTargetPoint(Vector3 tp)
	{
		this.targetPoint = tp;
	}
}
