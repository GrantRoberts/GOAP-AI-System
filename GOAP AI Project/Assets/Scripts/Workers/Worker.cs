using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public abstract class Worker : MonoBehaviour, GOAPInterface
{
	/// <summary>
	/// The worker's inventory.
	/// </summary>
	private Inventory m_Inventory = null;

	/// <summary>
	/// The worker's move speed.
	/// </summary>
	public float m_MoveSpeed = 3.0f;

	/// <summary>
	/// The range the worker can interact with things.
	/// </summary>
	public float m_InteractionRange = 1.0f;

	private void Awake()
	{
		m_Inventory = GetComponent<Inventory>();
	}

	/// <summary>
	/// Get the current world state.
	/// </summary>
	/// <returns>The world state.</returns>
	public HashSet<KeyValuePair<string, object>> GetWorldState()
	{
		HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

		worldData.Add(new KeyValuePair<string, object>("hasWood", (m_Inventory.GetWood() > 0)));

		return worldData;
	}

	/// <summary>
	/// Create a goal in the world.
	/// </summary>
	/// <returns>The goal created.</returns>
	public abstract HashSet<KeyValuePair<string, object>> CreateWorldGoal();

	/// <summary>
	/// The worker's plan failed.
	/// </summary>
	/// <param name="failedGoal">The goal that failed.</param>
	public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
	{
		Debug.Log("Goal Failed!");
	}

	/// <summary>
	/// The worker found a plan.
	/// </summary>
	/// <param name="goal">The goal the worker created.</param>
	/// <param name="actions">The actions for achieveing that goal</param>
	public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> actions)
	{
		Debug.Log("Goal Found!");
	}

	/// <summary>
	/// The worker is finished with their actions.
	/// </summary>
	public void ActionsFinished()
	{
		Debug.Log("Actions Finished!");
	}

	/// <summary>
	/// Plan was aborted for some reason.
	/// </summary>
	/// <param name="aborter">The action that aborted the plan.</param>
	public void PlanAborted(GOAPAction aborter)
	{
		Debug.Log("Plan Aborted by " + aborter.name);
	}

	/// <summary>
	/// Move the agent to their target.
	/// </summary>
	/// <param name="nextAction">The action the worker is moving towards to achieve their goal</param>
	/// <returns>If the agent has reached their destination.</returns>
	public bool MoveAgent(GOAPAction nextAction)
	{
		// If the worker is within interation range of their target, they have reached their destination.
		if ((transform.position - nextAction.m_Target.transform.position).magnitude < m_InteractionRange)
		{
			nextAction.SetInRange(true);
			return true;
		}
		// Else, they're still going.
		else
			return false;
	}
}
