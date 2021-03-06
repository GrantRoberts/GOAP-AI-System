﻿using System.Collections;
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
	public CollectWood()
	{
		// Agent needs to have a wood axe.
		AddPrecondition("hasWoodAxe", true);

		// This action causes the agent to have wood in it's inventory.
		AddEffect("hasWoodHeld", true);
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
		GameObject closest = null;
		float closestDistance = float.MaxValue;

		// Find the closest tree.
		foreach (GameObject t in trees)
		{
			// Check the tree is fully grown and no one is targetting this tree.
			Tree tree = t.GetComponent<Tree>();
			if (tree.GetFullyGrown() && tree.GetCurrentLogger() == null)
			{
				float dist = (t.transform.position - agent.transform.position).magnitude;
				// Target the closest tree that no one else is targeting.
				if (dist < closestDistance)
				{
					closest = t;
					closestDistance = dist;
				}
			}
		}

		if (closest != null)
		{
			// Target the closest tree.
			m_Target = closest;
			// Tell the tree that this agent is targeting it.
			m_Target.GetComponent<Tree>().SetCurrentLogger(agent.name);

			return m_Target != null;
		}
		else
			return false;
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

		// Work complete.
		if (Time.time - m_StartTime > m_WorkDuration)
		{
			// Update everything that work has been done.
			m_Inventory.IncreaseWood(1);
			m_Chopped = true;
			Tree targetTree = m_Target.GetComponent<Tree>();
			targetTree.DecreaseWoodAmount(1);
			targetTree.SetCurrentLogger(null);

			agent.GetComponent<Worker>().DecreaseHunger(m_WorkHunger);
		}

		// Set progress for the progress bar.
		m_Inventory.SetProgress((Time.time - m_StartTime) / m_WorkDuration);

		// Return true, the action was performed this frame.
		return true;
	}
}