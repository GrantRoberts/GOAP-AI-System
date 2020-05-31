using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatWheat : GOAPAction
{
	/// <summary>
	/// If this action has been performed yet.
	/// </summary>
	private bool m_Eaten = false;

	/// <summary>
	/// The time an agent began eating.
	/// </summary>
	private float m_StartTime = 0.0f;

	/// <summary>
	/// How long it takes to eat.
	/// </summary>
	public float m_EatDuration = 3.0f;

	/// <summary>
	/// On startup.
	/// </summary>
	private void Awake()
	{
		AddPrecondition("needFood", true);

		AddEffect("needFood", false);
	}

	/// <summary>
	/// Reset the variables for eating.
	/// </summary>
	public override void DoReset()
	{
		m_Eaten = false;
		m_StartTime = 0.0f;
	}

	/// <summary>
	/// Check if the agent has eaten.
	/// </summary>
	/// <returns>If the agent has eaten.</returns>
	public override bool IsDone()
	{
		return m_Eaten;
	}

	/// <summary>
	/// Check if this action needs to be in range of the target.
	/// </summary>
	/// <returns>True, the agent must be in range to perform this action.</returns>
	public override bool RequiresInRange()
	{
		return true;
	}

	/// <summary>
	/// Check if a target can be found for this action.
	/// </summary>
	/// <param name="agent">The agent to check for.</param>
	/// <returns>If a target was found.</returns>
	public override bool CheckProceduralPrecondition(GameObject agent)
	{
		m_Target = GameObject.FindGameObjectWithTag("Base");

		return m_Target != null;
	}

	/// <summary>
	/// Eat from a wheat field.
	/// </summary>
	/// <param name="agent">The agent performing this action.</param>
	/// <returns>If the action was performed this frame.</returns>
	public override bool Perform(GameObject agent)
	{
		if (m_StartTime == 0.0f)
			m_StartTime = Time.time;

		if (Time.time - m_StartTime > m_EatDuration)
		{
			// Make sure there is enough food to feed this agent.
			Base b = m_Target.GetComponent<Base>();
			if (b.GetFoodCollected() > 0)
			{
				agent.GetComponent<Worker>().ResetHunger();
				m_Eaten = true;
				b.DecreaseFoodCollected(1);
			}
			else
				return false;
		}
		return true;
	}
}