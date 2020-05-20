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
	/// The idle state. Agent will create a new plan from their currently unfullfilled goals.
	/// </summary>
	private FiniteStateMachine.FSMState m_IdleState;
	/// <summary>
	/// The move state. Agent will move to their target.
	/// </summary>
	private FiniteStateMachine.FSMState m_MoveToState;
	/// <summary>
	/// The perform action state. Agent will do whatever it is they need to do for the current action.
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
		// Get the component that will keep track of our world state and goals (anything that inherits the Worker class).
		m_DataProvider = gameObject.GetComponent<GOAPInterface>();

		// Create the 3 states required for the AI to function.
		// Doing nothing, so create a plan.
		CreateIdleState();
		// Move to target.
		CreateMoveToState();
		// Do whatever is needed for the current action.
		CreatePerformActionState();

		// Begin in the idle state.
		m_StateMachine.AddState(m_IdleState);

		// Get the actions the agent has avaliable to them.
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
			// Check the world state.
			HashSet<KeyValuePair<string, object>> worldState = m_DataProvider.GetWorldState();

			// Check the current goals.
			HashSet<KeyValuePair<string, object>> goal = m_DataProvider.CreateWorldGoal();

			// Create a plan to fulfill the agent's goal world state.
			Queue<GOAPAction> plan = m_Planner.Plan(gameObject, m_AvaliableActions, worldState, goal);

			// If a valid plan was created, begin enacting on that plan.
			if (plan != null)
			{
				m_CurrentActions = plan;
				m_DataProvider.PlanFound(goal, plan);

				fsm.RemoveState();
				fsm.AddState(m_PerformActionState);
			}
			// Else, no valid plan could be created, say plan failed and try again.
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
			// If the current actions requires us to be in range of a target but we couldn't find a target, go to the idle state.
			if (action.RequiresInRange() && action.GetTarget() == null)
			{
				fsm.RemoveState();
				fsm.RemoveState();
				fsm.AddState(m_IdleState);
				return;
			}

			// If the agent has reached the target of their current action, move to the next state.
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
			// If the agent has no plan, go to the idle state and say we are doing nothing.
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

			// If the agent has a plan.
			if (HasActionPlan())
			{
				action = m_CurrentActions.Peek();
				// Check if in range of action.
				bool inRange = action.RequiresInRange() ? action.IsInRange() : true;

				// If we are in range for the action.
				if (inRange)
				{
					// Check if the agent was able to perform the action.
					bool success = action.Perform(gameObject);

					// If not, abort plan and go back to idle.
					if (!success)
					{
						fsm.RemoveState();
						fsm.AddState(m_IdleState);
						m_DataProvider.PlanAborted(action);
					}
				}
				// If not in range, start moving towards target.
				else
				{
					fsm.AddState(m_MoveToState);
				}
			}
		};
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