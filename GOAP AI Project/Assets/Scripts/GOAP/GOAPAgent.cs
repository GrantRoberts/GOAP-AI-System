using System;
using System.Collections.Generic;
using UnityEngine;

public class GOAPAgent : MonoBehaviour
{
	/// <summary>
	/// The state machine of the agent.
	/// </summary>
	private FiniteStateMachine m_StateMachine = new FiniteStateMachine();

	/// <summary>
	/// The idle state.
	/// </summary>
	private FiniteStateMachine.FSMState m_IdleState;

	/// <summary>
	/// The move to point state.
	/// </summary>
	private FiniteStateMachine.FSMState m_MoveToState;

	/// <summary>
	/// The perform action state.
	/// </summary>
	private FiniteStateMachine.FSMState m_PerformActionState;

	/// <summary>
	/// All avaliable actions.
	/// </summary>
	private HashSet<GOAPAction> m_AvaliableActions = new HashSet<GOAPAction>();

	/// <summary>
	/// Queue of current actions to perform.
	/// </summary>
	private Queue<GOAPAction> m_CurrentActions = new Queue<GOAPAction>();

	/// <summary>
	/// Where data for the agent is coming from.
	/// </summary>
	private GOAPInterface m_DataProvider;

	/// <summary>
	/// The planner for the agent.
	/// </summary>
	private GOAPPlanner m_Planner = new GOAPPlanner();

	/// <summary>
	/// On startup.
	/// </summary>
	private void Awake()
	{
		FindDataProvider();
		CreateIdleState();
		CreateMoveToState();
		CreatePerformActionState();
		m_StateMachine.AddState(m_IdleState);
		LoadActions();
	}

	private void Update()
	{
		m_StateMachine.Update(gameObject);
	}

	/// <summary>
	/// Add an action to the agent.
	/// </summary>
	/// <param name="action">The action to add.</param>
	public void AddAction(GOAPAction action)
	{
		m_AvaliableActions.Add(action);
	}

	/// <summary>
	/// Get an action from the agent.
	/// </summary>
	/// <param name="action">The action to get.</param>
	/// <returns>The retrieved action. Null if action could not be found.</returns>
	public GOAPAction GetAction(Type action)
	{
		foreach(GOAPAction a in m_AvaliableActions)
		{
			if (a.GetType().Equals(action))
				return a;
		}
		return null;
	}

	/// <summary>
	/// Remove an action from the agent.
	/// </summary>
	/// <param name="action">The action to remove.</param>
	public void RemoveAction(GOAPAction action)
	{
		m_AvaliableActions.Remove(action);
	}

	/// <summary>
	/// Check if the agent has a plan it is following.
	/// </summary>
	/// <returns>If the agent has a plan.</returns>
	private bool HasActionPlan()
	{
		return m_CurrentActions.Count > 0;
	}

	/// <summary>
	/// Create the idle state.
	/// </summary>
	private void CreateIdleState()
	{
		m_IdleState = (fsm, gameObject) =>
		{
			HashSet<KeyValuePair<string, object>> worldState = m_DataProvider.GetWorldState();
			HashSet<KeyValuePair<string, object>> goal = m_DataProvider.CreateWorldGoal();

			Queue<GOAPAction> plan = m_Planner.Plan(gameObject, m_AvaliableActions, worldState, goal);
			if (plan != null)
			{
				m_CurrentActions = plan;
				m_DataProvider.PlanFound(goal, plan);

				fsm.RemoveState();
				fsm.AddState(m_PerformActionState);
			}
			else
			{
				m_DataProvider.PlanFailed(goal);
				fsm.RemoveState();
				fsm.AddState(m_IdleState);
			}
		};
	}

	/// <summary>
	/// Create the move state.
	/// </summary>
	private void CreateMoveToState()
	{
		m_MoveToState = (fsm, gameObject) =>
		{
			GOAPAction action = m_CurrentActions.Peek();
			if (action.RequiresInRange() && action.m_Target == null)
			{
				fsm.RemoveState();
				fsm.RemoveState();
				fsm.AddState(m_IdleState);
				return;
			}

			if (m_DataProvider.MoveAgent(action))
			{
				fsm.RemoveState();
			}
		};
	}

	/// <summary>
	/// Create the perform action state.
	/// </summary>
	private void CreatePerformActionState()
	{
		m_PerformActionState = (fsm, gameObject) =>
		{
			if (!HasActionPlan())
			{
				fsm.RemoveState();
				fsm.AddState(m_IdleState);
				m_DataProvider.ActionsFinished();
				return;
			}

			GOAPAction action = m_CurrentActions.Peek();

			if (action.IsDone())
				m_CurrentActions.Dequeue();

			if (HasActionPlan())
			{
				action = m_CurrentActions.Peek();
				bool inRange = action.RequiresInRange() ? action.IsInRange() : true;

				if (inRange)
				{
					bool success = action.Perform(gameObject);

					if (!success)
					{
						fsm.RemoveState();
						fsm.AddState(m_IdleState);
						m_DataProvider.PlanAborted(action);
					}
				}
				else
				{
					fsm.AddState(m_MoveToState);
				}
			}
			else
			{
				fsm.RemoveState();
				fsm.AddState(m_IdleState);
				m_DataProvider.ActionsFinished();
			}
		};
	}

	/// <summary>
	/// Find the component of the agent that is providing data.
	/// </summary>
	private void FindDataProvider()
	{
		m_DataProvider = gameObject.GetComponent<GOAPInterface>();
	}

	/// <summary>
	/// Load the actions from the game object components.
	/// </summary>
	private void LoadActions()
	{
		GOAPAction[] actions = gameObject.GetComponents<GOAPAction>();
		foreach(GOAPAction a in actions)
		{
			m_AvaliableActions.Add(a);
		}
	}
}