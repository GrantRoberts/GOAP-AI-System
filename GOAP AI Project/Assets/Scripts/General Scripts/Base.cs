using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
	/// <summary>
	/// The wood that has been collected.
	/// </summary>
    private int m_WoodCollected = 0;

	/// <summary>
	/// The ore that has been collected.
	/// </summary>
	private int m_OreCollected = 0;

	/// <summary>
	/// Text Mesh Pro text for the score of wood collected.
	/// </summary>
	public TextMeshProUGUI m_WoodCollectedScore = null;

	public TextMeshProUGUI m_OreCollectedScore = null;

	/// <summary>
	/// Get how much wood has been collected.
	/// </summary>
	/// <returns>The amount of wood that has been stored in the base.</returns>
	public int GetWoodCollected()
	{
		return m_WoodCollected;
	}

	/// <summary>
	/// Add wood the the wood stored in the base.
	/// </summary>
	/// <param name="wood">The amount of wood to be added to the base.</param>
	public void AddWoodCollected(int wood)
	{
		m_WoodCollected += wood;
		UpdateWoodCollected();
	}

	/// <summary>
	/// Update the text showing the wood that has been stored in the base.
	/// </summary>
	private void UpdateWoodCollected()
	{
		m_WoodCollectedScore.text = m_WoodCollected.ToString();
	}

	/// <summary>
	/// Get how much ore has been collected.
	/// </summary>
	/// <returns>The amount of ore that has been stored in the base.</returns>
	public int GetOreCollected()
	{
		return m_OreCollected;
	}

	/// <summary>
	/// Add ore to the ore stored in the base.
	/// </summary>
	/// <param name="ore">The amount of ore to be added to the base.</param>
	public void AddOreCollected(int ore)
	{
		m_OreCollected += ore;
		UpdateOreCollected();
	}

	/// <summary>
	/// Update the text showing the ore that has been stored in the base.
	/// </summary>
	private void UpdateOreCollected()
	{
		m_OreCollectedScore.text = m_OreCollected.ToString();
	}
}
