using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Worker
{
	public override HashSet<KeyValuePair<string, object>> GetWorldState()
	{
		HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

		worldData.Add(new KeyValuePair<string, object>("hasFarmingHoe", m_Inventory.GetTool() == "farmingHoe"));
		worldData.Add(new KeyValuePair<string, object>("needFood", m_Hunger < m_HungerThreshold));

		return worldData;
	}

	public override HashSet<KeyValuePair<string, object>> CreateWorldGoal()
	{
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

		goal.Add(new KeyValuePair<string, object>("collectWheat", true));
		goal.Add(new KeyValuePair<string, object>("needFood", false));

		return goal;
	}
}