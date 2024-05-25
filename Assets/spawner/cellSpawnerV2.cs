using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellSpawnerV2 : MonoBehaviour
{
	//[SerializeField] private List<GameObject> objectsToSpawn = new List<GameObject>();// возможность в одном спаунере определенные виды жуков; но вариант, что извне даются жуки
	[SerializeField] private targetArea targetArea;

	private int objectsInside = 0;

	public bool IsFree => objectsInside == 0;

	// Start is called before the first frame update
	void Start()
	{
		

		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void TrySpown(GameObject o)
	{
		if (objectsInside > 0) return;// один или более объектов в боксе, нельзя создавать еще, чтобы наложился сверху

		var bMover = o.GetComponent<IBugMover>();

		bMover.SetTargetPoint(GenerateRandomPoint(targetArea.gameObject.transform.position, targetArea.size));
		Instantiate(o, transform.position, Quaternion.identity);
	}

	private void OnTriggerEnter(Collider other)
	{
		objectsInside++;
	}

	private void OnTriggerExit(Collider other)
	{
		objectsInside--;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue; // Set Gizmo color
		Gizmos.DrawWireSphere(transform.position, 0.2f);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.gray; // Set Gizmo color
		Gizmos.DrawWireSphere(transform.position, 0.2f);
	}

	Vector3 GenerateRandomPoint(Vector3 center, Vector3 size)
	{
		float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
		float y = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
		float z = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

		var res = new Vector3(x, y, z);

		return res;
	}
}
