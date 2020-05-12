using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	/// <summary>
	/// The amount of wood this agent has collected.
	/// </summary>
	private int m_WoodCollected = 0;

	/// <summary>
	/// The tool the agent has equipped.
	/// </summary>
	private string m_Tool;

	/// <summary>
	/// Increase the wood in the agent's inventory.
	/// </summary>
	/// <param name="add">How much wood to add.</param>
	public void IncreaseWood(int add)
	{
		m_WoodCollected += add;
	}

	/// <summary>
	/// Get the amount of wood the agent has.
	/// </summary>
	/// <returns>The amount of wood the agent is carrying.</returns>
	public int GetWood()
	{
		return m_WoodCollected;
	}

	/// <summary>
	/// Set how much wood the agent has.
	/// </summary>
	/// <param name="newWood">What to set the agent's wood count to.</param>
	public void SetWood(int newWood)
	{
		m_WoodCollected = newWood;
	}

	/// <summary>
	/// Get the tool the agent has.
	/// </summary>
	/// <returns>The tool the agent has equipped.</returns>
	public string GetTool()
	{
		return m_Tool;
	}

	/// <summary>
	/// Set the tool of the agent.
	/// </summary>
	/// <param name="tool">The new tool.</param>
	public void SetTool(string tool)
	{
		m_Tool = tool;
	}
}
