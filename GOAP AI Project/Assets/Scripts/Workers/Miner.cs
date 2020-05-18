using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Worker))]
public class Miner : Worker
{
	/// <summary>
	/// Create the goal to collect ore.
	/// </summary>
	/// <returns>The goal to collect ore.</returns>
    public override HashSet<KeyValuePair<string, object>> CreateWorldGoal()
	{
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

		goal.Add(new KeyValuePair<string, object>("collectOre", true));

		return goal;
	}
}