using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private int m_WoodCollected = 0;

	private int m_OreCollected = 0;

	public int GetWoodCollected()
	{
		return m_WoodCollected;
	}

	public void AddWoodCollected(int wood)
	{
		m_WoodCollected += wood;
	}

	public int GetOreCollected()
	{
		return m_OreCollected;
	}

	public void AddOreCollected(int ore)
	{
		m_OreCollected += ore;
	}
}
