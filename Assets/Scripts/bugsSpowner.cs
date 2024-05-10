using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugsSpowner : MonoBehaviour
{
	[SerializeField] private bugsManager bugsManager;

    [SerializeField] private List<GameObject> objectsToSpawn;
	//[SerializeField] private Vector3 areaSize;
	//[SerializeField] private float yLevel;

	//[SerializeField] private Vector3 spownAreaA;
	//[SerializeField] private Vector3 spownAreaB;
	//[SerializeField] private float spownZCenterA;
	//[SerializeField] private float spownZCenterB;

	[SerializeField] private spawningArea areaA;
	[SerializeField] private spawningArea areaB;

	//private Vector3 areaCenter = Vector3.zero;

	private const int maxBugs = 10;

	// Start is called before the first frame update
	void Start()
    {
		spanwMore(maxBugs);
		StartCoroutine(SpawnEveryFiveSeconds());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator SpawnEveryFiveSeconds()
    {
		while (true) // This creates an infinite loop
		{
			yield return new WaitForSeconds(2f); // Wait for 5 seconds

			GameObject[] bugs = GameObject.FindGameObjectsWithTag("bag");

			spanwMore(maxBugs - bugs.Length);
		}
	}

	private void spanwMore(int amount)
	{
		for(int i = 0; i < amount; i++)
		{
			int randomIndex = Random.Range(0, objectsToSpawn.Count);

			int areaIndexFrom = Random.Range(1, 3);

			var obj = objectsToSpawn[randomIndex];

			var bMover = obj.GetComponent<BugMover>();

			bMover.bugsManager = this.bugsManager;
			//bMover.areaSize = this.areaSize;
			if (areaIndexFrom == 1)
			{
				bMover.targetPoint = GenerateRandomPoint(areaA.gameObject.transform.position, areaA.size);
				Instantiate(obj, GenerateRandomPoint(areaB.gameObject.transform.position, areaB.size), Quaternion.identity);
			}
			else
			{
				bMover.targetPoint = GenerateRandomPoint(areaB.gameObject.transform.position, areaB.size);
				Instantiate(obj, GenerateRandomPoint(areaA.gameObject.transform.position, areaA.size), Quaternion.identity);
			}
			//Instantiate(obj, GenerateRandomPoint(areaCenter, areaSize, 2/*obj.transform.position.y*/), Quaternion.identity);
		}
	}

	Vector3 GenerateRandomPoint(Vector3 center, Vector3 size)
	{
		float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
		float y = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
		float z = Random.Range(center.z - size.z / 2, center.z + size.z / 2);
		return new Vector3(x, y, z);
	}
}
