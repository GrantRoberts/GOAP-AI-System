using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffWood : GOAPAction
{
	/// <summary>
	/// Has wood been dropped off to the base.
	/// </summary>
	private bool m_DroppedOffWood = false;

	/// <summary>
	/// Constructor.
	/// </summary>
	public DropOffWood()
	{
		// Agent must have wood.
		AddPrecondition("hasWoodHeld", true);

		// Agent will no longer have wood.
		AddEffect("hasWoodHeld", false);

		// Wood will have been collected.
		AddEffect("collectedWood", true);
	}

	/// <summary>
	/// Reset the action.
	/// </summary>
	public override void DoReset()
	{
		m_DroppedOffWood = false;
	}

	/// <summary>
	/// Has wood been dropped off.
	/// </summary>
	/// <returns>If wood has been dropped off.</returns>
	public override bool IsDone()
	{
		return m_DroppedOffWood;
	}

	/// <summary>
	/// Check if agent must be in range to drop wood off.
	/// </summary>
	/// <returns>True, the agent must be in range to drop wood off.</returns>
	public override bool RequiresInRange()
	{
		return true;
	}

	/// <summary>
	/// Find the base to drop wood off to.
	/// </summary>
	/// <param name="agent">The agent dropping off wood.</param>
	/// <returns>If a base was found.</returns>
	public override bool CheckProceduralPrecondition(GameObject agent)
	{
		m_Target = GameObject.FindGameObjectWithTag("Base");

		return m_Target != null;
	}

	/// <summary>
	/// Drop wood off to the base.
	/// </summary>
	/// <param name="agent">The agent dropping wood off.</param>
	/// <returns>If wood was dropped off successfully.</returns>
	public override bool Perform(GameObject agent)
	{
		Inventory inv = agent.GetComponent<Inventory>();

		m_Target.GetComponent<Base>().AddWoodCollected(inv.GetWood());
		inv.SetWood(0);
		inv.SetProgress(0);
		m_DroppedOffWood = true;

		return true;
	}
}