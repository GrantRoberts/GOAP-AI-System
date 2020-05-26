using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
	/// <summary>
	/// How much wood can be collected from the tree.
	/// </summary>
	public int m_WoodAmount = 5;

	/// <summary>
	/// How much wood to restore the trees to.
	/// </summary>
	private int m_DefaultWoodAmount = 0;

	/// <summary>
	/// If the tree is fully grown and can be harvested for wood.
	/// </summary>
	private bool m_FullyGrown = true;

	/// <summary>
	/// The scale at which the tree starts growing from.
	/// </summary>
	public Vector3 m_GrowStartingScale = Vector3.zero;

	/// <summary>
	/// The scale of the tree when it is fully grown.
	/// </summary>
	private Vector3 m_FullyGrownScale  = Vector3.zero;

	/// <summary>
	/// How long it takes for the tree to grow back.
	/// </summary>
	public float m_GrowingTime = 3.0f;

	/// <summary>
	/// The time an agent began cutting this tree down.
	/// </summary>
	private float m_StartGrowingTime = 0.0f;

	/// <summary>
	/// Who is currently targetting this tree.
	/// </summary>
	[SerializeField] private GameObject m_CurrentLogger = null;

	private void Awake()
	{
		m_FullyGrownScale = transform.localScale;
		m_DefaultWoodAmount = m_WoodAmount;
	}

	private void Update()
	{
		if (!m_FullyGrown)
		{
			float timeCovered = (Time.time - m_StartGrowingTime);
			float fractionOfTime = timeCovered / m_GrowingTime;
		
			transform.localScale = Vector3.Lerp(transform.localScale, m_FullyGrownScale, fractionOfTime);

			if (transform.localScale == m_FullyGrownScale)
			{
				m_FullyGrown = true;
				m_WoodAmount = m_DefaultWoodAmount;
			}
		}
	}

	/// <summary>
	/// Get how much wood the tree has left to be collected.
	/// </summary>
	/// <returns>How much wood can be colleceted.</returns>
	public int GetWoodAmount()
	{
		return m_WoodAmount;
	}

	/// <summary>
	/// Decrease the amount of wood that can be collected from the tree.
	/// </summary>
	/// <param name="decrease">How much to decrease the avaliable wood by.</param>
	public void DecreaseWoodAmount(int decrease)
	{
		m_WoodAmount -= decrease;

		if (m_WoodAmount == 0)
		{
			m_FullyGrown = false;
			transform.localScale = m_GrowStartingScale;
			m_StartGrowingTime = Time.time;
		}
	}

	/// <summary>
	/// Get if the tree is fully grown and ready for collecting wood from.
	/// </summary>
	/// <returns>If the tree is ready for wood to be collected from.</returns>
	public bool GetFullyGrown()
	{
		return m_FullyGrown;
	}

	public void SetCurrentLogger(GameObject logger)
	{
		m_CurrentLogger = logger;
	}

	public GameObject GetCurrentLogger()
	{
		return m_CurrentLogger;
	}
}