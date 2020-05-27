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

	/// <summary>
	/// The icons for the tools.
	/// 0 - axe.
	/// 1 - pick.
	/// </summary>
	public Sprite[] m_ToolIcons = new Sprite[0];

	/// <summary>
	/// The icon for the tool sprite.
	/// </summary>
	private Image m_ToolIcon = null;

	/// <summary>
	/// The progress bar.
	/// </summary>
	private Image m_ProgressBar = null;

	private void Awake()
	{
		m_ToolIcon = transform.GetChild(0).GetChild(0).GetComponent<Image>();

		m_ProgressBar = transform.GetChild(0).GetChild(1).GetComponent<Image>();
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

		switch(m_Tool)
		{
			case"woodAxe":
				m_ToolIcon.sprite = m_ToolIcons[0];
				break;
			case "orePick":
				m_ToolIcon.sprite = m_ToolIcons[1];
				break;
			case "farmingHoe":
				m_ToolIcon.sprite = m_ToolIcons[2];
				break;
		}
	}

	/// <summary>
	/// Set the progress of the task for the progress bar.
	/// </summary>
	/// <param name="progress">Normalized float of the time taken to perform the task.</param>
	public void SetProgress(float progress)
	{
		m_ProgressBar.fillAmount = progress;
	}

	/// <summary>
	/// Set the sprite of the progress bar of the agent.
	/// </summary>
	/// <param name="progBar">The sprite to set the progress bar sprite to.</param>
	public void SetProgressBarSprite(Sprite progBar)
	{
		m_ProgressBar.sprite = progBar;
	}
}