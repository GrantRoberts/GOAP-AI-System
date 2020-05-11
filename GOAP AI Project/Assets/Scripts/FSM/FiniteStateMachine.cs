using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
	/// <summary>
	/// The states of the state machine.
	/// </summary>
	private Stack<FSMState> m_StateStack = new Stack<FSMState>();

	public delegate void FSMState(FiniteStateMachine fsm, GameObject gameObject);

	/// <summary>
	/// Update the FSM.
	/// </summary>
	/// <param name="gameObject">The state in the state machine to update.</param>
	public void Update(GameObject gameObject)
	{
		if (m_StateStack.Peek() != null)
			m_StateStack.Peek().Invoke(this, gameObject);
	}

	/// <summary>
	/// Add a state to the state machine.
	/// </summary>
	/// <param name="state">The state to add.</param>
	public void AddState(FSMState state)
	{
		m_StateStack.Push(state);
	}

	/// <summary>
	/// Remove a state from the FSM.
	/// </summary>
	public void RemoveState()
	{
		m_StateStack.Pop();
	}
}