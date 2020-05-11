using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPAction : MonoBehaviour
{
	/// <summary>
	/// What world state does this action require.
	/// </summary>
	private HashSet<KeyValuePair<string, object>> m_Preconditions;

	/// <summary>
	/// What world state will this action cause the world to be in.
	/// </summary>
	private HashSet<KeyValuePair<string, object>> m_Effects;

	/// <summary>
	/// Is the agent in range for this action.
	/// </summary>
	private bool m_InRange = false;

	/// <summary>
	/// The cost of performing this action.
	/// </summary>
	public float m_Cost = 1.0f;

	/// <summary>
	/// The target of this action.
	/// </summary>
	public GameObject m_Target = null;

	/// <summary>
	/// Constructor.
	/// </summary>
	public GOAPAction()
	{
		m_Preconditions = new HashSet<KeyValuePair<string, object>>();
		m_Effects = new HashSet<KeyValuePair<string, object>>();
	}

	/// <summary>
	/// Reset the variables of the action.
	/// </summary>
	public void GOAPActionReset()
	{
		m_InRange = false;
		m_Target = null;
	}

	/// <summary>
	/// Gets if the action is complete.
	/// </summary>
	/// <returns>If the agent is done with this action.</returns>
	public abstract bool IsDone();

	/// <summary>
	/// Check the preconditions of the action.
	/// </summary>
	/// <param name="agent">The agent that we are checking can perform this action.</param>
	/// <returns>If the agent can perform the action.</returns>
	public abstract bool CheckProceduralPrecondition(GameObject agent);

	/// <summary>
	/// Perform this action.
	/// </summary>
	/// <param name="agent">The GOAP agent doing this action.</param>
	/// <returns>If the action was performed successfully.</returns>
	public abstract bool Perform(GameObject agent);

	/// <summary>
	/// Check if the agent needs to be in range to perform this action.
	/// </summary>
	/// <returns>If the agent needs to be in range of the target.</returns>
	public abstract bool RequiresInRange();

	/// <summary>
	/// Is the agent in range.
	/// </summary>
	/// <returns>If the agent is in range.</returns>
	public bool IsInRange()
	{
		return m_InRange;
	}

	/// <summary>
	/// Set if the agent is in range of the target.
	/// </summary>
	/// <param name="inRange">If the agent is in range of the target.</param>
	public void SetInRange(bool inRange)
	{
		m_InRange = inRange;
	}

	/// <summary>
	/// Add a precondition to the action.
	/// </summary>
	/// <param name="conditionName">The name the precondition will be reffered to.</param>
	/// <param name="conditionValue">The value the precondition has to equal for the action.</param>
	public void AddPrecondition(string name, object value)
	{
		m_Preconditions.Add(new KeyValuePair<string, object>(name, value));
	}

	/// <summary>
	/// Remove a precondition.
	/// </summary>
	/// <param name="conditionName">The name of the precondiiton to remove.</param>
	public void RemoveProcondition(string name)
	{
		KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
		foreach(KeyValuePair<string, object> con in m_Preconditions)
		{
			if (con.Key.Equals(name))
				remove = con;
		}
		if (!default(KeyValuePair<string, object>).Equals(remove))
			m_Preconditions.Remove(remove);
	}

	/// <summary>
	/// Add an effect to the action.
	/// </summary>
	/// <param name="name">The name the effect will be reffered to.</param>
	/// <param name="value">The value the effect will become.</param>
	public void AddEffect(string name, object value)
	{
		m_Effects.Add(new KeyValuePair<string, object>(name, value));
	}

	/// <summary>
	/// Remove an effect of the action.
	/// </summary>
	/// <param name="name">The name of the effect to remove.</param>
	public void RemoveEffect(string name)
	{
		KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
		foreach (KeyValuePair<string, object> con in m_Effects)
		{
			if (con.Key.Equals(name))
				remove = con;
		}
		if (!default(KeyValuePair<string, object>).Equals(remove))
			m_Effects.Remove(remove);
	}

	/// <summary>
	/// Get the preconditions of this action.
	/// </summary>
	/// <returns>The preconditions.</returns>
	public HashSet<KeyValuePair<string, object>> GetPreconditions()
	{
		return m_Preconditions;
	}

	/// <summary>
	/// Get the effects of this action.
	/// </summary>
	/// <returns>The effects.</returns>
	public HashSet<KeyValuePair<string, object>> GetEffects()
	{
		return m_Effects;
	}
}