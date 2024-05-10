using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using TouchScript.Gestures;
using UnityEngine;

public class BugMover : MonoBehaviour
{
	private AudioSource audioSource;

	[SerializeField] private GameObject splashObj;
	[SerializeField] private float Speed = 1.0f;

	private Animator animator;
	private bool isDying = false;

	public float correctingYRotation = 0;

	public bugsManager bugsManager;

	public Vector3 targetPoint;
	//public Vector3 areaSize = new Vector3(20, 20, 20);
	//private Vector3 areaCenter = Vector3.zero;
	//private Vector3 targetDirection;

	private PressGesture pressGesture;

	private float arrivalThreshold = 0.8f;

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
		audioSource = GetComponent<AudioSource>();

		animator = GetComponent<Animator>();

		//targetPoint = GenerateRandomPoint(areaCenter, areaSize, transform.position.y);
		//targetDirection = CalculateDirection(transform.position, targetPoint);

		transform.LookAt(targetPoint);
		transform.rotation *= Quaternion.AngleAxis(correctingYRotation, Vector3.up);
	}

	void PlayDieSound()
	{
		// Play the sound if it is not already playing
		//if (!audioSource.isPlaying)
		//{
		//	audioSource.Play();
		//}

		audioSource.Play();
	}

	void PlayDie()
	{
		gameObject.GetComponent<Rigidbody>().isKinematic = true;
		gameObject.GetComponent<BoxCollider>().enabled = false;

		animator.SetTrigger("die");

		Instantiate(splashObj, gameObject.transform.position, Quaternion.Euler(-90,0,0));

		PlayDieSound();

		Destroy(gameObject, 1.3f);
		//Destroy(gameObject);
		//animator.Play(animationName);
		//StartCoroutine(WaitForAnimation(GetComponent<Animation>(), animationName));
	}

	//private IEnumerator WaitForAnimation(Animator anim)
	//private IEnumerator WaitForAnimation(Animation anim, string animName)
	//{
	//	yield return new WaitForSeconds(GetComponent<Animation>()[animName].length);
	//	Destroy(gameObject, 0.6f);
	//}

	// Update is called once per frame
	void Update()
	{
		//var pos = transform.position;
		//pos.z -= 0.001f;
		//transform.position = pos;

		if (isDying) return;

		if (CheckArrival(transform.position, targetPoint))
		{
			//targetPoint = GenerateRandomPoint(areaCenter, areaSize, transform.position.y);
			//targetDirection = CalculateDirection(transform.position, targetPoint);
			//transform.LookAt(targetPoint);
			//transform.rotation *= Quaternion.AngleAxis(correctingYRotation, Vector3.up);

			Destroy(gameObject);
		}
		else
		{
			MoveTowardsTarget();
		}
	}

	Vector3 GenerateRandomPoint(Vector3 center, Vector3 size, float y)
	{
		float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
		//float y = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
		float z = Random.Range(center.z - size.z / 2, center.z + size.z / 2);
		return new Vector3(x, y, z);
	}

	Vector3 CalculateDirection(Vector3 source, Vector3 target)
	{
		Vector3 direction = target - source;
		return direction.normalized; // Normalized to get direction only
	}

	void RotateObjectAlongDirection(Vector3 direction)
	{
		if (direction != Vector3.zero)
		{
			float singleStep = 1.0f * Time.deltaTime;

			Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);

			//transform.rotation = 
			//Quaternion targetRotation = Quaternion.LookRotation(direction);
			//transform.localRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 40.0f); // Smooth rotation
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
}
