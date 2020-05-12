using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWood : GOAPAction
{
	/// <summary>
	/// If the agent has already collected wood.
	/// </summary>
	private bool m_Chopped = false;

	/// <summary>
	/// The time the agent started chopping.
	/// </summary>
	private float m_StartTime = 0.0f;
	/// <summary>
	/// How long it takes for the agent to collect wood.
	/// </summary>
	public float m_WorkDuration = 2.0f;

	/// <summary>
	/// Constructor.
	/// </summary>
	public CollectWood ()
	{
		// Agent needs to have a tool.
		//AddPrecondition("hasTool", true);
		// Agent can't currently have wood already
		AddPrecondition("hasWood", false);

		// This action causes the agent to have wood in it's inventory.
		AddEffect("hasWood", true);
	}

	/// <summary>
	/// Reset the action.
	/// </summary>
	public override void DoReset()
	{
		m_Chopped = false;
		m_StartTime = 0.0f;
	}

	/// <summary>
	/// Has the action been completed.
	/// </summary>
	/// <returns>If the agent has chopped a tree.</returns>
	public override bool IsDone()
	{
		return m_Chopped;
	}

	/// <summary>
	/// Check if an agent needs to be in range for this action.
	/// </summary>
	/// <returns>True, the agent must be in range of a tree to perform this action.</returns>
	public override bool RequiresInRange()
	{
		return true;
	}

	/// <summary>
	/// Find a tree to perform this action on.
	/// </summary>
	/// <param name="agent">The agent we are checking for.</param>
	/// <returns>If there is a tree the agent can chop for wood.</returns>
	public override bool CheckProceduralPrecondition(GameObject agent)
	{
		// Get all the trees in the scene.
		GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
		GameObject closest = trees[0];
		float closestDistance = (trees[0].transform.position - agent.transform.position).magnitude;

		// Find the closest tree.
		foreach(GameObject t in trees)
		{
			float dist = (t.transform.position - agent.transform.position).magnitude;
			if (dist < closestDistance)
			{
				closest = t;
				closestDistance = dist;
			}
		}

		if (closest == null)
			return false;

		// Target the closest tree.
		m_Target = closest;

		return closest != null;
	}

	/// <summary>
	/// Perform the chop tree action.
	/// </summary>
	/// <param name="agent">The agent performing this action.</param>
	/// <returns>If the action is being performed.</returns>
	public override bool Perform(GameObject agent)
	{
		if (m_StartTime == 0)
			m_StartTime = Time.time;

		if (Time.time - m_StartTime > m_WorkDuration)
		{
			Inventory inv = agent.GetComponent<Inventory>();
			inv.IncreaseWood(1);
			m_Chopped = true;
		}
		return true;
	}
}