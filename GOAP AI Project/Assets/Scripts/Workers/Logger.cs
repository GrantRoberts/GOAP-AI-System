using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Worker))]
public class Logger : Worker
{
	/// <summary>
	/// Create the goal to collect wood.
	/// </summary>
	/// <returns>The goal to collect wood.</returns>
	public override HashSet<KeyValuePair<string, object>> CreateWorldGoal()
	{
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

		goal.Add(new KeyValuePair<string, object>("collectWood", true));
		return goal;
	}
}
