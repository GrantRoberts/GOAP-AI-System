using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVein : MonoBehaviour
{
	/// <summary>
	/// The amount of ore this ore vein has stored.
	/// </summary>
	public int m_OreAmount = 100;

	/// <summary>
	/// The agent currently targeting this ore vein.
	/// </summary>
	private string m_CurrentMiner = null;

	/// <summary>
	/// Get the amount of ore avaliable from this vein.
	/// </summary>
	/// <returns>Int of the ore avaliable.</returns>
	public int GetOreAmount()
	{
		return m_OreAmount;
	}

	/// <summary>
	/// Decrease the amount of ore this ore vein has stored.
	/// </summary>
	/// <param name="decrease">Int of how much to decrease the amount of ore by.</param>
	public void DecreaseOreAmount(int decrease)
	{
		m_OreAmount -= decrease;

		if (m_OreAmount <= 0)
		{
			gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// Set the agent currently targeting this ore vein.
	/// </summary>
	/// <param name="miner">GameObject of the agent targeting this tree.</param>
	public void SetCurrentMiner(string miner)
	{
		m_CurrentMiner = miner;
	}

	/// <summary>
	/// Get the current agent targeting this tree.
	/// </summary>
	/// <returns>GameObject of the agent targeting this tree, null if none.</returns>
	public string GetCurrentMiner()
	{
		return m_CurrentMiner;
	}
}