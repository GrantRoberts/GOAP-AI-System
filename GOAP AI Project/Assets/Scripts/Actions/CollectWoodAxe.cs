using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWoodAxe : GOAPAction
{
	/// <summary>
	/// If a tool has been collected.
	/// </summary>
	private bool m_Collected = false;

	/// <summary>
	/// Constructor.
	/// </summary>
	public CollectWoodAxe()
	{
		AddPrecondition("hasWoodAxe", false);

		AddEffect("hasWoodAxe", true);
	}

	/// <summary>
	/// Reset the action.
	/// </summary>
	public override void DoReset()
	{
		m_Collected = false;
	}

	/// <summary>
	/// If the action has been completed.
	/// </summary>
	/// <returns>If the action has been completed.</returns>
	public override bool IsDone()
	{
		return m_Collected;
	}

	/// <summary>
	/// Check if the action needs the agent to be in range.
	/// </summary>
	/// <returns>True, this action requires the agent to be in range.</returns>
	public override bool RequiresInRange()
	{
		return true;
	}

	/// <summary>
	/// Check if the agent can find the base to collect a wood axe from.
	/// </summary>
	/// <param name="agent">The agent checking this action.</param>
	/// <returns>If a target was found.</returns>
	public override bool CheckProceduralPrecondition(GameObject agent)
	{
		m_Target = GameObject.FindGameObjectWithTag("Base");

		return m_Target != null;
	}

	/// <summary>
	/// Perform the action.
	/// </summary>
	/// <param name="agent">The agent performing the action.</param>
	/// <returns>If the action was completed.</returns>
	public override bool Perform(GameObject agent)
	{
		Inventory inv = agent.GetComponent<Inventory>();

		inv.SetTool("woodAxe");

		m_Collected = true;

		return m_Collected;
	}
}