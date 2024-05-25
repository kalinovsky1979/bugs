using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class BugMoverCapsule : MonoBehaviour, IBugMover
{
	//[SerializeField] private GameObject splashBung;
	[SerializeField] private GameObject bung;
	[SerializeField] private GameObject splash;
	[SerializeField] private float Speed = 1.0f;

	[SerializeField] private Vector3 splashScale = Vector3.one;
	[SerializeField] private Vector3 bungScale = Vector3.one;

	private Animator childObjectAnimator = null;
	private Animator parentObjectAnimator = null;
	private bool isDying = false;

	private bugManager bugsManager;

	private clapPlayer clapPlayer;

	public Vector3 targetPoint;

	[SerializeField] private float yPos;

	private PressGesture pressGesture;

	private float arrivalThreshold = 0.8f;

	private void OnEnable()
	{
		pressGesture = GetComponent<PressGesture>();

		if (pressGesture != null)
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

		PlayDie();
	}

	// Start is called before the first frame update
	void Start()
	{
		bugsManager = FindObjectOfType<bugManager>();

		clapPlayer = FindObjectOfType<clapPlayer>();

		if (bugsManager == null)
		{
			Debug.LogWarning("BugManager not found in the scene.");
		}

		parentObjectAnimator = GetComponent<Animator>();

		var main_model_transformation = transform.GetChild(0);
		if (main_model_transformation != null)
		{
			var main_model = main_model_transformation.gameObject;
			if (main_model != null)
				childObjectAnimator = main_model.GetComponent<Animator>();
		}

		Vector3 landedPos = new Vector3(transform.position.x, yPos, transform.position.z);

		transform.position = landedPos;

		transform.LookAt(targetPoint);
	}

	void PlayDieSound()
	{
		clapPlayer.Play();
	}

	void PlayDie()
	{
		gameObject.GetComponent<Rigidbody>().isKinematic = true;
		//gameObject.GetComponent<BoxCollider>().enabled = false;
		gameObject.GetComponent<Collider>().enabled = false;

		/*
		 * bug prefab that is capsule
		 * parent object has animation "die" AND child has animation "die"
		 * child object, that is acually a model, has animation "walk" that is to stop
		 */

		if (parentObjectAnimator != null)
			parentObjectAnimator.SetTrigger("die");

		if (childObjectAnimator != null)
			childObjectAnimator.SetTrigger("die");

		float splashYRotation = Random.Range(0, 180);

		//var spl = Instantiate(splashObj, gameObject.transform.position, Quaternion.Euler(-90, splashYRotation, 0));
		var spl = Instantiate(splash, gameObject.transform.position, Quaternion.Euler(0, splashYRotation, 0));
		spl.transform.localScale = splashScale;

		var bng = Instantiate(bung, gameObject.transform.position, Quaternion.identity);
		bng.transform.localScale = bungScale;

		PlayDieSound();

		Destroy(gameObject, 1.3f);
	}

	// Update is called once per frame
	void Update()
	{
		//Debug.DrawLine(transform.position, targetPoint, Color.red);

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
		//return Vector3.Distance(transform.position, targetPoint) <= arrivalThreshold;
	}

	public void SetTargetPoint(Vector3 tp)
	{
		this.targetPoint = tp;
	}
}
