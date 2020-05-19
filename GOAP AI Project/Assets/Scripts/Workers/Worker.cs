using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Inventory))]
public abstract class Worker : MonoBehaviour, GOAPInterface
{
	/// <summary>
	/// The worker's inventory.
	/// </summary>
	protected Inventory m_Inventory = null;

	/// <summary>
	/// The range the worker can interact with things.
	/// </summary>
	public float m_InteractionRange = 3.0f;

	/// <summary>
	/// The nav mesh agent component.
	/// </summary>
	private NavMeshAgent m_NavAgent = null;

	/// <summary>
	/// The position of the target of the goal.
	/// </summary>
	private Vector3 m_TargetPosition = Vector3.zero;

	private void Awake()
	{
		m_Inventory = GetComponent<Inventory>();
		m_NavAgent = GetComponent<NavMeshAgent>();
		m_NavAgent.stoppingDistance = m_InteractionRange;
	}

	private void Update()
	{
		//Debug.Log(m_Inventory.GetWood());
	}

	/// <summary>
	/// Get the current world state.
	/// </summary>
	/// <returns>The world state.</returns>
	public abstract HashSet<KeyValuePair<string, object>> GetWorldState();

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
		//Debug.Log("Goal Failed!");
	}

	/// <summary>
	/// The worker found a plan.
	/// </summary>
	/// <param name="goal">The goal the worker created.</param>
	/// <param name="actions">The actions for achieveing that goal</param>
	public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> actions)
	{
		//Debug.Log("Goal Found!");
	}

	/// <summary>
	/// The worker is finished with their actions.
	/// </summary>
	public void ActionsFinished()
	{
		//Debug.Log("Actions Finished!");
	}

	/// <summary>
	/// Plan was aborted for some reason.
	/// </summary>
	/// <param name="aborter">The action that aborted the plan.</param>
	public void PlanAborted(GOAPAction aborter)
	{
		//Debug.Log("Plan Aborted by " + aborter.name);
	}

	/// <summary>
	/// Move the agent to their target.
	/// </summary>
	/// <param name="nextAction">The action the worker is moving towards to achieve their goal</param>
	/// <returns>If the agent has reached their destination.</returns>
	public bool MoveAgent(GOAPAction nextAction)
	{
		// If the worker is within interation range of their target, they have reached their destination.
		if ((transform.position - nextAction.GetTarget().transform.position).magnitude < m_InteractionRange)
		{
			//Debug.Log("I have reached my destination.");
			nextAction.SetInRange(true);
			return true;
		}
		// Else, they're still going.
		else
		{
			// Set the nav mesh agent's destination to the destination of the goal.
			m_TargetPosition = nextAction.GetTarget().transform.position;
			m_NavAgent.destination = m_TargetPosition;

			return false;
		}
	}
}
