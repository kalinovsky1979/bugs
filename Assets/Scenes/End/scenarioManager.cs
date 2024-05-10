using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class scenarioManager : MonoBehaviour
{
	public List<nextScenarioItem> scenarioItems = new List<nextScenarioItem>();

	// Start is called before the first frame update
	void Start()
	{
		foreach (var item in scenarioItems)
		{
			item.gameObject.SetActive(false);
		}

		StartCoroutine(handleScenario(scenarioItems));

		//Debug.Log($"start scenario handling: {scenarioItems.Count} things");
	}

	private IEnumerator handleScenario(List<nextScenarioItem> scenarios)
	{
		foreach (var scenario in scenarios)
		{
			if (scenario == null)
				continue;

			if (scenario.delayBeforeStart != 0)
				yield return new WaitForSeconds(scenario.delayBeforeStart);

			scenario.gameObject.SetActive(true);

			yield return new WaitForSeconds(scenario.ownPlayTime);
		}
	}
}