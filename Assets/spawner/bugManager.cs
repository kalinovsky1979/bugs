using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class bugManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToSpawn;
	[SerializeField] private List<cellSpawnerV2> cellSpawners;

	private const int maxBugs = 20;

	// Start is called before the first frame update
	void Start()
    {
		//spanwMore(maxBugs);
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
			yield return new WaitForSeconds(0.7f);

			// спавним по количеству
			//GameObject[] bugs = GameObject.FindGameObjectsWithTag("bag");
			//spanwMore(maxBugs - bugs.Length);

			// если на сцене жуков 6 или меньше, спавним еще
			GameObject[] bugs = GameObject.FindGameObjectsWithTag("bag");

			if(bugs.Length >= 20)
				yield break;

			if (bugs.Length <= 15)
				spawnOne();
		}
	}

	private void spanwMore(int amount)
	{
		var freeSpwnCells = cellSpawners.Where(x => x.IsFree).ToList();

		Shuffle(freeSpwnCells);

		if (freeSpwnCells.Count < amount) amount = freeSpwnCells.Count;

		for (int i = 0; i < amount; i++)
		{
			int objectIndex = Random.Range(0, objectsToSpawn.Count);
			var obj = objectsToSpawn[objectIndex];

			var spawner = freeSpwnCells[i];

			spawner.TrySpown(obj);
		}
	}

	private void spawnOne()
	{
		var freeSpwnCells = cellSpawners.Where(x => x.IsFree).ToList();

		//Shuffle(freeSpwnCells);
		int objectIndex = Random.Range(0, objectsToSpawn.Count);
		var obj = objectsToSpawn[objectIndex];
		var spawner = freeSpwnCells[Random.Range(0, freeSpwnCells.Count)];
		spawner.TrySpown(obj);
	}

	void Shuffle<T>(List<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = UnityEngine.Random.Range(0, n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	[SerializeField] private TextMeshProUGUI Text;

	int scores = 0;

	public void AddScore(int scores)
	{
		this.scores += scores;
		Text.text = this.scores.ToString();
		GameResult.Instance.playerScore = this.scores;
	}
}
