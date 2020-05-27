using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatField : MonoBehaviour
{
	private string m_Farmer = null;

	public void SetCurrentFarmer(string farmer)
	{
		m_Farmer = farmer;
	}

	public string GetCurrentFarmer()
	{
		return m_Farmer;
	}
}
