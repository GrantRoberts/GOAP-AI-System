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
		// Agent needs to have a wood (chopping) axe.
		AddPrecondition("hasWoodAxe", true);
		// Agent can't currently have wood already
		//AddPrecondition("hasWood", false);

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
		if (m_Target != null)
			m_Target.GetComponent<Tree>().SetCurrentLogger(null);
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
		float closestDistance = (closest.transform.position - agent.transform.position).magnitude;

		Tree targetTree = null;

		// Find the closest tree.
		foreach (GameObject t in trees)
		{
			Tree tree = t.GetComponent<Tree>();
			if (tree.GetFullyGrown() && tree.GetCurrentLogger() == null)
			{
				float dist = (t.transform.position - agent.transform.position).magnitude;
				if (dist < closestDistance)
				{
					closest = t;
					closestDistance = dist;
					targetTree = tree;
				}
			}
		}

		if (closest == null)
			return false;

		// Target the closest tree.
		m_Target = closest;
		if (targetTree != null)
			targetTree.SetCurrentLogger(gameObject);

		return closest != null;
	}

	/// <summary>
	/// Perform the chop tree action.
	/// </summary>
	/// <param name="agent">The agent performing this action.</param>
	/// <returns>If the action is being performed.</returns>
	public override bool Perform(GameObject agent)
	{
		// Make sure the target is still active before performing an action on it.
		if (m_Target.GetComponent<Tree>().GetFullyGrown())
		{
			if (m_StartTime == 0)
				m_StartTime = Time.time;

			if (Time.time - m_StartTime > m_WorkDuration)
			{
				m_Inventory.IncreaseWood(1);
				m_Chopped = true;
				m_Target.GetComponent<Tree>().DecreaseWoodAmount(1);

				agent.GetComponent<Worker>().DecreaseHunger(m_WorkHunger);
			}

			m_Inventory.SetProgress((Time.time - m_StartTime) / m_WorkDuration);

			return true;
		}
		else
			return false;
	}
}