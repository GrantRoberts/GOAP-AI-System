using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatWheat : GOAPAction
{
	private bool m_Eaten = false;

	private float m_StartTime = 0.0f;

	public float m_EatDuration = 3.0f;

	private void Awake()
	{
		AddPrecondition("hungry", true);

		AddEffect("hungry", false);
	}

	public override void DoReset()
	{
		m_Eaten = false;
		m_StartTime = 0.0f;
	}

	public override bool IsDone()
	{
		return m_Eaten;
	}

	public override bool RequiresInRange()
	{
		return true;
	}

	public override bool CheckProceduralPrecondition(GameObject agent)
	{
		GameObject[] fields = GameObject.FindGameObjectsWithTag("Wheat Field");
		GameObject closest = fields[0];
		float closestDistance = (closest.transform.position - agent.transform.position).magnitude;

		foreach(GameObject f in fields)
		{
			float dist = (f.transform.position - agent.transform.position).magnitude;
			if (dist < closestDistance)
			{
				closest = f;
				closestDistance = dist;
			}
		}

		if (closest == null)
			return false;

		m_Target = closest;

		return closest != null;
	}

	public override bool Perform(GameObject agent)
	{
		if (m_StartTime == 0.0f)
			m_StartTime = Time.time;

		if (Time.time - m_StartTime > m_EatDuration)
		{
			agent.GetComponent<Worker>().ResetHunger();
			m_Eaten = true;
		}
		return true;
	}
}
