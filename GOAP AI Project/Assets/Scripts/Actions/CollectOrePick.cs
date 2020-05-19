using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOrePick : GOAPAction
{
	/// <summary>
	/// If an ore pick has been collected.
	/// </summary>
	private bool m_Collected = false;

	/// <summary>
	/// Constructor.
	/// </summary>
	public CollectOrePick()
	{
		AddPrecondition("hasOrePick", false);

		AddEffect("hasOrePick", true);
	}

	/// <summary>
	/// Reset the action.
	/// </summary>
	public override void DoReset()
	{
		m_Collected = false;
	}

	/// <summary>
	/// If an ore pick has been collected.
	/// </summary>
	/// <returns>If the agent has collected an ore pick./returns>
	public override bool IsDone()
	{
		return m_Collected;
	}

	/// <summary>
	/// If the agent has to be in range to complete this action.
	/// </summary>
	/// <returns>True, the agent must be in range of the base.</returns>
	public override bool RequiresInRange()
	{
		return true;
	}

	/// <summary>
	/// Check if the agent can find the base to collect an ore pick from.
	/// </summary>
	/// <param name="agent">The agent checking this action.</param>
	/// <returns>If a target was found.</returns>
	public override bool CheckProceduralPrecondition(GameObject agent)
	{
		m_Target = GameObject.FindGameObjectWithTag("Base");

		return m_Target != null;
	}

	/// <summary>
	/// Collect a ore pick.
	/// </summary>
	/// <param name="agent">The agent performing the action.</param>
	/// <returns>If the action was completed.</returns>
	public override bool Perform(GameObject agent)
	{
		Inventory inv = agent.GetComponent<Inventory>();

		inv.SetTool("orePick");

		m_Collected = true;

		return m_Collected;
	}
}