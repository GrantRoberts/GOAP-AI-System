﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffWood : GOAPAction
{
	/// <summary>
	/// Has wood been dropped off to the base.
	/// </summary>
	private bool m_DroppedOffWood = false;

	/// <summary>
	/// The base to drop wood off to.
	/// </summary>
	private GameObject m_Base = null;

	/// <summary>
	/// Constructor.
	/// </summary>
	public DropOffWood()
	{
		// Agent must have wood.
		AddPrecondition("hasWood", true);

		// Agent will no longer have wood.
		AddEffect("hasWood", false);

		// Wood will have been collected.
		AddEffect("collectWood", true);
	}

	/// <summary>
	/// Reset the action.
	/// </summary>
	public override void DoReset()
	{
		m_DroppedOffWood = false;
		m_Base = null;
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
		GameObject supply = GameObject.FindGameObjectWithTag("Base");

		m_Target = supply;
		m_Base = supply;

		return true;
	}

	/// <summary>
	/// Drop wood off to the base.
	/// </summary>
	/// <param name="agent">The agent dropping wood off.</param>
	/// <returns>If wood was dropped off successfully.</returns>
	public override bool Perform(GameObject agent)
	{
		Inventory inv = agent.GetComponent<Inventory>();

		m_Base.GetComponent<Base>().AddWoodCollected(inv.GetWood());
		inv.SetWood(0);
		m_DroppedOffWood = true;

		return true;
	}
}