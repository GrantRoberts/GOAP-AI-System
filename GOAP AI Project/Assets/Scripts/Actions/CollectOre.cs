using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOre : GOAPAction
{
	/// <summary>
	/// If ore has been mined.
	/// </summary>
	private bool m_Mined = false;

	/// <summary>
	/// When the action was started.
	/// </summary>
	private float m_StartTime = 0.0f;

	/// <summary>
	/// How long it takes to complete this action.
	/// </summary>
	public float m_WorkDuration = 5.0f;

	public float m_WorkHunger = 2.0f;

	/// <summary>
	/// Constructor.
	/// </summary>
	public CollectOre()
	{
		AddPrecondition("hasOrePick", true);
		AddPrecondition("hasOre", false);

		AddEffect("hasOre", true);
	}

	/// <summary>
	/// Reset the variables for this action.
	/// </summary>
	public override void DoReset()
	{
		m_Mined = false;
		m_StartTime = 0.0f;
	}

	/// <summary>
	/// If this action has been performed.
	/// </summary>
	/// <returns>If the agent has mined ore.</returns>
	public override bool IsDone()
	{
		return m_Mined;
	}

	/// <summary>
	/// Check if an agent must be in range to complete this action.
	/// </summary>
	/// <returns>True, an agent must be in range for this action.</returns>
	public override bool RequiresInRange()
	{
		return true;
	}

	/// <summary>
	/// Check for the closest ore vein to mine ore from.
	/// </summary>
	/// <param name="agent">The agent we are checking for.</param>
	/// <returns>If an ore vein was found.</returns>
	public override bool CheckProceduralPrecondition(GameObject agent)
	{
		GameObject[] veins = GameObject.FindGameObjectsWithTag("Ore");
		GameObject closest = veins[0];
		float closestDistance = (closest.transform.position - agent.transform.position).magnitude;

		foreach(GameObject v in veins)
		{
			float dist = (v.transform.position - agent.transform.position).magnitude;
			if (dist < closestDistance)
			{
				closest = v;
				closestDistance = dist;
			}
		}

		if (closest == null)
			return false;

		m_Target = closest;

		return closest != null;
	}

	/// <summary>
	/// Perform the collect ore action.
	/// </summary>
	/// <param name="agent">The agent performing this action.</param>
	/// <returns>If the action was performed this frame.</returns>
	public override bool Perform(GameObject agent)
	{
		if (m_Target.activeSelf != false)
		{
			if (m_StartTime == 0.0f)
				m_StartTime = Time.time;

			if (Time.time - m_StartTime > m_WorkDuration)
			{
				Inventory inv = agent.GetComponent<Inventory>();
				inv.IncreaseOre(1);
				m_Mined = true;
				agent.GetComponent<Worker>().DecreaseHunger(m_WorkHunger);
			}
			return true;
		}
		else
			return false;
	}
}
