using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : Worker
{
	public override HashSet<KeyValuePair<string, object>> GetWorldState()
	{
		HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

		worldData.Add(new KeyValuePair<string, object>("hasWoodAxe", m_Inventory.GetTool() == "woodAxe"));
		worldData.Add(new KeyValuePair<string, object>("hasWoodHeld", m_Inventory.GetWood() > 0));
		worldData.Add(new KeyValuePair<string, object>("needFood", m_Hunger < m_HungerThreshold));

		return worldData;
	}

	/// <summary>
	/// Create the goal to collect wood.
	/// </summary>
	/// <returns>The goal to collect wood.</returns>
	public override HashSet<KeyValuePair<string, object>> CreateWorldGoal()
	{
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

		goal.Add(new KeyValuePair<string, object>("collectedWood", true));
		goal.Add(new KeyValuePair<string, object>("needFood", false));

		return goal;
	}
}
