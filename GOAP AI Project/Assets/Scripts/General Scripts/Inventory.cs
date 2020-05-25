using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	/// <summary>
	/// The amount of wood this agent has collected.
	/// </summary>
	private int m_WoodCollected = 0;

	/// <summary>
	/// The amount of ore this agent has collected.
	/// </summary>
	private int m_OreCollected = 0;

	/// <summary>
	/// The tool the agent has equipped.
	/// </summary>
	private string m_Tool = "";

	public Sprite[] m_ToolIcons = new Sprite[0];

	private Image m_ToolIcon = null;

	private void Awake()
	{
		m_ToolIcon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
	}

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
	/// Increase the ore in the agent's inventory.
	/// </summary>
	/// <param name="add">How much ore to add.</param>
	public void IncreaseOre(int add)
	{
		m_OreCollected += add;
	}

	/// <summary>
	/// Get the amout of ore the agent has.
	/// </summary>
	/// <returns>The amount of ore the agent is carrying.</returns>
	public int GetOre()
	{
		return m_OreCollected;
	}

	/// <summary>
	/// Set how much ore the agent has.
	/// </summary>
	/// <param name="newOre">What to set the agent's ore count to.</param>
	public void SetOre(int newOre)
	{
		m_OreCollected = newOre;
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

		if (m_Tool == "woodAxe")
			m_ToolIcon.sprite = m_ToolIcons[0];
		else if (m_Tool == "orePick")
			m_ToolIcon.sprite = m_ToolIcons[1];

		m_ToolIcon.color = new Color(1,1,1,1);
	}
}