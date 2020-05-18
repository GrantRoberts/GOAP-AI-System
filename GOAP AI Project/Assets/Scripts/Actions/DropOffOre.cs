using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffOre : GOAPAction
{
	/// <summary>
	/// If ore has been dropped off.
	/// </summary>
	private bool m_DroppedOffOre = false;

	/// <summary>
	/// Constructor.
	/// </summary>
	public DropOffOre()
	{
		// Must have ore for this action.
		AddPrecondition("hasOre", true);

		// Will no longer have ore.
		AddEffect("hasOre", false);
		// Will have collected ore as a result of this action.
		AddEffect("collectOre", true);
	}

	/// <summary>
	/// Reset having dropped ore off.
	/// </summary>
	public override void DoReset()
	{
		m_DroppedOffOre = false;
	}

	/// <summary>
	/// Check if this action is complete.
	/// </summary>
	/// <returns>If this action has been completed.</returns>
	public override bool IsDone()
	{
		return m_DroppedOffOre;
	}

	/// <summary>
	/// Check if this action requires the agent to be in range.
	/// </summary>
	/// <returns>True, the agent must be in range.</returns>
	public override bool RequiresInRange()
	{
		return true;
	}

	/// <summary>
	/// Check if this action can be completed.
	/// </summary>
	/// <param name="agent">The agent we are checking for.</param>
	/// <returns>If a target was found for this action.</returns>
	public override bool CheckProceduralPrecondition(GameObject agent)
	{
		m_Target = GameObject.FindGameObjectWithTag("Base");

		return m_Target != null;
	}

	/// <summary>
	/// Perform this action.
	/// </summary>
	/// <param name="agent">The agent performing this action.</param>
	/// <returns>If the action was performed this frame.</returns>
	public override bool Perform(GameObject agent)
	{
		Inventory inv = agent.GetComponent<Inventory>();

		m_Target.GetComponent<Base>().AddOreCollected(inv.GetOre());
		inv.SetOre(0);
		m_DroppedOffOre = true;

		return true;
	}
}
