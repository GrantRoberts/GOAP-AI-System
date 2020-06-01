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
	/// The food that has been collected.
	/// </summary>
	private int m_FoodCollected = 0;

	/// <summary>
	/// Text Mesh Pro text for the score of wood collected.
	/// </summary>
	public TextMeshProUGUI m_WoodCollectedScore = null;

	/// <summary>
	/// Text Mesh Pro text for the score of ore collected.
	/// </summary>
	public TextMeshProUGUI m_OreCollectedScore = null;

	/// <summary>
	/// Text Mesh Pro text for the score of food collected.
	/// </summary>
	public TextMeshProUGUI m_FoodCollectedScore = null;

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

	/// <summary>
	/// Get how much food has been collected.
	/// </summary>
	/// <returns>The amount of food that has been stored in the base.</returns>
	public int GetFoodCollected()
	{
		return m_FoodCollected;
	}

	/// <summary>
	/// Add food to the food stored in the base.
	/// </summary>
	/// <param name="food">The amount of food to be added to the base.</param>
	public void AddFoodCollected(int food)
	{
		m_FoodCollected += food;
		UpdateFoodCollected();
	}

	public void DecreaseFoodCollected(int food)
	{
		m_FoodCollected -= food;
		UpdateFoodCollected();
	}

	/// <summary>
	/// Update the text showing the food that has been stored in the base.
	/// </summary>
	private void UpdateFoodCollected()
	{
		m_FoodCollectedScore.text = m_FoodCollected.ToString();
	}
}
