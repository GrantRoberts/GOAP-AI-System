using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWheat : GOAPAction
{
	/// <summary>
	/// If wheat has been collected.
	/// </summary>
	private bool m_Farmed = false;

	/// <summary>
	/// When the action was started.
	/// </summary>
	private float m_StartTime = 0.0f;

	/// <summary>
	/// How long it takes to complete this action.
	/// </summary>
	public float m_WorkDuration = 5.0f;

	/// <summary>
	/// The hunger this action costs to perform.
	/// </summary>
	public float m_WorkHunger = 2.0f;

	/// <summary>
	/// The inventory of this agent.
	/// </summary>
	private Inventory m_Inventory = null;

	private void Awake()
	{
		m_Inventory = GetComponent<Inventory>();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	public CollectWheat()
	{
		AddPrecondition("hasFarmingHoe", true);

		AddEffect("hasWheat", true);
	}

	/// <summary>
	/// Reset the variables for this action.
	/// </summary>
	public override void DoReset()
	{
		m_Farmed = false;
		m_StartTime = 0.0f;
	}

	/// <summary>
	/// If this action has been performed.
	/// </summary>
	/// <returns>If the agent has mined ore.</returns>
	public override bool IsDone()
	{
		return m_Farmed;
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
	/// Check for the closest wheat field to collect wheat from.
	/// </summary>
	/// <param name="agent">The agent we are checking for.</param>
	/// <returns>If a wheat field was found.</returns>
	public override bool CheckProceduralPrecondition(GameObject agent)
	{
		// Get all the ore veins in the scene.
		GameObject[] fields = GameObject.FindGameObjectsWithTag("Wheat Field");
		GameObject closest = fields[0];
		float closestDistance = (closest.transform.position - agent.transform.position).magnitude;

		// Find the closest ore vein.
		foreach (GameObject f in fields)
		{
			WheatField field = f.GetComponent<WheatField>();
			if (field.GetCurrentFarmer() == null)
			{
				float dist = (f.transform.position - agent.transform.position).magnitude;
				// Target the closest ore vein that no one else is targeting.
				if (dist < closestDistance)
				{
					closest = f;
					closestDistance = dist;
				}
			}
		}

		// Return false if a wheat field couldn't be found.
		if (closest == null)
			return false;

		// Target the closest wheat field.
		m_Target = closest;
		// Tell the wheat field that this agent is targeting it.
		m_Target.GetComponent<WheatField>().SetCurrentFarmer(agent.name);

		return closest != null;
	}

	/// <summary>
	/// Perform the collect ore action.
	/// </summary>
	/// <param name="agent">The agent performing this action.</param>
	/// <returns>If the action was performed this frame.</returns>
	public override bool Perform(GameObject agent)
	{
		// Make sure the wheat field is active.
		if (m_Target.activeSelf != false)
		{
			if (m_StartTime == 0.0f)
				m_StartTime = Time.time;

			// Work complete.
			if (Time.time - m_StartTime > m_WorkDuration)
			{
				// Update everything that work has been done.
				m_Inventory.IncreaseFood(1);
				m_Farmed = true;
				WheatField wheatField = m_Target.GetComponent<WheatField>();
				wheatField.SetCurrentFarmer(null);

				agent.GetComponent<Worker>().DecreaseHunger(m_WorkHunger);
			}

			// Set progress for progress bar.
			m_Inventory.SetProgress((Time.time - m_StartTime) / m_WorkDuration);

			// The action was performed this frame.
			return true;
		}
		// The action was not performed this frame.
		else
			return false;
	}
}