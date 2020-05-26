using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Worker))]
public class Miner : Worker
{
	public override HashSet<KeyValuePair<string, object>> GetWorldState()
	{
		HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

		worldData.Add(new KeyValuePair<string, object>("hasOrePick", (m_Inventory.GetTool() == "orePick")));
		worldData.Add(new KeyValuePair<string, object>("hasOre", (m_Inventory.GetOre() > 0)));
		worldData.Add(new KeyValuePair<string, object>("needFood", (m_Hunger < m_HungerThreshold)));

		return worldData;
	}

	/// <summary>
	/// Create the goal to collect ore.
	/// </summary>
	/// <returns>The goal to collect ore.</returns>
	public override HashSet<KeyValuePair<string, object>> CreateWorldGoal()
	{
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

		goal.Add(new KeyValuePair<string, object>("collectOre", true));
		goal.Add(new KeyValuePair<string, object>("needFood", false));

		return goal;
	}
}