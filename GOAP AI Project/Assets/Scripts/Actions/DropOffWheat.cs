using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffWheat : GOAPAction
{
	/// <summary>
	/// Has wood been dropped off to the base.
	/// </summary>
	private bool m_DroppedOffWheat = false;

	/// <summary>
	/// Constructor.
	/// </summary>
	public DropOffWheat()
	{
		// Agent must have wood.
		AddPrecondition("hasWheat", true);

		// Agent will no longer have wood.
		AddEffect("hasWheat", false);

		// Wood will have been collected.
		AddEffect("collectWheat", true);
	}

	/// <summary>
	/// Reset the action.
	/// </summary>
	public override void DoReset()
	{
		m_DroppedOffWheat = false;
	}

	/// <summary>
	/// Has wheat been dropped off.
	/// </summary>
	/// <returns>If wheat has been dropped off.</returns>
	public override bool IsDone()
	{
		return m_DroppedOffWheat;
	}

	/// <summary>
	/// Check if agent must be in range to drop wheat off.
	/// </summary>
	/// <returns>True, the agent must be in range to drop wheat off.</returns>
	public override bool RequiresInRange()
	{
		return true;
	}

	/// <summary>
	/// Find the base to drop wheat off to.
	/// </summary>
	/// <param name="agent">The agent dropping off wheat.</param>
	/// <returns>If a base was found.</returns>
	public override bool CheckProceduralPrecondition(GameObject agent)
	{
		m_Target = GameObject.FindGameObjectWithTag("Base");

		return m_Target != null;
	}

	/// <summary>
	/// Drop wheat off to the base.
	/// </summary>
	/// <param name="agent">The agent dropping wheat off.</param>
	/// <returns>If wheat was dropped off successfully.</returns>
	public override bool Perform(GameObject agent)
	{
		Inventory inv = agent.GetComponent<Inventory>();
		m_Target.GetComponent<Base>().AddFoodCollected(inv.GetFood());
		inv.SetFood(0);
		inv.SetProgress(0);
		m_DroppedOffWheat = true;

		return true;
	}
}