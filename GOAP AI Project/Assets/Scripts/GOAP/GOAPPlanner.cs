using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner
{
	/// <summary>
	/// Create a plan.
	/// </summary>
	/// <param name="agent">The agent to create a plan for.</param>
	/// <param name="avaliableActions">Actions avaliable to the agent.</param>
	/// <param name="worldState">The current world state.</param>
	/// <param name="goal">The goal the planner will aim to achieve.</param>
	/// <returns>The actions required to fulfill the plan. Null if it couldn't create a valid plan.</returns>
	public Queue<GOAPAction> Plan(GameObject agent,
		HashSet<GOAPAction> avaliableActions,
		HashSet<KeyValuePair<string, object>> worldState,
		HashSet<KeyValuePair<string, object>> goal)
	{
		// Reset the actions.
		foreach (GOAPAction action in avaliableActions)
		{
			action.GOAPActionReset();
		}

		// Actions we can perform.
		HashSet<GOAPAction> usableActions = new HashSet<GOAPAction>();

		// Go through each action the agent has avaliable and find the ones they can perform now.
		foreach (GOAPAction action in avaliableActions)
		{
			if (action.CheckProceduralPrecondition(agent))
				usableActions.Add(action);
		}

		// Actions to the goal.
		List<Node> leaves = new List<Node>();

		// Create a starting node and begin building the graph of actions.
		Node start = new Node(null, 0, worldState, null);
		bool success = BuildGraph(start, leaves, usableActions, goal);

		if (!success)
		{
			Debug.Log(agent.name + "'s planning failed!");
			return null;
		}

		// Find the cheapest node in the list.
		Node cheapest = leaves[0];
		foreach (Node leaf in leaves)
		{
			if (leaf.m_Cost < cheapest.m_Cost)
				cheapest = leaf;
		}

		// Build a linked list of actions for achieving the goal via the cheapest path.
		List<GOAPAction> result = new List<GOAPAction>();
		Node n = cheapest;
		while (n != null)
		{
			if (n.m_Action != null)
				result.Insert(0, n.m_Action);
			n = n.m_Parent;
		}

		// Create the queue of actions.
		Queue<GOAPAction> queue = new Queue<GOAPAction>();
		foreach(GOAPAction action in result)
		{
			queue.Enqueue(action);
		}

		return queue;
	}

	/// <summary>
	/// Build the graph of actions for the GOAP AI to traverse.
	/// </summary>
	/// <param name="parent">The starting node for the graph.</param>
	/// <param name="leaves">The list of actions.</param>
	/// <param name="usableActions">The actions that the agent can use.</param>
	/// <param name="goal">The goal the agent is trying to complete</param>
	/// <returns>The built graph.</returns>
	private bool BuildGraph(Node parent, List<Node> leaves, HashSet<GOAPAction> usableActions, HashSet<KeyValuePair<string, object>> goal)
	{
		bool foundOne = false;

		// Go through each action that is avaliable.
		foreach(GOAPAction action in usableActions)
		{
			// If the parent state has the conditions of this action's preconditions, it can be used.
			if (InState(action.GetPreconditions(), parent.m_State))
			{
				// Apply the actions effects to the state.
				HashSet<KeyValuePair<string, object>> currentState = PopulateState(parent.m_State, action.GetEffects());
				Node node = new Node(parent, parent.m_Cost + action.m_Cost, currentState, action);

				// If a solution was found, add it to the actions towards the goal.
				if (InState(goal, currentState))
				{
					leaves.Add(node);
					foundOne = true;
				}
				// If no solution was found, test the remaining actions.
				else
				{
					HashSet<GOAPAction> subset = ActionSubset(usableActions, action);
					bool found = BuildGraph(node, leaves, subset, goal);
					if (found)
						foundOne = true;
				}
			}
		}
		return foundOne;
	}

	/// <summary>
	/// Create a new set of actions without a specified action.
	/// </summary>
	/// <param name="actions">The actions.</param>
	/// <param name="remove">The actions to remove.</param>
	/// <returns></returns>
	private HashSet<GOAPAction> ActionSubset(HashSet<GOAPAction> actions, GOAPAction remove)
	{
		HashSet<GOAPAction> subset = new HashSet<GOAPAction>();
		foreach(GOAPAction action in actions)
		{
			if (!action.Equals(remove))
				subset.Add(action);
		}
		return subset;
	}

	/// <summary>
	/// Test if all actions are applicable to the current world state.
	/// </summary>
	/// <param name="test">The actions to test.</param>
	/// <param name="state">The world state.</param>
	/// <returns></returns>
	private bool InState(HashSet<KeyValuePair<string, object>> test, HashSet<KeyValuePair<string, object>> state)
	{
		bool allMatch = true;
		foreach(KeyValuePair<string,object> t in test)
		{
			bool match = false;
			foreach(KeyValuePair<string, object> s in state)
			{
				if (s.Equals(t))
				{
					match = true;
					break;
				}
			}
			if (!match)
				allMatch = false;
		}
		return allMatch;
	}

	/// <summary>
	/// Apply changes to the state to the current world state.
	/// </summary>
	/// <param name="currentState">The current world state.</param>
	/// <param name="stateChange">The world state that is being applied.</param>
	/// <returns>The new world state.</returns>
	private HashSet<KeyValuePair<string, object>> PopulateState(HashSet<KeyValuePair<string, object>> currentState, HashSet<KeyValuePair<string, object>> stateChange)
	{
		HashSet<KeyValuePair<string, object>> state = new HashSet<KeyValuePair<string, object>>();
		// Copy the changed state to the new state.
		foreach(KeyValuePair<string, object> s in currentState)
		{
			state.Add(new KeyValuePair<string, object>(s.Key, s.Value));
		}
		
		// Check if the changed state has the same values as the current state.
		foreach(KeyValuePair<string, object> change in stateChange)
		{
			bool exists = false;

			foreach(KeyValuePair<string, object> s in state)
			{
				if (s.Equals(change))
				{
					exists = true;
					break;
				}
			}

			// If a key exists in the new state, update it's value to the new value.
			if (exists)
			{
				state.RemoveWhere((KeyValuePair<string, object> kvp) => { return kvp.Key.Equals(change.Key); });
				KeyValuePair<string, object> updated = new KeyValuePair<string, object>(change.Key, change.Value);
				state.Add(updated);
			}
			// Else create the state.
			else
			{
				state.Add(new KeyValuePair<string, object>(change.Key, change.Value));
			}
		}
		return state;
	}

	/// <summary>
	/// Node for the graph of GOAP actions.
	/// </summary>
	private class Node
	{
		/// <summary>
		/// Parent of the node.
		/// </summary>
		public Node m_Parent;
		/// <summary>
		/// Cost of changing to this node.
		/// </summary>
		public float m_Cost;
		/// <summary>
		/// The state of this node.
		/// </summary>
		public HashSet<KeyValuePair<string, object>> m_State;
		/// <summary>
		/// The action in this node.
		/// </summary>
		public GOAPAction m_Action;

		/// <summary>
		/// Consturctor
		/// </summary>
		/// <param name="parent">Parent of the node.</param>
		/// <param name="cost">Cost of the node.</param>
		/// <param name="state">The state in the node.</param>
		/// <param name="action">The action in the node.</param>
		public Node(Node parent, float cost, HashSet<KeyValuePair<string, object>> state, GOAPAction action)
		{
			m_Parent = parent;
			m_Cost = cost;
			m_State = state;
			m_Action = action;
		}
	}
}